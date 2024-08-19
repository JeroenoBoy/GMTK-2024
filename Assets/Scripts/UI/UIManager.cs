using JUtils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class UIManager : SingletonBehaviour<UIManager>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _gameWonScreen;

        public bool isOverUI { get; private set; }

        private bool _gameWon;
        private bool _gameLost;

        public void OnPointerEnter(PointerEventData eventData)
        {
            isOverUI = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isOverUI = false;
        }

        private void OnEnable()
        {
            EventBus.instance.onOutOfBalance += HandleOutOfBalance;
            EventBus.instance.onGameWon += HandleGameWon;
        }

        private void OnDisable()
        {
            EventBus.instance.onOutOfBalance -= HandleOutOfBalance;
            EventBus.instance.onGameWon -= HandleGameWon;
        }

        private void HandleOutOfBalance()
        {
            if (_gameWon) return;
            _gameLost = true;
            _gameOverScreen.SetActive(true);
        }

        private void HandleGameWon()
        {
            if (_gameLost) return;
            _gameWon = true;
            _gameWonScreen.SetActive(true);
        }
    }
}