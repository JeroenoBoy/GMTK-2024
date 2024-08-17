using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NeedsWidget : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _text;

    public void SetNeed(NeedPair needPair)
    {
        _background.color = needPair.need.color;
        _icon.sprite = needPair.need.icon;
        _text.text = needPair.need.name;
    }
}