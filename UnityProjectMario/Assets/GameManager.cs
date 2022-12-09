using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerMover Player;
    
    public int coins;
    public int lives = 2;
    public int score;
    public int bigCoins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Coin() {
        coins += 1;
        score += 100;
    }

    public void BigCoin() {
        bigCoins += 1;
        score += 1000;
    }

    //time
    //score
    //coins collected
    //big coins collected

   
   
}
