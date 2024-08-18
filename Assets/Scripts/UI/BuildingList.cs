using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BuildingList : MonoBehaviour
{
    [SerializeField] private BuildingWidget _buildingWidget;

    private List<GameObject> _objects = new();

    private void Start()
    {
        Recalculate();
    }

    private void OnEnable()
    {
        EventBus.instance.onHeightUpdate += HandleHeightChanged;
    }

    private void OnDisable()
    {
        EventBus.instance.onHeightUpdate -= HandleHeightChanged;
    }

    private void Recalculate()
    {
        foreach (GameObject o in _objects) {
            Destroy(o);
        }

        _objects.Clear();

        GameObject[] objs = Resources.LoadAll<GameObject>("Buildings");
        foreach (GameObject o in objs.OrderBy(o => o.GetComponents<NeedProvider>().OrderByDescending(it => it.minHeight).First().minHeight)) {
            BuildingWidget instance = Instantiate(_buildingWidget, transform);
            _objects.Add(instance.gameObject);
            instance.SetBuilding(o);
        }
    }

    private void HandleHeightChanged(float height)
    {
        Recalculate();
    }
}