using UnityEngine;

/*
Author: Justin Collins
Purpose of Script: Constrains the camera to the player location.
Your player character must have the tag Player.
    */

public class CameraToPlayer : MonoBehaviour {

    public float cameraDistance = 10.0f;

    private GameObject player;
    private Vector3 applyCameraDistance;

	void Awake () {
        applyCameraDistance = new Vector3(0.0f, cameraDistance, 0.0f);
        this.gameObject.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f);
        this.gameObject.transform.position += applyCameraDistance;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        TrackPlayer();
    }

    private void TrackPlayer() {
        applyCameraDistance.Set(player.transform.position.x, cameraDistance, player.transform.position.z);
        this.gameObject.transform.position = applyCameraDistance;
    }
}
