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
    public LayerMask player;
    Vector2 rayAngleRight;
    Vector2 rayAngleLeft;
    float range = 1f;
    public LayerMask turnCollision;
    float tickTime = 5f;
    public float timer;
    public GameObject enemyAnimation;
    Animator anim;
    PipeCollider pipe;
    Animator animSort;
    SpriteRenderer rend;
    Vector2 stompCollider;
    public GameObject shell;

    

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        enemySize = GetComponent<BoxCollider2D>().size * 0.95f;
        anim = enemyAnimation.GetComponent<Animator>();
        animSort = GetComponentInChildren<Animator>();
        rend = animSort.GetComponent<SpriteRenderer>();
        feetSize = new Vector3(enemySize.x, groundCheckDepth);
        rayAngleRight = new Vector2(5, -1).normalized;
        rayAngleLeft = new Vector2(-5, -1).normalized;
        stompCollider = new Vector2(enemySize.x, 0.25f);

        //randomize layer
        int order;
        order = Random.Range(1, 999);
        rend.sortingOrder = order;
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
            if (enemyType == enemyCat.Mushroom) {
                var hits = Physics2D.BoxCastAll(transform.position + Vector3.up * 0.51f * enemySize.y, enemySize, 0, rb.velocity, rb.velocity.magnitude * Time.deltaTime, turnCollision);
                if (hits.Length > 0) {
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
            } else {
                var hits = Physics2D.BoxCastAll(transform.position + Vector3.up * 0.51f * enemySize.y, enemySize, 0, rb.velocity, rb.velocity.magnitude * Time.deltaTime, turnCollision);

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
            }

            // standing on a floor, if not, fall
            var floor = Physics2D.OverlapBox(transform.position + Vector3.down * groundCheckDepth * 0.7f, feetSize, 0, platform);

            if (floor == null) {
                rb.gravityScale = 1f;
            }
            else {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
            }
        }

        //death by stomp
        var stomp = Physics2D.OverlapBox(transform.position + Vector3.up * enemySize.y * 0.25f, stompCollider, 0, player);

        if (stomp == true) {
            if (enemyType == enemyCat.Goompa) {
                Destroy(gameObject);
                FindObjectOfType<PlayerMover>().StompJump();
            } else {
                if (enemyType == enemyCat.RedKoopa || enemyType == enemyCat.Koopa) {
                    Destroy(gameObject);
                    Instantiate(shell, transform.position, transform.rotation);
                    FindObjectOfType<PlayerMover>().StompJump();
                }
            }
        }
        

        // flip sprite
        bool flipped = dirX > 0;
        enemyAnimation.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));

        

        //enemy movement type and speed
        if (enemyType == enemyCat.Goompa) {
            movementSpeed = 1f;
            rb.velocity = new Vector2(movementSpeed * dirX, rb.velocity.y);
            anim.Play("snek");
        }

        if (enemyType == enemyCat.Mushroom) {
            movementSpeed = 2.5f;
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


