
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    
    public PhoneController phoneController;
    public GameObject mainCamera;


    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


}
