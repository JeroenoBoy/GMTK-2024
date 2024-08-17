using UnityEngine;


public class BalancingBeam : MonoBehaviour
{
    [SerializeField] private Transform _beamTransform;
    [SerializeField] private BalancingContainer _containerLeft;
    [SerializeField] private BalancingContainer _containerRight;

    [Header("Settings")]
    [SerializeField] private float _maxWeightDifference;
    [SerializeField] private float _currentHeight;
    [SerializeField] private float _maxSway;
    [SerializeField] private float _maxAngle;

    [Header("Wiggle")]
    [SerializeField] private float _wiggleIntensity;
    [SerializeField] private float _wiggleFrequency;

    private float _leftMass;
    private float _rightMass;

    private bool _didGoOutOfBalance = false;

    public void Update()
    {
        float offsetMovement = (Mathf.PerlinNoise(0, Time.time * _wiggleFrequency) * 2 - 1) * _wiggleIntensity;

        float massPercentage = (_containerRight.currentWeight - _containerLeft.currentWeight) / _maxWeightDifference;

        float maxAngle = Mathf.Atan2(_maxSway, _currentHeight) * Mathf.Rad2Deg;
        maxAngle = Mathf.Min(maxAngle, _maxAngle);
        float angle = massPercentage * maxAngle;

        if (massPercentage > 0 && !_didGoOutOfBalance) {
            _didGoOutOfBalance = true;
            EventBus.instance.onOutOfBalance?.Invoke();
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle + offsetMovement);
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 - Mathf.Exp(-10 * Time.deltaTime));
        transform.rotation = newRotation;
    }
}