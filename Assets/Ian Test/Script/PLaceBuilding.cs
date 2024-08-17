using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PLaceBuilding : MonoBehaviour
{
    public List<GameObject> _buildingObjects = new();
    private GameObject _sellectedObject;
    public Vector3 _screenPostion;
    public Vector3 _worldPostion;
    private Plane plane = new(Vector3.forward, 0);
    private bool _kanNietPlaatssen = false;

    [SerializeField] private GameObject _parentobject;
    private bool _isClicked = true;

    private float _timer;

    private BoxCollider2D _bC2D;
    private SpriteRenderer _sR;

    private void Start()
    {
        _sR = GetComponent<SpriteRenderer>();
        _bC2D = GetComponent<BoxCollider2D>();

        _sellectedObject = _buildingObjects[0];
        _sR.sprite = _sellectedObject.GetComponent<SpriteRenderer>().sprite;
        _bC2D.size = _sellectedObject.GetComponent<BoxCollider2D>().size;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_kanNietPlaatssen) {
            _sR.color = Color.Lerp(Color.white, Color.red, .5f);
        }

        _kanNietPlaatssen = true;
    }

    private void FixedUpdate()
    {
        if (!_kanNietPlaatssen) {
            _sR.color = Color.white;
        } else {
            _kanNietPlaatssen = false;
        }
    }

    private void Update()
    {
        _screenPostion = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(_screenPostion);

        if (plane.Raycast(ray, out float distance)) {
            _worldPostion = ray.GetPoint(distance);
        }

        ChangeObject();

        transform.position = _worldPostion;
        _timer -= Time.deltaTime;
        if (_timer < 0f) {
            if (_isClicked == true) {
                _isClicked = false;
                _timer = .5f;
                if (!_kanNietPlaatssen) {
                    GameObject building = Instantiate(_sellectedObject, _worldPostion, Quaternion.Euler(0, 0, 0), _parentobject.transform);
                    building.AddComponent<buildingFalling>();
                    building.AddComponent<Rigidbody2D>();
                }
            }
        }
    }

    private void ChangeObject()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            _sellectedObject = _buildingObjects[0];
            _sR.sprite = _sellectedObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().size = _sellectedObject.GetComponent<BoxCollider2D>().size;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            _sellectedObject = _buildingObjects[1];
            _sR.sprite = _sellectedObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().size = _sellectedObject.GetComponent<BoxCollider2D>().size;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            _sellectedObject = _buildingObjects[2];
            _sR.sprite = _sellectedObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<BoxCollider2D>().size = _sellectedObject.GetComponent<BoxCollider2D>().size;
        }
    }

    public void HandleMouseLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) {
            _isClicked = true;
        }
    }
}