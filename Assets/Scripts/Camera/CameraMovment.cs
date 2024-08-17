using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    private BalancingBeam _balancingBeamScript;

    void Start()
    {
        _balancingBeamScript = FindObjectOfType<BalancingBeam>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetpostion = new Vector3(transform.position.x, _balancingBeamScript.currentHeight, transform.position.z);

        Vector3 hans = Vector3.Lerp(transform.position, targetpostion, 1 - Mathf.Exp(-3 * Time.deltaTime));

        transform.position = hans;
    }
}
