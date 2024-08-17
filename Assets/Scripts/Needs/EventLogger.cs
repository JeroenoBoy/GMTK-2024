using UnityEngine;


public class NeedLogger : MonoBehaviour
{
    public void Start()
    {
        EventBus.instance.onNeedAdded += (need) => Debug.Log($"Added Need <color=red>{need.name}</color>");
        EventBus.instance.onNeedBalanceLost += (need) => Debug.Log($"Unbalanced Need <color=red>{need.name}</color>");
        EventBus.instance.onNeedBalanceRegained += (need) => Debug.Log($"Need Balance Stabilized <color=red>{need.name}</color>");
    }
}