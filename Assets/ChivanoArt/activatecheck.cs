using UnityEngine;
using UnityEngine.EventSystems;


public class activatecheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private GameObject check;

    public void OnPointerClick(PointerEventData eventData)
    {
        check.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        check.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        check.SetActive(false);
    }
}