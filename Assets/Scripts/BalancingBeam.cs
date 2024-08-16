using UnityEngine;


public class BalancingBeam : MonoBehaviour
{
    [SerializeField] private Transform _beamTransform;
    [SerializeField] private BalancingContainer _containerLeft;
    [SerializeField] private BalancingContainer _containerRight;

    [Header("Settings")]
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _maxWeightDifference;

    [Header("Wiggle")]
    [SerializeField] private float _wiggleIntensity;
    [SerializeField] private float _wiggleFrequency;

    private float _leftMass;
    private float _rightMass;

    public void Update()
    {
        float offsetMovement = (Mathf.PerlinNoise(0, Time.time * _wiggleFrequency) * 2 - 1) * _wiggleIntensity;

        float massPercentage = _containerRight.currentWeight * _containerLeft.currentWeight / _maxWeightDifference;
        float angle = massPercentage * _maxAngle;

        _beamTransform.rotation = Quaternion.Euler(0, 0, angle + offsetMovement);
    }
}