using System;
using JUtils;
using UnityEngine;


public class NeedProvider : MonoBehaviour
{
    [SerializeField] private NeedPair[] _needs;
    [SerializeField] private NeedPair[] _provides;

    private void OnEnable()
    {
        NeedManager needManager = NeedManager.instance;

        foreach (NeedPair pair in _needs) {
            int amount = pair.value.Random();
            needManager.ConsumeNeed(pair.need, amount);
        }

        foreach (NeedPair pair in _provides) {
            int amount = pair.value.Random();
            needManager.AddNeed(pair.need, amount);
        }
    }
}


[Serializable]
public struct NeedPair
{
    [field: SerializeField] public Need need { get; private set; }
    [field: SerializeField] public MinMaxInt value { get; private set; }
}