using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class activatecheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameObject check;
 
   

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
