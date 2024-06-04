using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawns;

    [Header("Enemies")]
    [SerializeField] private EnemyController _zombi;

    private int _roundCount = 1;
    private List<EnemyController> _enemies = new List<EnemyController>();

    private const int DEF_ZOMBI_SPAWN_CHACHE = 50;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartNewRound();
    }

    public void StartNewRound()
    {
        PlayerController.Instance.IsCanMoving = true;
        int zombiSpawnChance = DEF_ZOMBI_SPAWN_CHACHE + (int)(_roundCount * 1.5);
        zombiSpawnChance = zombiSpawnChance < 100 ? zombiSpawnChance : 100;

        foreach (var spawn in _spawns)
        {
            int random = Random.Range(0, 101);
            switch (random)
            {
                case var n when n <= zombiSpawnChance:
                    _enemies.Add(Instantiate(_zombi, spawn.position, _zombi.transform.rotation));
                    break;
            } 
        }

        if(_enemies.Count == 0)
        {
            _enemies.Add(Instantiate(_zombi, _spawns[Random.Range(0, _spawns.Length)].position, _zombi.transform.rotation));
        }
    }

    public void CheckFinishRound()
    {
        int enemyLeft = _enemies.Count;
        foreach (var enemy in _enemies)
        {
            if (!enemy.IsDead) return;

            enemyLeft--;
        }

        if (enemyLeft != 0) return;
        FinishRound();
    }

    private void FinishRound()
    {
        _roundCount++;

        foreach (var enemy in _enemies)
        {
            Destroy(enemy.gameObject);
        }

        _enemies.Clear();

        PlayerController.Instance.IsCanMoving = false;
        UIController.Instance.ShowStatShop();
    }
}
