using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingFalling : MonoBehaviour
{
    private Rigidbody2D _RB2D;
    [SerializeField] private float _maxTime = 3;
    private float _time;
    
    void Start()
    {
        _RB2D = gameObject.GetComponent<Rigidbody2D>();
        _time = _maxTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(_RB2D.velocity.magnitude < 0.2f)
        {
            _time -= Time.deltaTime;
            if(_time <0f)
            {
                Destroy(_RB2D);
                Destroy(this);
            }
        }
        else
        {
            _time = _maxTime;
        }
    }
}
