using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomUP : MonoBehaviour {
    float movementSpeed;
    Rigidbody2D rb;
    public float dirX = -1f;
    private void Start() {
        movementSpeed = 2.5f;
        rb.velocity = new Vector2(movementSpeed , rb.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Wall") {
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameObject player = collision.gameObject;
            PlayerMover playerMover = player.GetComponent<PlayerMover>();

            if (playerMover) {
                if (playerMover.hits >= 2)
                    { playerMover.hits += 0; }
                else { playerMover.hits = 2; }

                Destroy(gameObject);
            }
        }
    }
}
