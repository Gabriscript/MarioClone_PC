using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class momentanyplayer : MonoBehaviour {
    public float speed = 20f;
    Rigidbody2D rb;
    bool wallhangPressed = false;
    bool jumpPressed = false;
    bool momentumPressed = false;
    bool facingRight = true;
    bool grounded;
    public LayerMask Platform;
    public LayerMask Wall;
    public Transform groundCheckPoint;
    public Transform wallGrabPoint;
    bool onWall = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (jumpPressed) {
            Jump();
            jumpPressed = false;
        }
        if (wallhangPressed) {
            WallHanging();
            wallhangPressed = false;

        }
    }
    private void Update() {
        grounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, Platform);
        onWall = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, Wall);
        if (Input.GetKeyDown(KeyCode.Space)) { jumpPressed = true; }
        if (onWall == true && grounded == false) {
            if (!facingRight && Input.GetAxisRaw("Horizontal") > 0 || (facingRight && Input.GetAxisRaw("Horizontal") < 0)) {

                wallhangPressed = true;
            }
        }
        UpdateFacing();
        if (Input.GetKeyDown(KeyCode.Space)) { jumpPressed = true; }
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
    public void WallHanging() {

        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);

    }
    void Jump() {
        if (wallhangPressed) {
            {
                rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * 25, 25);
            }
        } else if (grounded) {

            rb.velocity = new Vector2(rb.velocity.x, speed);
            grounded = false;
        }

    }
}
