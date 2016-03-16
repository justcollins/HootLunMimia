using UnityEngine;

/*
Author: Justin Collins
Purpose of Script: Moves the player around the map and turns the player to always face the cursor position.
This script requires to have a MainCamera tag in the scene on your MainCamera and the player must have a rigidbody.
There must also be a Ground layer and have the ground in the scene be on that layer.
When setting up, please make sure to lock the X and Z rotation constraints on the player.
    */

[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public float playerSpeed = 10.0f;
    public float turnSensitivityX = 15.0f;
    public float turnSensitivityZ = 15.0f;
    public int sprintSpeedMultiplier = 2;

    private new Rigidbody rigidbody;
    private Animator animator;
    private Vector3 movement;
    private int groundMask;
    private float camRayLength;
    private bool isSprinting = false;

	void Awake () {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        groundMask = LayerMask.GetMask("Ground");
	}

    void Start() {
        camRayLength = 1000000;
    }
	
	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift)) {
            isSprinting = true;
        } else {
            isSprinting = false;
        }

        MovementManger(h, v);
        turning();
    }

    private void MovementManger(float horizontalMove, float verticalMove) {
        movement.Set(horizontalMove, 0.0f, verticalMove);
        if(!isSprinting) {
            movement = movement.normalized * playerSpeed * Time.deltaTime;
        } else {
            movement = movement.normalized * Sprint(playerSpeed) * Time.deltaTime;
        }
        rigidbody.MovePosition(this.transform.position + movement);
    }

    private void turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, groundMask)){
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rigidbody.MoveRotation(newRotation);
        }
    }

    private float Sprint(float currentSpeed) {
        return currentSpeed * sprintSpeedMultiplier;
    }
}
