using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector2 velocity;

    public PlayerMover pm;
    public GameObject  fireball ;
    public  Rigidbody2D rb;
    
    float ticktime = 5;
    float timer = 0;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
      
        rb.velocity = velocity;
    }
    void Update() {
       if (rb.velocity.y < velocity.y)
            rb.velocity = velocity;


        timer += Time.deltaTime;
        if (timer > ticktime) {
            Destroy(gameObject);
            pm.fireList.Remove(gameObject);
            timer = 0;
        }
       
        
        
    }

   
    private void OnCollisionEnter2D(Collision2D collision) {
        //Explode();
       rb.velocity = new Vector2(velocity.x, -velocity.y);

        if (collision.gameObject.tag == "Enemy") { Destroy(collision.gameObject); Destroy(gameObject); pm.fireList.Remove(gameObject);Destroy(collision.gameObject); }
      //  if (collision.gameObject.tag == "Wall") { Destroy(gameObject); pm.fireList.Remove(gameObject); }
     if (collision.contacts[0].normal.x != 0) {
            pm.fireList.Remove(gameObject);
           Destroy(gameObject);
       }

    }
   
    /* void Explode() {
         var explo = Instantiate(*explosionPrefab*);
         explo.transform.position = transform.position;
     }*/


}

