using System;
using System.Collections;
using System.Collections.Generic;
using DragoRyu.Utilities;
using Entities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private List<GameObject> EnemyPrefab;
    public Transform PlayerTransform { get; private set; }
    public Action PlayerDeath;
    
    // Start is called before the first frame update
    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        var player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player.SetData();
        PlayerTransform = player.transform;
    }

    private IEnumerator SpawnLogic()
    {
        
        
        yield break;
    }
    private void SpawnEnemies()
    {
        
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
