using DragoRyu.Utilities;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private EnemyBoss EnemyBossPrefab;
    [SerializeField] private WeightedRandom<Enemy> EnemyPrefabs;
    [SerializeField] private NumberRange SpawnRange;
    [SerializeField] private Weapon FirstWeaponPickup;
    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    [FormerlySerializedAs("pickableWeapons")] [SerializeField] private List<Weapon> pickableWeapons;
    [Range(0.1f, 10)]
    [SerializeField]
    private float InitialTime;

    public Transform PlayerTransform { get; private set; }
    public Transform BossTransform { get; private set; }
    public Action PlayerDeath;
    public Action GameStart;
    public Action BossPhase;
    public float DropChance
    {
        get
        {
            var totalEnemies = ActiveEnemies.Count;
            return Mathf.Lerp(0.5f, 0.1f, (float)totalEnemies/10);
        }
    }

    public static GameManager Instance;
    public static readonly List<Enemy> ActiveEnemies = new();
    public static List<Weapon> PickableWeapons;
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
        PickableWeapons = new();
        PickableWeapons.AddRange(PickableWeapons);
    }

    private void Start()
    {
        SpawnPlayer();
        FirstWeaponPickup.OnPickupEvent += StartGame;
    }

    private void StartGame()
    {
        GameStart.SafeInvoke();
        StartCoroutine(SpawnLogic());
        StartCoroutine(SpawnBossTimer());
    }

    private void SpawnPlayer()
    {
        Player player = Instantiate(PlayerPrefab, Vector2.zero, Quaternion.identity); ;
        player.SetData(Camera.main);
        PlayerTransform = player.transform;
        VirtualCamera.Follow = PlayerTransform;
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

    private IEnumerator SpawnBossTimer()
    {
        yield return new WaitForSeconds(.5f*60f);
        EnemyBoss boss = Instantiate(EnemyBossPrefab, GetEnemySpawnPosition(), Quaternion.identity);
        BossPhase.SafeInvoke();
        
    }
    private void SpawnEnemy()
    {
        Vector2 cartPosition = GetEnemySpawnPosition();
        var obj = EnemyPrefabs.GetWeightedRandom();
        var enemy = Instantiate(obj, cartPosition, Quaternion.identity);
        ActiveEnemies.Add(enemy);
    }

    private Vector2 GetEnemySpawnPosition()
    {
        if (PlayerTransform == null) return Vector2.zero;
        var origin = PlayerTransform.position;
        float angle = Random.Range(0, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(SpawnRange.Min, SpawnRange.Max);
        Vector2 cartPosition;
        cartPosition.x = distance * Mathf.Cos(angle);
        cartPosition.y = distance * Mathf.Sin(angle);
        return cartPosition;
    }

    public void EnemyDied(Enemy enemy)
    {
        ActiveEnemies.Remove(enemy);
    }
    public void PlayerDied(Type type)
    {
        if (type != typeof(Player)) return;
        StopAllCoroutines();
        PlayerDeath.SafeInvoke();
    }
}
