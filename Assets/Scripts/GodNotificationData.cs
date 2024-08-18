using System;
using JUtils;
using UnityEngine;


[CreateAssetMenu]
public class GodNotificationData : ScriptableObject
{
    [field: SerializeField] [field: TextArea(2, 2)]
    public string message { get; private set; }
    [field: SerializeField] public float stayTime { get; private set; } = 5f;
    [field: SerializeField] [field: Space] [field: SerializeReference] [field: TypeSelector]
    public IGodNotificationCheck notificationCheck { get; private set; }
}


public interface IGodNotificationCheck
{
    public void OnEnable();
    public void OnDisable();
    public bool CanSee();
}


[Serializable]
public class HeightCheck : IGodNotificationCheck
{
    [SerializeField] private float _height;

    public void OnEnable() { }
    public void OnDisable() { }

    public bool CanSee()
    {
        return BalancingBeam.instance.currentHeight > _height;
    }
}


[Serializable]
public class NeedNotification : IGodNotificationCheck
{
    [SerializeField] private Optional<Need> _need;

    private bool _isBalanceLostOnce;

    public void OnEnable()
    {
        _isBalanceLostOnce = false;
        EventBus.instance.onNeedBalanceLost += HandleBalanceLost;
    }

    public void OnDisable()
    {
        EventBus.instance.onNeedBalanceLost -= HandleBalanceLost;
    }

    public bool CanSee()
    {
        return _isBalanceLostOnce;
    }

    private void HandleBalanceLost(Need need)
    {
        if (_need.enabled && need != _need.value) {
            return;
        }

        _isBalanceLostOnce = true;
    }
}


[Serializable]
public class PatienceLost : IGodNotificationCheck
{
    [SerializeField] private Optional<Need> _need;

    private bool _isBalanceLostOnce;

    public void OnEnable()
    {
        _isBalanceLostOnce = false;
        EventBus.instance.onNeedBalanceParienceLost += HandleBalanceLost;
    }

    public void OnDisable()
    {
        EventBus.instance.onNeedBalanceParienceLost -= HandleBalanceLost;
    }

    public bool CanSee()
    {
        return _isBalanceLostOnce;
    }

    private void HandleBalanceLost(Need need)
    {
        if (_need.enabled && need != _need.value) {
            return;
        }

        _isBalanceLostOnce = true;
    }
}


[Serializable]
public class NeedAdded : IGodNotificationCheck
{
    [SerializeField] private Optional<Need> _need;

    private bool _isNeedAdded;

    public void OnEnable()
    {
        _isNeedAdded = false;
        EventBus.instance.onNeedAdded += HandleNeedAdded;
    }

    public void OnDisable()
    {
        EventBus.instance.onNeedAdded -= HandleNeedAdded;
    }

    public bool CanSee()
    {
        return _isNeedAdded;
    }

    private void HandleNeedAdded(Need need)
    {
        if (_need.enabled && need != _need.value) {
            return;
        }

        _isNeedAdded = true;
    }
}


[Serializable]
public class BuildingPlaced : IGodNotificationCheck
{
    [SerializeField] private Optional<GameObject> _prefab;

    private bool _isNeedAdded;

    public void OnEnable()
    {
        _isNeedAdded = false;
        EventBus.instance.onBuildingSettle += HandleBuildingPlaced;
    }

    public void OnDisable()
    {
        EventBus.instance.onBuildingSettle -= HandleBuildingPlaced;
    }

    public bool CanSee()
    {
        return _isNeedAdded;
    }

    private void HandleBuildingPlaced(buildingFalling buildingFalling)
    {
        if (_prefab.enabled && buildingFalling.prefab != _prefab.value) {
            return;
        }

        _isNeedAdded = true;
    }
}


[Serializable]
public class IsNearlyUnbalanced : IGodNotificationCheck
{
    public void OnEnable() { }
    public void OnDisable() { }

    public bool CanSee()
    {
        return Mathf.Abs(BalancingBeam.instance.unbalancedPercentage) > .75f;
    }
}