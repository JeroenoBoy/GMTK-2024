using System.Collections.Generic;
using UnityEngine;


public class NeedsListWidget : MonoBehaviour
{
    [SerializeField] private NeedsWidget _needsWidget;

    private List<GameObject> _objs = new();

    public void SetNeeds(NeedPair[] needPairs)
    {
        foreach (GameObject o in _objs) {
            Destroy(o);
        }

        _objs.Clear();

        foreach (NeedPair needPair in needPairs) {
            NeedsWidget instance = Instantiate(_needsWidget, transform);
            _objs.Add(instance.gameObject);
            instance.SetNeed(needPair);
        }
    }
}