using JUtils;
using UnityEngine;


[CreateAssetMenu]
public class Need : ScriptableObject
{
    [field: SerializeField] public new string name { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
    [field: SerializeField] public MinMax percentage { get; private set; }
}