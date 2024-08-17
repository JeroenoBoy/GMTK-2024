using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PLaceBuilding : MonoBehaviour
{

    public GameObject _chosenObject;
    public Vector3 _screenPostion;
    public Vector3 _worldPostion;
    Plane plane = new Plane(Vector3.forward, 0);
    private int _kanNietPlaatsen = 0;

    [SerializeField] private GameObject _parentobject;
    private bool _isClicked = true;

    private float _timer;

    private void Update()
    {

        _screenPostion = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(_screenPostion);

        if(plane.Raycast(ray,out float distance))
        {
            _worldPostion = ray.GetPoint(distance);
        }

        transform.position = _worldPostion;
        _timer -= Time.deltaTime;
        if(_timer < 0f)
        {
            if(_isClicked == true)
            {
                _isClicked = false;
                _timer = 2f;
                if (_kanNietPlaatsen == 0 )
                {
                    GameObject building = Instantiate(_chosenObject, _worldPostion, Quaternion.Euler(0, 0, 0), _parentobject.transform);
                    building.AddComponent<buildingFalling>();
                    building.AddComponent<Rigidbody2D>();
                   
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {   
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        _kanNietPlaatsen ++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _kanNietPlaatsen--;
        if (_kanNietPlaatsen == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }



    public void HandleMouseLeft(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Started)
        {
            _isClicked = true;
        }
    }
}
