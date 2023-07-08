using System;
using System.Collections;
using System.Collections.Generic;
using DragoRyu.Utilities;
using Entities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private List<Enemy> EnemyPrefabs;
    [SerializeField] private NumberRange SpawnRange;
    [Range(0.1f, 10)]
    [SerializeField] 
    private float InitialTime;
    public Transform PlayerTransform { get; private set; }
    public Action PlayerDeath;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance !=this) 
        {
            Destroy(instance);
            instance = this;
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
            if(InitialTime>=0.1f)
                InitialTime -= 0.01f;
            SpawnEnemy();
        }

    }
    private void SpawnEnemy()
    {
        var origin = PlayerTransform.position;
        float angle = Random.Range(0, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(SpawnRange.Min, SpawnRange.Max);
        Vector2 cartPosition;
        cartPosition.x = distance * Mathf.Cos(angle);
        cartPosition.y = distance * Mathf.Sin(angle);
        var obj = EnemyPrefabs.GetRandom();
        Instantiate(obj, cartPosition, Quaternion.identity);
    }

    public void EnemyDied()
    {
        
    }
    public void PlayerDied()
    {
        StopAllCoroutines();
        PlayerDeath.SafeInvoke();
    }
}
