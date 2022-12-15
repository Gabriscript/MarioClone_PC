using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickkiller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Brick") {
            Destroy(collision.gameObject);
           

        }
    }
}
