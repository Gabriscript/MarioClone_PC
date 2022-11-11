using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerMover : MonoBehaviour {
    public float speed = 2f;
    Rigidbody2D rb;
    bool facingRight = true;
    bool grounded;
    Transform sprite;
    public GameObject fireball;
   // enum animState { Idle, Run, Jump, Death,SmallMario,NormalMario }; TODO BIG Mario Small Mario

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Mario_1");
    }


    void Update() {

        UpdateFacing();
        Rush();
        Crouch();//TODO crouching movement 
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            Jump();
        }
        Momentum();//TODO fix the slowing
        if (Input.GetKeyDown(KeyCode.E)) {
            Shoot();
        }
    }                                    //         ---alternative movement--
    void UpdateFacing() {/* float HorizotalMov = Input.GetAxis("Horizontal");rb.velocity = new Vector2(HorizotalMov * speed, rb.velocity.y); if (HorizotalMov > 0.1f) {transform.localScale = Vector3.one;  }  if ( HorizotalMov < -0.1f) {transform.localScale = new Vector3(-1,1, 1);*/

        float HorizotalMov = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(HorizotalMov * speed, rb.velocity.y);
        if ((!facingRight && HorizotalMov < -0.1f) ||
            (facingRight && HorizotalMov > 0.1f)) {
            // now pressing in different direction than formerly facing: flip sprite & facing
            var scale = sprite.localScale;
            scale.x *= -1;
            sprite.localScale = scale;
            facingRight = !facingRight;
        }
    }
    void Shoot() {
        var fireObject = Instantiate(fireball);
        fireObject.transform.position = transform.position;

    }
    public void Rush() {
        float HorizotalMov = Input.GetAxis("Horizontal");
       
        if (Input.GetKey(KeyCode.E)) {
           var rush = speed*2;
            rb.velocity = new Vector2(HorizotalMov * rush, rb.velocity.y);

        }

    }
    void Jump() {
       
            rb.velocity = new Vector2(rb.velocity.x, speed);
            grounded = false;
        
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    void Crouch() {
        if (Input.GetKey(KeyCode.X)) {



        }


    }
     void  Momentum() {
        float HorizotalMov = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKeyDown(KeyCode.X)) {

            
            rb.velocity = new Vector3(HorizotalMov * speed*0.5f*Time.deltaTime, rb.velocity.y);
            
        }

      }


}


