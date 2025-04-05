using UnityEngine;

[CreateAssetMenu(fileName = "New Description", menuName = "Descriptions/Grave", order = 0)]
public class GraveDescription : ScriptableObject {
    [TextArea(3, 5)] public string[] descriptionLines = new string[3];
    public uint weight;
}