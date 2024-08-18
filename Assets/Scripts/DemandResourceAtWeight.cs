using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class DemandResourceAtWeight : MonoBehaviour
    {
        [SerializeField] private Demand[] _demands;

        private List<Demand> _demandsLeft;

        private void Awake()
        {
            _demandsLeft = _demands.ToList();
        }

        private void FixedUpdate()
        {
            List<Demand> demandsToRemove = new();
            float weight = God.instance.currentWeight;
            foreach (Demand demand in _demandsLeft) {
                if (weight < demand.weight) continue;
                demandsToRemove.Add(demand);
                NeedManager.instance.AddNeed(demand.need, demand.amount);
            }

            foreach (Demand demand in demandsToRemove) {
                _demandsLeft.Remove(demand);
            }
        }


        [Serializable]
        private class Demand
        {
            public Need need;
            public int amount;
            public int weight;
        }
    }
}