using DragoRyu.Utilities;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private EnemyBoss EnemyBossPrefab;
    [SerializeField] private WeightedRandom<Enemy> EnemyPrefabs;
    [SerializeField] private NumberRange SpawnRange;
    [SerializeField] private List<Weapon> pickableWeapons;
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
    public static List<Enemy> _activeEnemies = new();
    public static List<Weapon> _pickableWeapons;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
            Instance = this;
        }
        _pickableWeapons = new();
        _pickableWeapons.AddRange(pickableWeapons);
        SpawnPlayer();
        StartCoroutine(SpawnLogic());
    }
    private void SpawnPlayer()
    {
        Player player = Instantiate(PlayerPrefab, Vector3.up * 10f + Vector3.right *10f, Quaternion.identity); ;

        player.SetData(Camera.main);
        PlayerTransform = player.transform;
        StartCoroutine(SpawnBoss());
    }

    private void Update()
    {
        Debug.Log(_pickableWeapons.Count);
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
        yield return null;
        EnemyBoss boss = Instantiate(EnemyBossPrefab, Vector3.zero, Quaternion.identity);
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
