using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerUP : MonoBehaviour {
    PlayerMover playerMover;
    private void Start() {
        playerMover = FindObjectOfType<PlayerMover>();
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) {
            
                       
                playerMover.hits = 3;
                Destroy(gameObject);
            
        }
    }
}
