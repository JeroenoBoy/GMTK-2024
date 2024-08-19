using UnityEngine;


public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private bool _canWinGame;
    private bool _wonGame;
    private int _unbalanceCount;

    private void OnEnable()
    {
        EventBus.instance.onNeedBalanceLost += HandleNeedUnbalanced;
        EventBus.instance.onNeedBalanceRegained += HandleNeedBalanceRegained;
        EventBus.instance.onBuildingSettle += HandleBuildingSettle;
    }

    private void OnDisable()
    {
        EventBus.instance.onNeedBalanceLost -= HandleNeedUnbalanced;
        EventBus.instance.onNeedBalanceRegained -= HandleNeedBalanceRegained;
        EventBus.instance.onBuildingSettle -= HandleBuildingSettle;
    }

    private void CheckWin()
    {
        if (_wonGame) return;
        if (!_canWinGame) return;
        if (_unbalanceCount == 0) return;
        _wonGame = true;
        EventBus.instance.onGameWon?.Invoke();
    }

    private void HandleBuildingSettle(buildingFalling buildingFalling)
    {
        if (buildingFalling.prefab != _prefab) return;
        _canWinGame = true;
        CheckWin();
    }

    private void HandleNeedUnbalanced(Need need)
    {
        _unbalanceCount++;
        CheckWin();
    }

    private void HandleNeedBalanceRegained(Need need)
    {
        _unbalanceCount--;
        CheckWin();
    }
}