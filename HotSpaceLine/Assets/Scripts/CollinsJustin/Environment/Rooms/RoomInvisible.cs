using UnityEngine;
using System.Collections;

public class RoomInvisible : MonoBehaviour {

    public GameObject room;
    public bool turnInvisible;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == ("InvisTrigger")) {
            IsRoomVisible();
        }
    }

    private void IsRoomVisible() {
        room.SetActive(turnInvisible);
    }
}
