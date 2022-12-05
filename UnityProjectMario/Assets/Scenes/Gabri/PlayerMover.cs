using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class PlayerMover : MonoBehaviour {
    public float speed = 20f;
    Rigidbody2D rb;
    bool wallhangPressed = false;
     bool jumpPressed = false;
    bool momentumPressed = false;
    bool facingRight = true;
    bool grounded;
    public GameObject MarioMini;
    public GameObject MarioBig;
    Transform sprite;
    Transform spriteMini;
    public GameObject fireball;
    public Transform Firestart;
    public LayerMask Platform;
    public LayerMask Wall;
    public Transform groundCheckPoint;
    int hP = 2;
    public static bool MarioIsSmall = false;
    bool onWall = false;
    bool buttSlumpPressed = false;
    public Transform wallGrabPoint;
    public Animator anim;


    // enum animState { Idle, Run, Jump, Death,SmallMario,NormalMario }; TODO BIG Mario Small Mario

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = transform.Find("Mario_1");
        spriteMini = transform.Find("MarioMini");
      
    }

    private void FixedUpdate() {
        if (jumpPressed) {
            Jump();
            jumpPressed = false;
            
        }
        //if (jumpwallPressed && wallhangPressed) {
        //    WallJumpToRight();
        //    jumpwallPressed = false;
        //}

        //if (jumpwallPressed && wallhangPressed) {
        //    WallJumpToLeft();
        //    jumpwallPressed = false;

        //}
        if (buttSlumpPressed) {
            Buttslump();
            buttSlumpPressed = false;
        }
        if (momentumPressed) {
            Momentum();
            momentumPressed = false;
        }
        if (wallhangPressed) {
            WallHanging();
            wallhangPressed = false;

        }


    }


    void Update() {
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, Platform);
       onWall = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, Wall);


        if (Input.GetKeyDown(KeyCode.E)) { if (fireList.Count < 2) Shoot();}
        if (Input.GetKeyDown(KeyCode.P)) { if (MarioIsSmall) { Big(); } else { Small(); } }
        UpdateFacing();
        Rush();
        if (Input.GetKeyDown(KeyCode.X)) { if (grounded == false) { buttSlumpPressed = true; } }
        if (Input.GetKeyDown(KeyCode.Space)) {  jumpPressed = true; }
        if (Input.GetKey(KeyCode.X)) { if (rb.velocity.x != 0f) { momentumPressed = true; } }
        //CHECK TO BE ABLE TO WALLJUMP
            if (onWall == true && grounded == false ) {
               if (!facingRight && Input.GetAxisRaw("Horizontal") > 0 || (facingRight && Input.GetAxisRaw("Horizontal") < 0)) {

                wallhangPressed = true;
                }
            }       
          
            if (hP == 0) {
                //Die();
            }

       /* anim.SetBool("", buttSlumpPressed);
        anim.SetBool("", jumpPressed);
        anim.SetBool("", wallhangPressed);*/
        

    }                                  
    public void UpdateFacing() {

        float HorizotalMov = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(HorizotalMov * speed, rb.velocity.y);
        if ((!facingRight && HorizotalMov < 0.0f) ||
            (facingRight && HorizotalMov > 0.0f)) {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }

    }
    /*  rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
      if (rb.velocity.x > 0) { transform.localScale =  new Vector3(-1,2,1); }
      else if (rb.velocity.x<0) { transform.localScale = new Vector3(1, 2, 1); }
  }*/
    public void Big() {
        MarioMini.SetActive(false);
        MarioBig.SetActive(true);
        MarioIsSmall = false;

    }
    public void Small() {
        MarioMini.SetActive(true);
        MarioBig.SetActive(false);
        MarioIsSmall = true;
    }

    public List<GameObject> fireList = new List<GameObject>();


    public void Shoot() {
        var fireB = Instantiate(fireball, Firestart.position, Firestart.rotation);
        fireB.GetComponent<Fireball>().pm = this;
        fireList.Add(fireB);

    }

    public void Rush() {
        float HorizotalMov = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.E)) {
            var rush = speed * 2;
            rb.velocity = new Vector2(HorizotalMov * rush, rb.velocity.y);

        }

    }
     
    public void WallHanging() {

        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);

    }
      
    public void Jump() {
        /*if (wallhangPressed == true) {
              Quaternion rotateCounterClockwise = Quaternion.Euler(0, 0,135);
             //rb.velocity = rotateCounterClockwise * Vector2.right*speed;
             // transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
             // rb.velocity = new Vector2(rb.velocity.x, speed);

              rb.velocity = rotateCounterClockwise * Vector2.right * speed;


              // jumpwallPressed = false;
          } 
          if (wallhangPressed == true ) {
              Quaternion rotateClockwise = Quaternion.Euler(0, 0, -135);
             rb.velocity = rotateClockwise * Vector2.left*speed;
              */

        // transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));


        //  jumpwallPressed = false;

        //}
        // if we can walljump:
        //   walljump

        if (wallhangPressed) {
            {
               
                rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * 750, 25);
               
            }
        }

        else if (grounded) {

            rb.velocity = new Vector2(rb.velocity.x, speed);
            grounded = false;
        }

    }

    public void Buttslump() {


        rb.velocity = Vector2.down * speed;

    }

    public void Momentum() {
       // float HorizotalMov = Input.GetAxis("Horizontal");

        print("slow down");
        //rb.gravityScale = 20 * Time.deltaTime;
        rb.velocity -= 0.1f * rb.velocity;
        // speed -=  * Time.deltaTime;

        //rb.velocity= new Vector3(HorizotalMov * speed * 0.4f * Time.deltaTime, rb.velocity.y);

    }
    
    
}


