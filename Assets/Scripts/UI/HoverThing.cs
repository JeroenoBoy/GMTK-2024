using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HoverThing : MonoBehaviour
{
    [SerializeField] private NeedsListWidget _requiresList;
    [SerializeField] private NeedsListWidget _providesList;

    private NeedProvider[] _needProviders;

    public void SetNeedProviders(NeedProvider[] providers)
    {
        _needProviders = providers;
    }

    private void OnEnable()
    {
        float height = BalancingBeam.instance.currentHeight;
        NeedProvider[] activeProviders = _needProviders.Where(it => it.minHeight > height).ToArray();

        _requiresList.SetNeeds(activeProviders.SelectMany(it => it.needs).Distinct(new ManStf()).ToArray());
        _providesList.SetNeeds(activeProviders.SelectMany(it => it.provides).Distinct(new ManStf()).ToArray());
    }


    private struct ManStf : IEqualityComparer<NeedPair>
    {
        public bool Equals(NeedPair x, NeedPair y)
        {
            return x.need == y.need;
        }

        public int GetHashCode(NeedPair obj)
        {
            return HashCode.Combine(obj.need, obj.value);
        }
    }
}