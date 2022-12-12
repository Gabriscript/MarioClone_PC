using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomUP : MonoBehaviour {
   
    PlayerMover playerMover;
    private void Start() {
         playerMover = FindObjectOfType<PlayerMover>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) {        
                                   
                if( playerMover.hits >= 2)
                    { playerMover.hits += 0; }
                else { 
                playerMover.hits = 2; }

                Destroy(gameObject);
            }
        }
    }

