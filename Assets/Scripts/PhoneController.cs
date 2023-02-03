using UnityEngine;
using TMPro;

public class PhoneController : MonoBehaviour {
    
    // Screens
    [SerializeField] private GameObject selectDroneScreen;
    [SerializeField] private GameObject manualControlScreen;
    [SerializeField] private GameObject autoPilotScreen;

    Lean.Gui.LeanButton button;

    // Statuses
    [SerializeField] private TMP_Text manualTemperature;
    [SerializeField] private TMP_Text manualHumidity;
    [SerializeField] private TMP_Text manualHealth;
    [SerializeField] private TMP_Text manualPestDensity;
    [SerializeField] private TMP_Text manualAcidity;

    [SerializeField] private TMP_Text autoTemperature;
    [SerializeField] private TMP_Text autoHumidity;
    [SerializeField] private TMP_Text autoHealth;
    [SerializeField] private TMP_Text autoPestDensity;
    [SerializeField] private TMP_Text autoAcidity;


    // The drone that it is currently controlling
    private Drone drone;


    // State for manual controlling drone
    private bool isSpinL = false;
    private bool isSpinR = false;
    private bool isForward = false;
    private bool isReverse = false;
    private bool isRaise = false;
    private bool isLand = false;



    private void Update() {
        if (isSpinL) drone?.SpinLeft();
        if (isSpinR) drone?.SpinRight();
        if (isForward) drone?.MoveForward();
        if (isReverse) drone?.MoveReverse();
        if (isRaise) drone?.Raise();
        if (isLand) drone?.Land();
    }






    //===================================
    // Screen controls
    //===================================
    public void GoToManualControl(Drone drone) {
        this.drone = drone;
        selectDroneScreen.SetActive(false);
        manualControlScreen.SetActive(true);

        GameManager.instance.mainCamera.SetActive(false);
        drone.ActivateCamera(true);
        drone.Propeller(true);
        drone.AutoPilot(false);
    }


    public void BackFromManualControl() {
        manualControlScreen.SetActive(false);
        selectDroneScreen.SetActive(true);

        GameManager.instance.mainCamera.SetActive(true);
        drone.ActivateCamera(false);
    }


    public void UpdateStatusPanel(Drone drone, CropStats stats) {
        if (drone != this.drone) return;

        if (manualControlScreen.activeSelf) {
            manualTemperature.text = stats.temp;
            manualHumidity.text = stats.humidity;
            manualHealth.text = stats.health;
            manualPestDensity.text = stats.pestDensity;
            manualAcidity.text = stats.acidity;
        }
        else if (autoPilotScreen.activeSelf) {
            autoTemperature.text = stats.temp;
            autoHumidity.text = stats.humidity;
            autoHealth.text = stats.health;
            autoPestDensity.text = stats.pestDensity;
            autoAcidity.text = stats.acidity;
        }
    }


    public void GoToAutopilot(Drone drone) {
        this.drone = drone;
        selectDroneScreen.SetActive(false);
        autoPilotScreen.SetActive(true);

        GameManager.instance.mainCamera.SetActive(false);
        drone.ActivateCamera(true);
    }


    public void BackFromAutopilot() {
        autoPilotScreen.SetActive(false);
        selectDroneScreen.SetActive(true);

        GameManager.instance.mainCamera.SetActive(true);
        drone.ActivateCamera(false);
    }



    //===================================
    // Drone autopilot controls
    //===================================
    public void Autopilot(bool start) {
        drone?.AutoPilot(start);
    }


    //===================================
    // Drone manual controls
    //===================================
    public void Spray(bool spray) {
        drone?.Spray(spray);
    }

    public void Propeller(bool start) {
        drone?.Propeller(start);
    }

    public void ManualSpinL(bool isHeld) {
        isSpinL = isHeld;
    }

    public void ManualSpinR(bool isHeld) {
        isSpinR = isHeld;
    }

    public void ManualForward(bool isHeld) {
        isForward = isHeld;
    }

    public void ManualReverse(bool isHeld) {
        isReverse = isHeld;
    }

    public void ManualRaise(bool isHeld) {
        isRaise = isHeld;
    }

    public void ManualLand(bool isHeld) {
        isLand = isHeld;
    }

}
