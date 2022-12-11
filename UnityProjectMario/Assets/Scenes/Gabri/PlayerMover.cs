using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class PlayerMover : MonoBehaviour {

   // [SerializeField] List<PowerUpType> marioPowerups;//addes this
    public  int hits = 2;// added this
   // public SpriteRenderer[] rend;
   // Color c;
    GameManager gm;
    Rigidbody2D rb; 
    [Header("Move info")]
    public float speed = 20f;
    bool wallhangPressed = false;
    bool jumpPressed = false;
    bool momentumPressed = false;
    bool facingRight = true;
    bool grounded ;
    bool canMove = true;
    bool onWall = false;    
    bool buttSlumpPressed = false;
    public float deathFromFallingY;
    bool dead;    
    [SerializeField] Vector2 wallJumpDirection;
    [Range(1, 10)] public float jumpVelocity;
    SpriteRenderer bigSprite;
    SpriteRenderer smallSprite;
    SpriteRenderer FireSprite;
    

    [Header("change status")]
    public GameObject MarioMini;
    public GameObject MarioBig;
    public GameObject FireMan;
   // Transform sprite;
    //Transform spriteMini;
    public GameObject fireball;
    public Transform Firestart;
    public Animator anim;
   // PowerUpType currentState = PowerUpType.None;
    public float fireDistance = 1f;
    


    [Header("Collision info")]   
    public LayerMask Platform;
    public LayerMask Wall;
    public Transform groundCheckPoint;   
    public static bool MarioIsSmall = false;
    public Transform wallGrabPoint;
    public float grabDistance = 1f;
   

    [Header("Audio info")]

    [SerializeField]  private AudioSource FireballSound;
    [SerializeField] private  AudioSource  jumpSound;
    [SerializeField] private AudioSource walljump;
    [SerializeField] private AudioSource death;




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
        if (hits <= 0  || transform.position.y < this.deathFromFallingY) { 
            //Die();
            return;
       
        }
    }


    void Update() {

        SetMarioState();

        CollisionCheck();
        //Abl to shoot
       if (hits == 3)
          {
            if (Input.GetKeyDown(KeyCode.E)) {
            if (fireList.Count < 2) { Shoot();
                FireballSound.Play();
            }
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
            if (!facingRight && Input.GetAxisRaw("Horizontal") > 0 || (facingRight && Input.GetAxisRaw("Horizontal") < 0)) {

                wallhangPressed = true;
            }
        }
        //tO MOVE AFTER WALLJUMP
        if (grounded)
            canMove = true;


        /*  if (hits <= 0) {
              //Die();
              gm.lives--;
        death.Play();
          }*/

        /* anim.SetBool("", buttSlumpPressed);
         anim.SetBool("", jumpPressed);
         anim.SetBool("", wallhangPressed);*/



        if (Input.GetAxis("Horizontal")< -0.1f) {
            facingRight =false; }
        else if (Input.GetAxis("Horizontal") > 0.1f) {
            facingRight =true;
        }
       
        Firestart.localPosition = Vector3.right * fireDistance * (facingRight ? 1 : -1);
        wallGrabPoint.localPosition = Vector3.right * grabDistance * (facingRight ? 1 : -1); 


        bigSprite.flipX = !facingRight;
        smallSprite.flipX = !facingRight;
        FireSprite.flipX = !facingRight;
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
        var fireballmov = fireB.GetComponent<Fireball>();
        fireballmov.pm = this;
        fireballmov.velocity = new Vector2(fireballmov.velocity.x * (facingRight ? 1 : -1),fireballmov.velocity.y);
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
                walljump.Play();
                canMove = false;  
            
            }
        }

        else if (grounded ) {
                    

            rb.AddForce(Vector2.up * jumpVelocity,ForceMode2D.Impulse);
            grounded = false;

            jumpSound.Play();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            hits--;
        }
    }

    /*public void Die() {
         dead = true;
       //  animator.Play("death");
              GetComponentInChildren<Collider2D>().enabled = false;
         rb.velocity = Vector2.up * 5f;
          gm.lives--;
     }*/
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
 
    public void SetMarioState() {


        if (hits == 1) {
             Small(); 
        }

        if(hits == 2) {
            Big();
        }
        if (hits == 3) {
            FireMario();
        }

      
    }
    


    void StompJump() {
        rb.AddForce(Vector2.up * jumpVelocity/2, ForceMode2D.Impulse);
    }
          
}






