using System.Collections.Generic;
using DefaultNamespace.UI;
using JUtils;
using UnityEngine;
using UnityEngine.InputSystem;


public class PLaceBuilding : SingletonBehaviour<PLaceBuilding>
{
    public List<GameObject> _buildingObjects = new();
    private GameObject _sellectedObject;
    public Vector3 _screenPostion;
    public Vector3 _worldPostion;
    private Plane plane = new(Vector3.forward, 0);
    private bool _kanNietPlaatssen;

    [SerializeField] private GameObject _parentobject;
    private bool _isClicked = true;

    private float _timer;

    private BoxCollider2D _bC2D;
    private PolygonCollider2D _pC2D;
    private SpriteRenderer _sR;

    public void SelectObject(GameObject obj)
    {
        _sellectedObject = obj;
        _sR.sprite = _sellectedObject.GetComponent<SpriteRenderer>().sprite;

        _bC2D.enabled = false;
        _pC2D.enabled = false;
        if (_sellectedObject.TryGetComponent(out BoxCollider2D boxCollider2D)) {
            _bC2D.enabled = true;
            _bC2D.size = boxCollider2D.size;
        } else if (_sellectedObject.TryGetComponent(out PolygonCollider2D polygonCollider2D)) {
            _pC2D.enabled = true;
            _pC2D.SetPath(0, polygonCollider2D.GetPath(0));
        }
    }

    private void Start()
    {
        _sR = GetComponent<SpriteRenderer>();
        _bC2D = GetComponent<BoxCollider2D>();
        _pC2D = GetComponent<PolygonCollider2D>();

        SelectObject(_buildingObjects[0]);
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
        if (UIManager.instance.isOverUI) {
            _sR.enabled = false;
            _isClicked = false;
            return;
        }

        _sR.enabled = true;

        _screenPostion = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(_screenPostion);

        if (plane.Raycast(ray, out float distance)) {
            _worldPostion = ray.GetPoint(distance);
        }

        transform.position = _worldPostion;
        _timer -= Time.deltaTime;
        if (_timer < 0f) {
            if (_isClicked) {
                _timer = .5f;
                if (!_kanNietPlaatssen) {
                    SoundManager.instance.Play("Place");
                    GameObject building = Instantiate(_sellectedObject, _worldPostion, Quaternion.Euler(0, 0, 0), _parentobject.transform);
                    building.AddComponent<buildingFalling>().prefab = _sellectedObject;
                    building.AddComponent<Rigidbody2D>();
                }
            }
        }

        _isClicked = false;
    }

    public void HandleMouseLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started) {
            _isClicked = true;
        }
    }
}