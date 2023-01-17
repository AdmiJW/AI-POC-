
using UnityEngine;


public class Drone : MonoBehaviour {

    // Assigned chunks to go on autopilot
    [SerializeField] private Transform[] chunks;
    // What height should the drone fly?
    [SerializeField] private float setY;

    private int currentChunkIndex = 0;



    
    // To move the drone.
    private Rigidbody rb;
    // To animate the drone's propellers.
    private Animator animator;
    // To spray pesticide
    private ParticleSystem sprayer;
    // The camera below the drone
    [SerializeField] private Camera belowCam;
    // The camera following the drone
    [SerializeField] private Camera followCam;


    // Movement parameters
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotateSpeed = 1f;


    private bool isAutoPilot = false;


    protected void Awake() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        sprayer = GetComponentInChildren<ParticleSystem>();
    }


    // Update is to allow drone to move with autopilot
    protected void Update() {
        if (!isAutoPilot) return;

        Vector3 target = new Vector3(chunks[currentChunkIndex].position.x, setY, chunks[currentChunkIndex].position.z);

        // Calculate direction vector to move to the target
        Vector3 directionVector = (target - transform.position).normalized;

        // Move to the calculated position by apply force
        rb.AddForce(directionVector * 0.3f);

        // If the drone is close enough to the chunk, move to the next chunk
        if (Vector3.Distance(transform.position, target) < 0.1f)
            currentChunkIndex = (currentChunkIndex + 1) % chunks.Length;

        // Apply torque towards target direction
        Vector3 torque = Vector3.Cross(transform.forward, directionVector);
        rb.AddTorque(torque * 0.05f);
    }


    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Chunk")) return;

        CropStats stats = other.GetComponent<CropChunk>().GetCropStats();
        GameManager.instance.phoneController.UpdateStatusPanel(this, stats);

        if (isAutoPilot) {
            if (stats.health.Equals("Good")) Spray(false);
            else Spray(true);
        }
    }




    public void Propeller(bool start) {
        if (start) animator.SetTrigger("Spin");
        else animator.SetTrigger("Stop");
    }

    public void Spray(bool spray) {
        if (spray) sprayer.Play();
        else sprayer.Stop();
    }

    public void ActivateCamera(bool activate) {
        belowCam.enabled = activate;
        followCam.enabled = activate;
    }

    public void AutoPilot(bool start) {
        isAutoPilot = start;

        if (start) {
            currentChunkIndex = 0;
            Propeller(true);
        }
    }





    public void MoveForward() {
        rb.AddForce(transform.forward * speed);
    }

    public void MoveReverse() {
        rb.AddForce(-transform.forward * speed);
    }

    public void SpinLeft() {
        rb.AddTorque(-transform.up * rotateSpeed);
    }

    public void SpinRight() {
        rb.AddTorque(transform.up * rotateSpeed);
    }

    public void Raise() {
        rb.AddForce(transform.up * speed);
    }

    public void Land() {
        rb.AddForce(-transform.up * speed);
    }
}
