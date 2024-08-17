using UnityEngine;


public class buildingFalling : MonoBehaviour
{
    [SerializeField] private float _maxTime = 2f;

    public new Collider2D collider { get; private set; }

    private Rigidbody2D _RB2D;
    private Weightable _weight;
    private BalancingContainer _parentContainer;

    private float _time;
    private int _collisionCount;

    private void Awake()
    {
        _weight = GetComponent<Weightable>();
        _parentContainer = GetComponentInParent<BalancingContainer>();
    }

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        _RB2D = GetComponent<Rigidbody2D>();
        _time = _maxTime;
    }

    private void Update()
    {
        if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, 0)) > 45) {
            collider.enabled = false;
        }

        if (transform.position.y < -10) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_collisionCount == 0) {
            _parentContainer.currentWeight += _weight.weight;
        }

        _collisionCount++;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _collisionCount--;

        if (_collisionCount == 0) {
            _parentContainer.currentWeight -= _weight.weight;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_RB2D.velocity.magnitude < 0.1f) {
            _time -= Time.deltaTime;
            if (_time < 0f) {
                Destroy(_RB2D);
                Destroy(this);
                EventBus.instance.onBuildingSettle?.Invoke(this);
            }
        } else {
            _time = _maxTime;
        }
    }
}