using JUtils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class UIManager : SingletonBehaviour<UIManager>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject _gameOverScreen;

        public bool isOverUI { get; private set; }

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
        }

        private void OnDisable()
        {
            EventBus.instance.onOutOfBalance -= HandleOutOfBalance;
        }

        private void HandleOutOfBalance()
        {
            _gameOverScreen.SetActive(true);
        }
    }
}