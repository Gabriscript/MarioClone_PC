using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    enum enemyCat { Null, Goompa, Koopa, RedKoopa, PiranhaPlant }
    [SerializeField] enemyCat enemyType;
    Rigidbody2D rb;
    float movementSpeed;
    Vector3 dir;
    public float dirX = -1f;
    Vector2 enemySize;
    Vector2 feetSize;
    float groundCheckDepth = 0.5f;
    public LayerMask platform;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemySize = GetComponent<BoxCollider2D>().size;
        feetSize = new Vector2(enemySize.x, groundCheckDepth);
    }

    void FixedUpdate() {
        var hits = Physics2D.BoxCastAll(transform.position, enemySize, 0, rb.velocity, rb.velocity.magnitude * Time.deltaTime);

        if (hits.Length > 2) {
            foreach (var hit in hits) {
                if (hit.collider.gameObject != gameObject) {
                    if (hit.point.x > transform.position.x) {
                        dirX = -1f;
                    }
                    if (hit.point.x < transform.position.x) {
                        dirX = 1f;
                    }
                }
            }
        }

        var floor = Physics2D.OverlapBox(transform.position + Vector3.down * groundCheckDepth * 0.5f, feetSize, 0, platform);

        if (floor == null) {
            rb.gravityScale = 1f;
        } else {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }

        if (enemyType == enemyCat.Goompa) {
            movementSpeed = 0.01f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
        }

        if (enemyType == enemyCat.Koopa) {
            movementSpeed = 5f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
        }

        if (enemyType == enemyCat.RedKoopa) {
            movementSpeed = 5f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
        }

        if (enemyType == enemyCat.PiranhaPlant) {
            movementSpeed = 0.1f;
            transform.position = base.transform.position + movementSpeed * dir;
        }
    }
}
