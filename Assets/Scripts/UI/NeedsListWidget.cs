using UnityEngine;


public class NeedsListWidget : MonoBehaviour
{
    [SerializeField] private NeedsWidget _needsWidget;

    public void SetNeeds(NeedPair[] needPairs)
    {
        foreach (NeedPair needPair in needPairs) {
            NeedsWidget instance = Instantiate(_needsWidget, transform);
            instance.SetNeed(needPair);
        }
    }
}