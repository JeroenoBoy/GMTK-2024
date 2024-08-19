using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BuildingList : MonoBehaviour
{
    [SerializeField] private BuildingWidget _buildingWidget;

    private List<GameObject> _objects = new();
    private bool _bypass;

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
        GameObject[] objs = Resources.LoadAll<GameObject>("Buildings");

        if (!_bypass) {
            float godWeight = God.instance.currentWeight;
            objs = objs
               .Select(it => (it, it.GetComponents<NeedProvider>().OrderBy(it => it.minWeight).ToArray()))
               .Where(it => godWeight >= it.Item2.First().minWeight)
               .OrderBy(it => it.Item2.First().minWeight)
               .Select(it => it.it)
               .ToArray();
        }

        if (objs.Length == _objects.Count) return;

        foreach (GameObject o in _objects) {
            Destroy(o);
        }

        _objects.Clear();

        foreach (GameObject o in objs) {
            BuildingWidget instance = Instantiate(_buildingWidget, transform);
            _objects.Add(instance.gameObject);
            instance.SetBuilding(o);
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) {
            _bypass = !_bypass;
        }
    }
#endif

    private void HandleWeightChanged(float weight)
    {
        Recalculate();
    }
}