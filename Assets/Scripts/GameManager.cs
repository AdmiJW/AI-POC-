
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    
    public PhoneController phoneController;
    public Camera mainCamera;


    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


}
