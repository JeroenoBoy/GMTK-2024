using UnityEngine;


public class CameraMovment : MonoBehaviour
{
    [SerializeField] private float _damping;

    private BalancingBeam _balancingBeamScript;

    private void Start()
    {
        _balancingBeamScript = FindObjectOfType<BalancingBeam>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 targetpostion = new(transform.position.x, _balancingBeamScript.currentHeight, transform.position.z);

        Vector3 hans = Vector3.Lerp(transform.position, targetpostion, 1 - Mathf.Exp(-_damping * Time.deltaTime));

        transform.position = hans;
    }
}