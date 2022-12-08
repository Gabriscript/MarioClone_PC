using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class PlayerMover : MonoBehaviour {
   public  GameManager gm;
    Rigidbody2D rb; 
    [Header("Move info")]
    public float speed = 20f;
    bool wallhangPressed = false;
    bool jumpPressed = false;
    bool momentumPressed = false;
    bool facingRight = false;
    bool grounded ;
    bool canMove = true;
    bool onWall = false;
    bool canWallJump = true;
    bool buttSlumpPressed = false;   
    [SerializeField] Vector2 wallJumpDirection;
    [Range(1, 10)] public float jumpVelocity;
    SpriteRenderer bigSprite;
    SpriteRenderer smallSprite;
    SpriteRenderer FireSprite;
    

    [Header("change status")]
    public GameObject MarioMini;
    public GameObject MarioBig;
    public GameObject FireMan;
    Transform sprite;
    Transform spriteMini;
    public GameObject fireball;
    public Transform Firestart;
    public Animator anim;
    PowerUpType currentState = PowerUpType.None;


    [Header("Collision info")]   
    public LayerMask Platform;
    public LayerMask Wall;
    public Transform groundCheckPoint;   
    public static bool MarioIsSmall = false;
    public Transform wallGrabPoint;
    public float grabDistance = 1f;
   
   
   
    // enum animState { Idle, Run, Jump, Death,SmallMario,NormalMario }; TODO BIG Mario Small Mario

    void Awake() {
        FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        bigSprite = transform.Find("Mario_1").GetComponent<SpriteRenderer>();
        smallSprite = transform.Find("MarioMini").GetComponent<SpriteRenderer>();
        FireSprite = transform.Find("FireMan").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (jumpPressed) {
            Jump();
            jumpPressed = false;
           
        }
      
        if (buttSlumpPressed) {
            Buttslump();
            buttSlumpPressed = false;
        }
        /*if (momentumPressed) {
            Momentum();
            momentumPressed = false;
        }*/
        if (wallhangPressed) {
            WallHanging();
            wallhangPressed = false;

        }
        if (canMove) {
            Move();
        }

    }


    void Update() {

        

        CollisionCheck();
        //Abl to shoot
        if (gm.hits == 3) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (fireList.Count < 2) Shoot();
            }
        }
       
      
        Rush();
        if (Input.GetKeyDown(KeyCode.X)) { if (grounded == false) { buttSlumpPressed = true; } }
        if (Input.GetKeyDown(KeyCode.Space)) { jumpPressed = true;
          
        }
        if (Input.GetKeyDown(KeyCode.X)) { //if (rb.velocity.x != 0f)
            Momentum();
                //momentumPressed = true;
                }
        //CHECK TO BE ABLE TO WALLJUMP
        if (onWall == true && grounded == false) {
            if (facingRight && Input.GetAxisRaw("Horizontal") > 0 || (!facingRight && Input.GetAxisRaw("Horizontal") < 0)) {

                wallhangPressed = true;
            }
        }
        //tO MOVE AFTER WALLJUMP
        if (grounded)
            canMove = true;
      

        if (gm.hits <= 0) {
            //Die();
            gm.lives--;
        }

        /* anim.SetBool("", buttSlumpPressed);
         anim.SetBool("", jumpPressed);
         anim.SetBool("", wallhangPressed);*/



        if (Input.GetAxis("Horizontal")< -0.1f) {
            facingRight = false; }
        else if (Input.GetAxis("Horizontal") > 0.1f) {
            facingRight = true;
        }


        wallGrabPoint.localPosition = Vector3.right * grabDistance * (facingRight ? 1 : -1); 


        bigSprite.flipX = facingRight;
        smallSprite.flipX = facingRight;
    }
  
    private void CollisionCheck() {
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, Platform);
        onWall = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, Wall);
    }
    private void Move() {      
           
            float HorizotalMov = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(HorizotalMov * speed, rb.velocity.y);
      
      
    }
   
    public void Big() {
        MarioMini.SetActive(false);
        MarioBig.SetActive(true);
        FireMan.SetActive(false);
        MarioIsSmall = false;

    }
    public void Small() {
        MarioMini.SetActive(true);
        MarioBig.SetActive(false);
        FireMan.SetActive(false);
        MarioIsSmall = true;
    }
    public void FireMario() {
        MarioBig.SetActive(false);
        MarioMini.SetActive(false);
        FireMan.SetActive(true);
        MarioIsSmall = false;
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
      

        if (wallhangPressed) {
            {
                
            rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * 20 , 20);

                canMove = false;  
            
            }
        }

        else if (grounded ) {
                    

            rb.AddForce(Vector2.up * jumpVelocity,ForceMode2D.Impulse);
            grounded = false;
        }

    }

    public void Buttslump() {


        rb.velocity = Vector2.down * speed;

    }

    public void Momentum() {
        if (canMove ) {
            float HorizotalMov = Input.GetAxis("Horizontal");

            print("slow down");

            
            rb.velocity = new Vector3(HorizotalMov  * 0.3f * Time.deltaTime, rb.velocity.y);

        }
    }
    public void ActivatePowerUp(PowerUpType powerup) {

        SetMarioState(powerup);
        
    }
    void SetMarioState(PowerUpType Newstate) {


        if(gm.hits == 1) {
             Small(); 
        }

        if(gm.hits == 2) {
            Big();
        }
        if (gm.hits == 3) {
            FireMario();
        }



        currentState = Newstate;
    }
}


