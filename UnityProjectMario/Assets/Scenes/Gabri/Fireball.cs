using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{ public float speed = 20f;
    
    public GameObject  fireball ;
    public  Rigidbody2D rb;
   
         void Start()
    {
        Destroy(gameObject, 1f);
        rb.velocity =  transform.right * speed;
    }        

    
    private void OnCollisionEnter2D(Collision2D collision) {
        //Explode();

        if (collision.gameObject.tag == "Enemy") Destroy(gameObject);
        if (collision.gameObject.tag == "Wall") Destroy(gameObject);


    }

   /* void Explode() {
        var explo = Instantiate(*explosionPrefab*);
        explo.transform.position = transform.position;
    }*/
  

}

