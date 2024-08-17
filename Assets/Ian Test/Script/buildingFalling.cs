using UnityEngine;


public class buildingFalling : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1;

    public new Collider2D collider { get; private set; }

    private Rigidbody2D _RB2D;
    private float _time;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        _RB2D = GetComponent<Rigidbody2D>();
        _time = _maxTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_RB2D.velocity.magnitude < 0.2f) {
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