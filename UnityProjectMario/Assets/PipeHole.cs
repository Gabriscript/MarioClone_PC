using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHole : MonoBehaviour {
    public bool frogNear;
    public Transform pipePairing;
    Transform player;

    private void Start() {
        player = GameObject.Find("Main Camera").GetComponent<cameraFollow>().player;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S) && frogNear) {
            player.position = pipePairing.position;
        }
    }

    void OnTriggerEnter2D(Collider2D player) {

        if (player.gameObject.layer == 3) {
            frogNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D player) {
        if (player.gameObject.layer == 3) {
            frogNear = false;
        }

    }
}

