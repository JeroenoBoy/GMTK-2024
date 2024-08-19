using JUtils;
using UnityEngine;


public class BalancingBeam : SingletonBehaviour<BalancingBeam>
{
    [SerializeField] private Transform _directParent;
    [SerializeField] private BalancingContainer _containerLeft;
    [SerializeField] private BalancingContainer _containerRight;
    [SerializeField] private Need _heightNeed;

    [Header("Settings")]
    [SerializeField] private float _maxWeightDifference;
    [field: SerializeField] public float currentHeight { get; private set; }
    [SerializeField] private float _maxSway;
    [SerializeField] private float _maxAngle;

    [Header("Wiggle")]
    [SerializeField] private float _wiggleIntensity;
    [SerializeField] private float _wiggleFrequency;

    [Header("Moving of pivot")]
    [SerializeField] private float _maxDistanceBehind;
    [SerializeField] private float _pivotSmoothTime;
    [SerializeField] private float _pivotMoveSpeed;

    public float unbalancedPercentage => (_containerRight.currentWeight - _containerLeft.currentWeight) / _maxWeightDifference;
    public bool isDone => _didGoOutOfBalance;

    private float _leftMass;
    private float _rightMass;
    private float _smoothVel;

    private bool _didGoOutOfBalance = false;
    private float _gameDoneTimer;

    protected override void Awake()
    {
        base.Awake();
        EventBus.instance.onBuildingSettle += HandleBuildingSettle;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EventBus.instance.onBuildingSettle -= HandleBuildingSettle;
    }

    private void Update()
    {
        if (_didGoOutOfBalance) {
            ProcessGameOver();
        } else {
            ProcessBalancing();
        }
    }

    private void ProcessBalancing()
    {
        float currentY = transform.position.y;
        float targetY = Mathf.Max(currentHeight - _maxDistanceBehind, currentY);
        float newY = Mathf.SmoothDamp(currentY, targetY, ref _smoothVel, _pivotMoveSpeed, _pivotMoveSpeed);
        transform.position = transform.position.With(y: newY);
        _directParent.localPosition = Vector3.down * newY;

        float offsetMovement = (Mathf.PerlinNoise(0, Time.time * _wiggleFrequency) * 2 - 1) * _wiggleIntensity;
        float massPercentage = unbalancedPercentage;

        float maxAngle = Mathf.Atan2(_maxSway, currentHeight - transform.position.y) * Mathf.Rad2Deg;
        maxAngle = Mathf.Min(maxAngle, _maxAngle);
        float angle = massPercentage * maxAngle;

        if (Mathf.Abs(massPercentage) > 1 && !_didGoOutOfBalance) {
            _didGoOutOfBalance = true;
            EventBus.instance.onOutOfBalance?.Invoke();
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + offsetMovement);
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 - Mathf.Exp(-10 * Time.deltaTime));
        transform.rotation = newRotation;
    }

    private void ProcessGameOver()
    {
        _gameDoneTimer += Time.deltaTime / 5;

        Quaternion targetRotation = Quaternion.Euler(0, 0, 85 * Mathf.Sign(unbalancedPercentage));
        Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 - Mathf.Exp(-Mathf.Clamp01(_gameDoneTimer) * Time.deltaTime));
        transform.rotation = newRotation;
    }

    private void HandleBuildingSettle(buildingFalling fallingBuilding)
    {
        float height = fallingBuilding.collider.ClosestPoint(Vector2.up * 10_000_000f).y;
        if (height > currentHeight) {
            currentHeight = height;
            EventBus.instance.onHeightUpdate?.Invoke(currentHeight);
        }

        NeedManager man = NeedManager.instance;
        if (!man.needs.TryGetValue(_heightNeed, out NeedData needData)) {
            needData = new NeedData(_heightNeed);
            man.needs[_heightNeed] = needData;
        }

        needData.consumed = Mathf.CeilToInt(currentHeight);
        man.RecalculateNeed(_heightNeed);
    }
}