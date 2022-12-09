using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollider : MonoBehaviour
{
    public bool frogNear;
    public LayerMask frog;

    void OnTriggerEnter2D(Collider2D player) {
        
        //if (player.gameObject.layer == 3) {
            if (player.gameObject.name == "DummyPlayer") {
            frogNear = true;
        }
     }

    void OnTriggerExit2D(Collider2D player) {
            if (player.gameObject.name == "DummyPlayer") {

            //if (player.gameObject.layer == 3) {
            frogNear = false;
        }

    }
}
