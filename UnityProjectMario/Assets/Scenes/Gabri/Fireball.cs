using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{ public float speed = 20f;      
    public  PlayerMover pm;
    public GameObject  fireball ;
    public  Rigidbody2D rb;
    
    float ticktime = 5;
    float timer = 0;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    void Update() {
       /*if(rb.velocity.y < velocity.y) {
            rb.velocity = velocity;
        }*/

        
        timer += Time.deltaTime;
        if (timer > ticktime) {
            Destroy(gameObject);
            pm.fireList.Remove(gameObject);
            timer = 0;
        }
       
        
        //  Vector2 mov = new Vector2(speed, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown);
        
    }

   
    private void OnCollisionEnter2D(Collision2D collision) {
        //Explode();
       
        if (collision.gameObject.tag == "Enemy") { Destroy(gameObject); pm.fireList.Remove(gameObject);Destroy(collision.gameObject); }
        if (collision.gameObject.tag == "Wall") { Destroy(gameObject); pm.fireList.Remove(gameObject); }
      //  if (collision.contacts[0].normal.x != 0) {
        //    Destroy(gameObject);
      //  }

    }
   
    /* void Explode() {
         var explo = Instantiate(*explosionPrefab*);
         explo.transform.position = transform.position;
     }*/


}

