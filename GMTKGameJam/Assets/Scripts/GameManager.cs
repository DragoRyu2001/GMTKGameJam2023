using System;
using System.Collections;
using System.Collections.Generic;
using DragoRyu.Utilities;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private WeightedRandom<Enemy> EnemyPrefabs;
    [SerializeField] private NumberRange SpawnRange;
    [Range(0.1f, 10)]
    [SerializeField] 
    private float InitialTime;

    public Transform PlayerTransform { get; private set; }
    public Action PlayerDeath;

    public float DropChance
    {
        get
        {
            var totalEnemies = _activeEnemies.Count;
            return Mathf.Lerp(0.5f, 0.1f, (float)totalEnemies/10);
        }
    }
    
    public static GameManager Instance;
    private static List<Enemy> _activeEnemies = new List<Enemy>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance !=this) 
        {
            Destroy(Instance);
            Instance = this;
        }
        SpawnPlayer();
        StartCoroutine(SpawnLogic());
    }
    private void SpawnPlayer()
    {
        Player player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player.SetData(Camera.main);
        PlayerTransform = player.transform;
    }

    private IEnumerator SpawnLogic()
    {
        yield return null;
        while (true)
        {
            yield return new WaitForSeconds(InitialTime);
            if (InitialTime >= 0.1f)
            {
                InitialTime -= 0.01f;
            }
            else
            {
                InitialTime = 0.5f;
            }
            SpawnEnemy();
        }
    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(2*60f);
        //SpawnBoss()
    }
    private void SpawnEnemy()
    {
        if (PlayerTransform == null) return;
        var origin = PlayerTransform.position;
        float angle = Random.Range(0, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(SpawnRange.Min, SpawnRange.Max);
        Vector2 cartPosition;
        cartPosition.x = distance * Mathf.Cos(angle);
        cartPosition.y = distance * Mathf.Sin(angle);
        var obj = EnemyPrefabs.GetWeightedRandom();
        var enemy = Instantiate(obj, cartPosition, Quaternion.identity);
        _activeEnemies.Add(enemy);
    }
    public void EnemyDied(Enemy enemy)
    {
        _activeEnemies.Remove(enemy);
    }
    public void PlayerDied()
    {
        StopAllCoroutines();
        PlayerDeath.SafeInvoke();
    }
}
