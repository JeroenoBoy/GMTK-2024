using UnityEngine;


public class SideWidget : MonoBehaviour
{
    [SerializeField] private int _side;
    [SerializeField] private float _margin = .3f;
    [SerializeField] private GameObject _obj;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup.alpha = 0;
    }

    private void Update()
    {
        float p = BalancingBeam.instance.unbalancedPercentage;
        float target = Mathf.Sign(p) != _side ? Mathf.Clamp01(Mathf.Abs(p) * (1 / _margin) - (1 - _margin)) : 0f;
        _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, target, 1 - Mathf.Exp(-1 * Time.deltaTime));
    }
}