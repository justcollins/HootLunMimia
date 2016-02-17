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

    public float playerSpeed = 10;

    private new Rigidbody rigidbody;
    private new GameObject camera;
    private Vector3 movement;
    private int groundMask;
    private float camRayLength;

	void Awake () {
        rigidbody = GetComponent<Rigidbody>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        groundMask = LayerMask.GetMask("Ground");
	}

    void Start() {
        camRayLength = camera.transform.position.y + 1;
    }
	
	void FixedUpdate () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        MovementManger(h, v);
        turning();
    }

    void MovementManger(float horizontalMove, float verticalMove) {
        movement.Set(horizontalMove, 0.0f, verticalMove);
        movement = movement.normalized * playerSpeed * Time.deltaTime;
        rigidbody.MovePosition(this.transform.position + movement);
    }

    void turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, groundMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            rigidbody.MoveRotation(newRotation);
        }
    }
}
