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
        EventBus.instance.onGodWeightUpdate += HandleWeightChanged;
    }

    private void OnDisable()
    {
        EventBus.instance.onGodWeightUpdate -= HandleWeightChanged;
    }

    private void Recalculate()
    {
        foreach (GameObject o in _objects) {
            Destroy(o);
        }

        _objects.Clear();

        GameObject[] objs = Resources.LoadAll<GameObject>("Buildings");
        float godWeight = God.instance.currentWeight;
        foreach (GameObject o in objs
           .Select(it => (it, it.GetComponents<NeedProvider>().OrderBy(it => it.minWeight).ToArray()))
           .Where(it => godWeight >= it.Item2.First().minWeight)
           .OrderBy(it => it.Item2.First().minWeight)
           .Select(it => it.it)) {
            BuildingWidget instance = Instantiate(_buildingWidget, transform);
            _objects.Add(instance.gameObject);
            instance.SetBuilding(o);
        }
    }

    private void HandleWeightChanged(float weight)
    {
        Recalculate();
    }
}