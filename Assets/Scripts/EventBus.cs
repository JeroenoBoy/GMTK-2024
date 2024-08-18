using System;
using JUtils;


public class EventBus : AutoSingletonBehaviour<EventBus>
{
    public Action onOutOfBalance;
    public Action<float> onHeightUpdate;
    public Action<float> onGodWeightUpdate;

    public Action<Need> onNeedAdded;
    public Action<Need> onNeedBalanceLost;
    public Action<Need> onNeedBalanceParienceLost;
    public Action<Need> onNeedBalanceRegained;
    public Action<buildingFalling> onBuildingSettle;
}