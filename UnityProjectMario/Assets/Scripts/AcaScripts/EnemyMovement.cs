using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    enum enemyCat {Null, Goompa, Koopa, Redkoopa, PiranhaPlant}
    [SerializeField] enemyCat enemyType;
    Transform enemy;
    float movementSpeed;
    Vector3 dir;

    void Start()
    {
        enemy = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyType == enemyCat.Goompa) {
            movementSpeed = 0.01f;
            if (Input.GetKey(KeyCode.RightArrow)) {
                dir = Vector3.right;
            } else {
                dir = Vector3.left;
            }
            enemy.position = transform.position + movementSpeed * dir;
        }

        if (enemyType == enemyCat.Koopa) {
            movementSpeed = 0.1f;
            if (Input.GetKey(KeyCode.RightArrow)) {
                dir = Vector3.right;
            }
            else {
                dir = Vector3.left;
            }
            enemy.position = transform.position + movementSpeed * dir;
        }

        //red koopa

        if (enemyType == enemyCat.PiranhaPlant) {
            movementSpeed = 0.1f;
            if (Input.GetKey(KeyCode.RightArrow)) {
                dir = Vector3.down;
            }
            else {
                dir = Vector3.up;
            }
            enemy.position = transform.position + movementSpeed * dir;
        }

    }
}
