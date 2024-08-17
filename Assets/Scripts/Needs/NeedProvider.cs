using System;
using JUtils;
using UnityEngine;


public class NeedProvider : MonoBehaviour
{
    [field: SerializeField] public NeedPair[] needs { get; private set; }
    [field: SerializeField] public NeedPair[] provides { get; private set; }

    private int[] _needValues;
    private int[] _providesValues;

    private void OnEnable()
    {
        NeedManager needManager = NeedManager.instance;

        _needValues = new int[needs.Length];
        _providesValues = new int[provides.Length];

        for (int i = 0; i < needs.Length; i++) {
            NeedPair pair = needs[i];
            int amount = pair.value.Random();
            _needValues[i] = amount;
            needManager.ConsumeNeed(pair.need, amount);
        }

        for (int i = 0; i < provides.Length; i++) {
            NeedPair pair = provides[i];
            int amount = pair.value.Random();
            _providesValues[i] = amount;
            needManager.AddNeed(pair.need, amount);
        }
    }

    private void OnDisable()
    {
        NeedManager needManager = NeedManager.instance;

        for (int i = 0; i < needs.Length; i++) {
            NeedPair pair = needs[i];
            needManager.ConsumeNeed(pair.need, -_needValues[i]);
        }

        for (int i = 0; i < provides.Length; i++) {
            NeedPair pair = provides[i];
            needManager.AddNeed(pair.need, -_providesValues[i]);
        }
    }
}


[Serializable]
public struct NeedPair
{
    [field: SerializeField] public Need need { get; private set; }
    [field: SerializeField] public MinMaxInt value { get; private set; }
}