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
    }
}