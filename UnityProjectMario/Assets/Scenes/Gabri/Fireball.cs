using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{ public float speed = 5;
    CircleCollider2D circleColli;
    public GameObject  fireball ;
    // Start is called before the first frame update
    void Start()
    {
        circleColli = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movement = speed * Time.deltaTime;
        transform.position = new Vector3(movement, 0, 0);
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        //Explode();
        if (collision.gameObject.tag == "Enemy")
               Destroy(gameObject);
    }

   /* void Explode() {
        var explo = Instantiate(*explosionPrefab*);
        explo.transform.position = transform.position;
    }*/
}
