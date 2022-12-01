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
    public LayerMask turnCollision;
    float tickTime = 5f;
    public float timer;
    public GameObject enemyAnimation;
    Animator anim;
    PipeCollider pipe;

    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemySize = GetComponent<BoxCollider2D>().size;
        anim = enemyAnimation.GetComponent<Animator>();
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
            var hits = Physics2D.BoxCastAll(transform.position, enemySize, 0, rb.velocity, rb.velocity.magnitude * Time.deltaTime, turnCollision);

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

        // flip sprite
        bool flipped = dirX > 0;
        enemyAnimation.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        

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
            movementSpeed = 1f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
            anim.Play("vSnailAnimation");
        }

        if (enemyType == enemyCat.RedKoopa) {
            movementSpeed = 1f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
            anim.Play("oSnailAnimation");
        }

        if (enemyType == enemyCat.PiranhaPlant) {
            movementSpeed = 3f;

            pipe = GetComponentInParent<PipeCollider>();

            
            if (timer >= 5) {
                if (pipe.frogNear == false) {
                    timer -= tickTime;
                    anim.Play("piranhaPop");
                }
            } else {
                timer += Time.deltaTime;
            }
        }

    }
}


