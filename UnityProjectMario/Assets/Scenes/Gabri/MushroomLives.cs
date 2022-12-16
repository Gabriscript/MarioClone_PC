using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomLives : MonoBehaviour {
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) {

           gm.lives++;
            gm.UpdateLivesText();
            Destroy(gameObject);
        }

          
    }
}

