using UnityEngine;

/*
Author: Justin Collins
Purpose of Script: Constrains the camera to the player location.
Your player character must have the tag Player.
    */

public class CameraToPlayer : MonoBehaviour {

    public float cameraDistanceX = 0.0f;
    public float cameraDistanceY = 10.0f;
    public float cameraDistanceZ = 10.0f;
    public float cameraRotationX = 0.0f;
    public float cameraRotationY = 0.0f;
    public float cameraRotationZ = 0.0f;
    public int screenBoundary = 50;
    public float scrollSpeed = 5;

    private GameObject player;
    private float currentCamDisX;
    private float currentCamDisY;
    private float currentCamDisZ;
    private float camMouseMoveX;
    private float camMouseMoveY;
    private int screenWidth;
    private int screenHeight;
    private Vector3 applyCameraDistance;
    private Vector3 applyCameraRotation;

	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        currentCamDisX = cameraDistanceX;
        currentCamDisY = cameraDistanceY;
        currentCamDisZ = cameraDistanceZ;
	}

    void Start() {
        applyCameraDistance = new Vector3(player.transform.position.x + currentCamDisX, player.transform.position.y + currentCamDisY, player.transform.position.z - currentCamDisZ);
        applyCameraRotation = new Vector3(cameraRotationX, cameraRotationY, cameraRotationZ);
        this.gameObject.transform.rotation = Quaternion.Euler(applyCameraRotation);
        this.gameObject.transform.position += applyCameraDistance;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }
	
	void Update () {
        if(Input.GetKey(KeyCode.Space)) {
            MouseScreenMovement();
        } else {
            camMouseMoveX = 0;
            camMouseMoveY = 0;
            TrackPlayer();
        }
    }

    private void TrackPlayer() {
        applyCameraDistance.Set(player.transform.position.x + currentCamDisX, player.transform.position.y + currentCamDisY, player.transform.position.z - currentCamDisZ);
        this.gameObject.transform.position = applyCameraDistance;
    }

    private void MouseScreenMovement() {
        camMouseMoveX = 0.0f;
        camMouseMoveY = 0.0f;
        if (Input.mousePosition.x > screenWidth - screenBoundary) {
            camMouseMoveX += scrollSpeed * Time.deltaTime;
            //this.gameObject.transform.position.x += scrollSpeed * Time.deltaTime;
        } else if (Input.mousePosition.x < 0 + screenBoundary) {
            camMouseMoveX -= scrollSpeed * Time.deltaTime;
            //transform.position.x -= scrollSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y > screenHeight - screenBoundary) {
            camMouseMoveY += scrollSpeed * Time.deltaTime;
            //transform.position.z += scrollSpeed * Time.deltaTime;
        } else if (Input.mousePosition.y < 0 + screenBoundary) {
            camMouseMoveY -= scrollSpeed * Time.deltaTime;
            //transform.position.z -= scrollSpeed * Time.deltaTime;
        }

        applyCameraDistance.Set(this.gameObject.transform.position.x + camMouseMoveX, this.gameObject.transform.position.y, this.gameObject.transform.position.z + camMouseMoveY);
        this.gameObject.transform.position = applyCameraDistance;
    }
}
