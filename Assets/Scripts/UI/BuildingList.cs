using UnityEngine;


public class BuildingList : MonoBehaviour
{
    [SerializeField] private BuildingWidget _buildingWidget;

    private void Start()
    {
        GameObject[] objs = Resources.LoadAll<GameObject>("Buildings");
        foreach (GameObject o in objs) {
            BuildingWidget instance = Instantiate(_buildingWidget, transform);
            instance.SetBuilding(o);
        }
    }
}