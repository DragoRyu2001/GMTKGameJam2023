using System;
using System.Collections;
using System.Collections.Generic;
using DragoRyu.Utilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
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
        PlayerTransform = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity).transform;
        
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
