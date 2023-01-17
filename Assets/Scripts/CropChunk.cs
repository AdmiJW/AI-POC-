
using UnityEngine;

public class CropChunk : MonoBehaviour {
    [SerializeField] private CropStats cropStats;

    public CropStats GetCropStats() {
        return cropStats;
    }
}
