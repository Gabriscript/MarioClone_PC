using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempImmunity : MonoBehaviour {
    public SpriteRenderer[] rend;
    Color c;
    void Start() {
        rend = GetComponentsInChildren<SpriteRenderer>();



    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject player = collision.gameObject;
        PlayerMover playerMover = player.GetComponent<PlayerMover>();
        if (collision.gameObject.name.Equals("Enemy") && playerMover.hits > 0) {
           {
                StartCoroutine(GetInvulnerable());                             //  StartCoroutine("GetInvulnerable");
            }


        }


        IEnumerator GetInvulnerable() {
            // c = rend.material.color;
            print("WTF");

            Physics2D.IgnoreLayerCollision(3, 7, true);
            foreach (SpriteRenderer r in rend) {
                r.color = Color.red;
            }
            yield return new WaitForSeconds(3f);
            foreach (SpriteRenderer r in rend) {
                r.color = Color.white;
            }
            Physics2D.IgnoreLayerCollision(3, 7, false);


        }
    }
}