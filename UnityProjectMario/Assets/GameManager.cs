using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerMover Player;
    [SerializeField] List<PowerUpType> marioPowerups;//addes this
    public int hits;// added this
    public int coins;
    public int lives;
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

    public void ActivatePowerup(PowerUpType powerup) {
        print("Power activated : " + powerup);
        if (powerup == PowerUpType.ExtraLife) {
            lives++;
            // UpdateLivesText();
        } else if (powerup == PowerUpType.Mushroom) {
            hits = 2;
        } else if (powerup == PowerUpType.FireFlower) {

            hits = 3;
        } else {
            Debug.LogError("Can´t handle powerup type : " + powerup);
        }
    }
}
