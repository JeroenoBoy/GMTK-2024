using JUtils;
using UnityEngine.EventSystems;

namespace DefaultNamespace.UI
{
    public class UIManager : SingletonBehaviour<UIManager>, IPointerEnterHandler, IPointerExitHandler
    {
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