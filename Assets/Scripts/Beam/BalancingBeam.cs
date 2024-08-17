using JUtils;
using UnityEngine;


public class BalancingBeam : MonoBehaviour
{
    [SerializeField] private Transform _directParent;
    [SerializeField] private BalancingContainer _containerLeft;
    [SerializeField] private BalancingContainer _containerRight;

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

    private float _leftMass;
    private float _rightMass;
    private float _smoothVel;

    private bool _didGoOutOfBalance = false;

    private void Awake()
    {
        EventBus.instance.onBuildingSettle += HandleBuildingSettle;
    }

    private void OnDestroy()
    {
        EventBus.instance.onBuildingSettle -= HandleBuildingSettle;
    }

    private void Update()
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

    private void HandleBuildingSettle(buildingFalling fallingBuilding)
    {
        float height = fallingBuilding.collider.ClosestPoint(Vector2.up * 10_000_000f).y;
        currentHeight = Mathf.Max(currentHeight, height);
    }
}