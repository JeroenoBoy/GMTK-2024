using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PLaceBuilding : MonoBehaviour
{
    [SerializeField] private GameObject _placeBuilding;

    public GameObject _chosenObject;
    public Vector3 _screenPostion;
    public Vector3 _worldPostion;
    Plane plane = new Plane(Vector3.forward, 0);

    [SerializeField] private GameObject _parentobject;

    private void Update()
    {

        _screenPostion = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(_screenPostion);

        if(plane.Raycast(ray,out float distance))
        {
            _worldPostion = ray.GetPoint(distance);
        }
        // _screenPostion.z = Camera.main.nearClipPlane -5;

        //_worldPostion = Camera.main.ScreenToWorldPoint(_screenPostion);
        transform.position = _worldPostion;

        // Camera.main.ScreenToViewportPoint(Input.mousePosition)

        //Camera.main.screen
        
        
        //_placeBuilding.transform.position = _muisPostie;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void HandleMouseLeft(InputAction.CallbackContext ctx)
    {
        Instantiate(_chosenObject, _worldPostion, Quaternion.Euler(0, 0, 0), _parentobject.transform);
        
    }

}
