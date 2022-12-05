using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    GameManager gm;
    public bool bigCoin;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 3) {
            if (bigCoin == true) {
                Destroy(gameObject);
                gm.BigCoin();
            } else {
                Destroy(gameObject);
                gm.Coin();
            }
        }
    }
}
