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
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        Player player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player.SetData(Camera.main);
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
