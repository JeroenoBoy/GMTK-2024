using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BuildingWidget : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private HoverThing _hoverThing;

    private Button _button;
    private GameObject _building;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(HandleClick);
    }

    public void SetBuilding(GameObject building)
    {
        _building = building;

        _image.sprite = building.GetComponent<SpriteRenderer>().sprite;
        _nameField.text = building.name.Replace("_", " ");

        NeedProvider[] providers = building.GetComponents<NeedProvider>();
        _hoverThing.SetNeedProviders(providers);
    }

    private void HandleClick()
    {
        PLaceBuilding.instance.SelectObject(_building);
        SoundManager.instance.Play("Selecated");
    }
}