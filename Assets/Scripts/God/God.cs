using DefaultNamespace;
using JUtils;
using UnityEngine;


public class God : BalancingContainer
{
    [SerializeField] private Godcube _godCube;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private int _spawnRows;
    [SerializeField] private float _spawnPadding;
    [SerializeField] private float _spawnHeight;

    public float towerHeight => Mathf.Floor((float)_spawnCount / _spawnRows) * _spawnPadding;

    private int _spawnCount;
    private float _spawnTimer;

    private void Start()
    {
        _spawnTimer = _spawnInterval;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
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
}