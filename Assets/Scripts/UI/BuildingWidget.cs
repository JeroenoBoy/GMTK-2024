using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BuildingWidget : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _nameField;

    [SerializeField] private NeedsListWidget _requiresList;
    [SerializeField] private NeedsListWidget _providesList;

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

        NeedProvider provider = building.GetComponent<NeedProvider>();

        _requiresList.SetNeeds(provider.needs);
        _providesList.SetNeeds(provider.provides);
    }

    private void HandleClick()
    {
        PLaceBuilding.instance.SelectObject(_building);
    }
}