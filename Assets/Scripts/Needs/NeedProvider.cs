using System;
using JUtils;
using UnityEngine;


public class NeedProvider : MonoBehaviour
{
    [SerializeField] private NeedPair[] _needs;
    [SerializeField] private NeedPair[] _provides;

    private int[] _needValues;
    private int[] _providesValues;

    private void OnEnable()
    {
        NeedManager needManager = NeedManager.instance;

        _needValues = new int[_needs.Length];
        _providesValues = new int[_provides.Length];

        for (int i = 0; i < _needs.Length; i++) {
            NeedPair pair = _needs[i];
            int amount = pair.value.Random();
            _needValues[i] = amount;
            needManager.ConsumeNeed(pair.need, amount);
        }

        for (int i = 0; i < _provides.Length; i++) {
            NeedPair pair = _provides[i];
            int amount = pair.value.Random();
            _providesValues[i] = amount;
            needManager.AddNeed(pair.need, amount);
        }
    }

    private void OnDisable()
    {
        NeedManager needManager = NeedManager.instance;

        for (int i = 0; i < _needs.Length; i++) {
            NeedPair pair = _needs[i];
            needManager.ConsumeNeed(pair.need, -_needValues[i]);
        }

        for (int i = 0; i < _provides.Length; i++) {
            NeedPair pair = _provides[i];
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