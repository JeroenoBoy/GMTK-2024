using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Godcube : Weightable
    {
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _height = 0.5f;

        public void StartMoving(Vector3 startPosition, Vector3 endPosition, Action onCubeLand)
        {
            StartCoroutine(MovingRoutine(startPosition, endPosition, onCubeLand));
        }

        private IEnumerator MovingRoutine(Vector3 startPosition, Vector3 targetPosition, Action onCubeLand)
        {
            transform.localPosition = startPosition;
            Vector3 moveDir = Vector3.down;
            float velocity = _startSpeed;

            while (Vector3.Dot(moveDir, targetPosition - transform.localPosition) > 0) {
                yield return new WaitForFixedUpdate();

                velocity += _acceleration * Time.fixedDeltaTime;
                transform.localPosition += moveDir * velocity * Time.fixedDeltaTime;
            }

            transform.localPosition = targetPosition;
            onCubeLand?.Invoke();
        }
    }
}