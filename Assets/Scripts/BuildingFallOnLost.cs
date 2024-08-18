using System.Collections;
using JUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BuildingFallOnLost : MonoBehaviour
    {
        [SerializeField] private MinMax _delay;

        private void OnEnable()
        {
            EventBus.instance.onOutOfBalance += HandleOutOfBalance;
        }

        private void OnDisable()
        {
            EventBus.instance.onOutOfBalance -= HandleOutOfBalance;
        }

        private void HandleOutOfBalance()
        {
            StartCoroutine(OutOfBalanceRoutine());
        }

        private IEnumerator OutOfBalanceRoutine()
        {
            yield return new WaitForSeconds(_delay.Random());
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();

            if (Random.Range(0f, 100f) > 50f) {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}