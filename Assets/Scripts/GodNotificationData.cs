using UnityEngine;


[CreateAssetMenu]
public class GodNotificationData : ScriptableObject
{
    [field: SerializeField] public string message { get; private set; }
    [field: SerializeField] public float stayTime { get; private set; } = 5f;
}