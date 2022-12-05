using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollider : MonoBehaviour
{
    public bool frogNear;
    public LayerMask frog;

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
