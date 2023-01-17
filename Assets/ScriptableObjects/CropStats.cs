
using UnityEngine;

[CreateAssetMenu(fileName = "CropStats", menuName = "ScriptableObjects/CropStats", order = 1)]
public class CropStats : ScriptableObject {
    public string temp;
    public string humidity;
    public string health;
    public string pestDensity;
    public string acidity;
}
