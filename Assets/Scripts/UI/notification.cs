using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class notification : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Need _need;

    public void SetNotification(Need need)
    {
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
    private void HandleNeedBalanceRegained(Need need)
    {
        if(need == _need)
        {
            Destroy(gameObject);
        }
        
            
          
    }
}
