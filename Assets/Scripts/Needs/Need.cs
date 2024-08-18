using JUtils;
using UnityEngine;


[CreateAssetMenu]
public class Need : ScriptableObject
{
    [field: SerializeField] public new string name { get; private set; }
    [field: SerializeField] public Color color { get; private set; }
    [field: SerializeField] public Sprite icon { get; private set; }
    [field: SerializeField] public MinMax margin { get; private set; }
    [field: SerializeField] public string tooLow { get; private set; }
    [field: SerializeField] public string tooHigh { get; private set; }
}