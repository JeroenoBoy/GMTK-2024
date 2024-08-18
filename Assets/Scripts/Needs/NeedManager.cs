using System.Collections.Generic;
using JUtils;


public class NeedManager : SingletonBehaviour<NeedManager>
{
    public Dictionary<Need, NeedData> needs = new();

    public void AddNeed(Need need, int value)
    {
        bool isNew = !needs.TryGetValue(need, out NeedData current);
        if (isNew) current = new NeedData(need);

        current.required += value;

        if (isNew) {
            needs.Add(need, current);
            EventBus.instance.onNeedAdded?.Invoke(need);
        }

        RecalculateNeed(need);
    }

    public void ConsumeNeed(Need need, int value)
    {
        bool isNew = !needs.TryGetValue(need, out NeedData current);
        if (isNew) current = new NeedData(need);
        current.consumed += value;

        if (isNew) {
            needs.Add(need, current);
            EventBus.instance.onNeedAdded?.Invoke(need);
        }

        RecalculateNeed(need);
    }

    public void RecalculateNeed(Need need)
    {
        if (!needs.TryGetValue(need, out NeedData needData)) return;

        float split = needData.required - needData.consumed;

        bool wasUnhappy = needData.isUnhappy;
        bool hadTooMuch = needData.hasTooMuch;
        bool hadTooPhew = needData.hasTooPhew;

        needData.hasTooMuch = split > need.margin.max;
        needData.hasTooPhew = split < need.margin.min;

        if (hadTooMuch && needData.hasTooPhew) {
            EventBus.instance.onNeedBalanceRegained?.Invoke(need);
            EventBus.instance.onNeedBalanceLost?.Invoke(need);
        } else if (hadTooPhew && needData.hasTooMuch) {
            EventBus.instance.onNeedBalanceRegained?.Invoke(need);
            EventBus.instance.onNeedBalanceLost?.Invoke(need);
        } else if (wasUnhappy && !needData.isUnhappy) {
            EventBus.instance.onNeedBalanceRegained?.Invoke(need);
        } else if (!wasUnhappy && needData.isUnhappy) {
            EventBus.instance.onNeedBalanceLost?.Invoke(need);
        }
    }
}


public class NeedData
{
    public Need need;

    public int required;
    public int consumed;

    public bool hasTooMuch;
    public bool hasTooPhew;

    public bool isUnhappy => hasTooMuch || hasTooPhew;

    public NeedData(Need need)
    {
        this.need = need;
    }
}