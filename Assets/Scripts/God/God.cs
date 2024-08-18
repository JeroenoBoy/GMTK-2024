using System.Collections.Generic;
using DefaultNamespace;
using JUtils;
using UnityEngine;


public class God : BalancingContainer, ISingleton<God>
{
    public static God instance => SingletonManager.GetSingleton<God>();

    [SerializeField] private Godcube _godCube;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _spawnRows;
    [SerializeField] private float _spawnPadding;
    [SerializeField] private float _spawnHeight;
    [SerializeField] private AnimationCurve _unbalanceSpawnMulti;

    private List<Need> _unbalancedNeeds = new();
    private int _spawnCount;
    private float _spawnTimer;

    private void Awake()
    {
        if (!SingletonManager.SetSingleton(this)) {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        SingletonManager.RemoveSingleton<God>();
    }

    private void OnEnable()
    {
        EventBus.instance.onNeedBalanceParienceLost += HandlePatienceLost;
        EventBus.instance.onNeedBalanceRegained += HandleBalanceRegained;
    }

    private void OnDisable()
    {
        EventBus.instance.onNeedBalanceParienceLost -= HandlePatienceLost;
        EventBus.instance.onNeedBalanceRegained -= HandleBalanceRegained;
    }

    private void Start()
    {
        _spawnTimer = _spawnInterval;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime * _unbalanceSpawnMulti.Evaluate(_unbalancedNeeds.Count);
        if (_spawnTimer > 0) return;
        _spawnTimer += _spawnInterval;

        Vector3 targetPosition = Vector3.left * (_spawnCount % _spawnRows * _spawnPadding - _spawnRows * _spawnPadding * .5f)
            + Vector3.up * (Mathf.Floor((float)_spawnCount / _spawnRows) * _spawnPadding);
        Vector3 spawnPosition = targetPosition + Vector3.up * _spawnHeight;
        Godcube instance = Instantiate(_godCube, Vector3.zero, Quaternion.identity);

        instance.transform.parent = transform;
        instance.transform.Reset();
        instance.StartMoving(spawnPosition, targetPosition, () => currentWeight += instance.weight);

        _spawnCount++;
    }

    private void HandlePatienceLost(Need need)
    {
        if (_unbalancedNeeds.Contains(need)) return;
        _unbalancedNeeds.Add(need);
    }

    private void HandleBalanceRegained(Need need)
    {
        _unbalancedNeeds.Remove(need);
    }
}