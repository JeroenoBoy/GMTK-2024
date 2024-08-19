using UnityEngine;


public class WinGame : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private bool _canWinGame;
    private bool _wonGame;
    private int _unbalanceCount;
    private float _winTimer = -1f;

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

    private void Update()
    {
        if (_wonGame) return;
        if (_winTimer == -1f) return;
        _winTimer -= Time.deltaTime;
        if (_winTimer > 0f) return;

        _wonGame = true;
        EventBus.instance.onGameWon?.Invoke();
    }

    private void CheckWin()
    {
        if (_wonGame) return;
        if (!_canWinGame) {
            _winTimer = -1;
            return;
        }

        if (_unbalanceCount > 0) {
            _winTimer = -1;
            return;
        }

        if (_winTimer != 1f) {
            _winTimer = .5f;
        }
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