using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class notification : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _bar;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Need _need;
    [SerializeField] private float _duration;

    private float _timer = 0;
    private bool _didSendEvent;

    public void SetNotification(Need need)
    {
        _timer = 0;
        _didSendEvent = false;

        _need = need;
        _background.color = need.color;
        _icon.sprite = need.icon;
        NeedData needData = NeedManager.instance.needs[need];
        _text.text = needData.hasTooPhew ? need.tooLow : need.tooHigh;
    }

    private void OnEnable()
    {
        EventBus.instance.onNeedBalanceRegained += HandleNeedBalanceRegained;
    }

    private void OnDisable()
    {
        EventBus.instance.onNeedBalanceRegained -= HandleNeedBalanceRegained;
    }

    private void Update()
    {
        _timer += Time.deltaTime / _duration;
        if (_timer >= 1 && !_didSendEvent) {
            _didSendEvent = true;
            EventBus.instance.onNeedBalanceParienceLost?.Invoke(_need);
        }

        _bar.fillAmount = _timer * 4f / 3f - 1f / 3f;
    }

    private void HandleNeedBalanceRegained(Need need)
    {
        if (need == _need) {
            Destroy(gameObject);
        }
    }
}