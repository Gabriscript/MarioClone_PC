using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    enum enemyCat { Null, Goompa, Koopa, RedKoopa, PiranhaPlant, Mushroom }
    [SerializeField] enemyCat enemyType;
    Rigidbody2D rb;
    public float movementSpeed;
    Vector3 dir;
    public float dirX = -1f;
    Vector2 enemySize;
    Vector2 feetSize;
    float groundCheckDepth = 0.1f;
    public LayerMask platform;
    Vector2 rayAngleRight;
    Vector2 rayAngleLeft;
    float range = 1f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemySize = GetComponent<BoxCollider2D>().size;
        feetSize = new Vector2(enemySize.x, groundCheckDepth);
        rayAngleRight = new Vector2(5, -1).normalized;
        rayAngleLeft = new Vector2(-5, -1).normalized;
    }

    void FixedUpdate() {


        if (enemyType != enemyCat.PiranhaPlant) {
            // if cliff edge, turn around
            var edger = Physics2D.Raycast(transform.position + 0.5f * Vector3.right, Vector3.down, range, platform);
            var edgel = Physics2D.Raycast(transform.position + 0.5f * -Vector3.right, Vector3.down, range, platform);

            if (enemyType == enemyCat.RedKoopa) {
                if (edger == false) {
                    dirX = -1f;
                }
                if (edgel == false) {
                    dirX = 1f;
                }
            }
            //hits wall, other enemies
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

            // standing on a floor, if not, fall
            var floor = Physics2D.OverlapBox(transform.position + Vector3.down * groundCheckDepth * 0.5f, feetSize, 0, platform);

            if (floor == null) {
                rb.gravityScale = 1f;
            }
            else {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
        }

        //enemy movement type and speed
        if (enemyType == enemyCat.Goompa) {
            movementSpeed = 1f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
        }

        if (enemyType == enemyCat.Mushroom) {
            movementSpeed = 2f;
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
            movementSpeed = 3f;
            rb.velocity = transform.up * movementSpeed;
        }

        //rb.velocity = transform.up * movementSpeed;

        //float maxX = 1f;
        //var newPos = transform.position;
        //newPos.y = (Mathf.PingPong(Time.time, 2) - 1)*maxX;
        //transform.position = newPos;
    }
}


