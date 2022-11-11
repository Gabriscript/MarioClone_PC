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
    public Vector2 enemySize;
    public float castDistance;

    void Start() {
        rb = this.GetComponent<Rigidbody2D>();
        enemySize = GetComponent<BoxCollider2D>().size;
    }

    void FixedUpdate() {
        var hits = Physics2D.BoxCastAll(transform.position, enemySize, 0, rb.velocity, rb.velocity.magnitude * Time.deltaTime);


        if (hits.Length > 1) {
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

        if (hits.Length < 1) {
            
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
