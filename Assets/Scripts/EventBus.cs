using System;
using JUtils;


public class EventBus : AutoSingletonBehaviour<EventBus>
{
    public Action onOutOfBalance;

    public Action<Need> onNeedAdded;
    public Action<Need> onNeedBalanceLost;
    public Action<Need> onNeedBalanceRegained;
    public Action<buildingFalling> onBuildingSettle;
}