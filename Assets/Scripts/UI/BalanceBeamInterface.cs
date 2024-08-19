using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class BalanceBeamInterface : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _degreesMax;
    [SerializeField] private Image _image;
    [SerializeField] private Gradient _imageGradient;

    private bool _didGameEnd;

    private void OnEnable()
    {
        EventBus.instance.onOutOfBalance += HandleOutOfBalance;
    }

    private void OnDisable()
    {
        EventBus.instance.onOutOfBalance -= HandleOutOfBalance;
    }

    private void Update()
    {
        if (_didGameEnd) return;

        float t = BalancingBeam.instance.unbalancedPercentage;
        t = Mathf.Sqrt(Mathf.Abs(t)) * Mathf.Sign(t);

        Quaternion targetRotation = Quaternion.Euler(0, 0, t * _degreesMax);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, 1 - Mathf.Exp(-10 * Time.deltaTime));
        _image.color = _imageGradient.Evaluate(Mathf.Abs(t));
    }

    private IEnumerator GameEndedRoutine()
    {
        Vector3 velocity = Vector3.zero;
        Color start = _imageGradient.Evaluate(1);
        Color end = start;
        end.a = 0;

        for (float time = 0; time < 1; time += Time.deltaTime) {
            velocity += Vector3.down * (9.81f * Time.deltaTime);
            transform.position += velocity;

            _image.color = Color.Lerp(start, end, 1);
            yield return null;
        }
    }

    private void HandleOutOfBalance()
    {
        _didGameEnd = true;
        StartCoroutine(GameEndedRoutine());
    }
}