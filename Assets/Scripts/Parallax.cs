using UnityEngine;


public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _multi = 0.4f;

    [Header("Debug")]
    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _originalPos;
    [SerializeField] private Vector2 _centerPos;

    private void OnValidate()
    {
        if (_multi is > 1 or < -1) _multi = Mathf.Clamp(_multi, -1, 1);
    }

    private void Start()
    {
        if (!_target) _target = Camera.main.transform;

        _originalPos = transform.position;
        _offset = _originalPos - (Vector2)_target.position;
        _centerPos = _originalPos - _offset * _multi;

        // _parentPos    = parent.FuturePosition;
        // _parentOffset = parent.transform.position - pos;
        // _targetOffset = (pos - _target.position) / (1-_multi);
        // _originalPos  = _parentPos + _parentOffset - _targetOffset;
    }

    private void Update()
    {
        transform.position = (Vector2)_target.position * _multi + _centerPos;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) Start();

        Bounds bounds = GetComponent<SpriteRenderer>().bounds;

        Gizmos.DrawWireSphere(_originalPos, bounds.size.x * .5f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_centerPos, bounds.size.x * .5f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_originalPos, bounds.size.x / (1 - _multi) / 1.5f);
    }
}