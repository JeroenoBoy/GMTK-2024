using UnityEngine;

namespace DefaultNamespace
{
    public class BalancingBeam : MonoBehaviour
    {
        [SerializeField] private Transform _beamTransform;

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
            float offsetMovement = Mathf.PerlinNoise(0, Time.time * _wiggleFrequency) * _wiggleIntensity;

            float massPercentage = _rightMass * _leftMass / _maxWeightDifference;
            float angle = massPercentage * _maxAngle;

            _beamTransform.rotation = Quaternion.Euler(0, 0, angle + offsetMovement);
        }
    }
}