using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMover : MonoBehaviour {
    public float speed = 20f;
    Rigidbody2D rb;
    bool facingRight = true;
    bool grounded;
    public GameObject MarioMini;
    public GameObject MarioBig;
    Transform sprite;
    Transform spriteMini;
    public GameObject fireball;
    public Transform Firestart;
    int hP = 2;
    public static bool MarioIsSmall = false;
    // enum animState { Idle, Run, Jump, Death,SmallMario,NormalMario }; TODO BIG Mario Small Mario

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Mario_1");
        spriteMini = transform.Find("MarioMini");
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (MarioIsSmall) {
                Big();
               
            } else {
                Small();
            }
        }
        UpdateFacing();
        Rush();
        Crouch();//TODO crouching movement 
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            Jump();
        }
        if (Input.GetKey(KeyCode.X)) {
            if (rb.velocity.x != 0f) {

                Momentum();//TODO fix the slowing
            } }
        if (Input.GetKeyDown(KeyCode.E)) {
            Shoot();
        }
    }                                    //         ---alternative movement--
    void UpdateFacing() {/* float HorizotalMov = Input.GetAxis("Horizontal");rb.velocity = new Vector2(HorizotalMov * speed, rb.velocity.y); if (HorizotalMov > 0.1f) {transform.localScale = Vector3.one;  }  if ( HorizotalMov < -0.1f) {transform.localScale = new Vector3(-1,1, 1);*/

        float HorizotalMov = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(HorizotalMov * speed, rb.velocity.y);
        if ((!facingRight && HorizotalMov < 0.0f) ||
            (facingRight && HorizotalMov > 0.0f)) {
            facingRight = !facingRight;
            transform.Rotate(0f,180f, 0f);
        }
    }
    public void Big (){
        MarioMini.SetActive(false);
        MarioBig.SetActive(true);
        MarioIsSmall = false;
       
    }
    public void Small() {
        MarioMini.SetActive(true);
        MarioBig.SetActive(false);
        MarioIsSmall = true;
    }
    void Shoot() {
         Instantiate(fireball,Firestart.position,Firestart.rotation);
       

    }
    public void Rush() {
        float HorizotalMov = Input.GetAxis("Horizontal");
       
        if (Input.GetKey(KeyCode.E)) {
           var rush = speed*2;
            rb.velocity = new Vector2(HorizotalMov * rush, rb.velocity.y);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            hP--;
        }
        if (collision.collider.CompareTag("Ground")) {
            grounded = true;
        }
    }
    void Jump() {
       
            rb.velocity = new Vector2(rb.velocity.x, speed);
            grounded = false;
        
    }
  
    void Crouch() {
        if (Input.GetKey(KeyCode.X)) {


          

        }


    }
    
     void  Momentum() {
        float HorizotalMov = Input.GetAxis("Horizontal");
        {

            print("slow down");
            rb.velocity = new Vector3(HorizotalMov * speed* 0.9f* Time.deltaTime, rb.velocity.y);
            
        }

      }


}


