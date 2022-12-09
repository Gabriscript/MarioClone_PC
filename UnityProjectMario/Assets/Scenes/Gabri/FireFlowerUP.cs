using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlowerUP : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            GameObject player = collision.gameObject;
            PlayerMover playerMover = player.GetComponent<PlayerMover>();

            if (playerMover) {
                playerMover.hits = 3;
                Destroy(gameObject);
            }
        }
    }
}
