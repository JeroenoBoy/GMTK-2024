using System.Collections;
using JUtils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BuildingFallOnLost : MonoBehaviour
    {
        [SerializeField] private float _destroyChance = 10;
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
            if (Random.Range(0, 100f) < _destroyChance) {
                Destroy(gameObject);
            } else {
                gameObject.AddComponent<Rigidbody2D>();

                if (Random.Range(0f, 100f) > 70f) {
                    GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
}