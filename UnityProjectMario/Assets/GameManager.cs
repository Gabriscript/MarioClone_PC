using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour {
    PlayerMover Player;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    public int coins;
    public int lives = 2;
    public int score;
    public int bigCoins;


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public void Coin() {
        coins += 1;
        score += 100;
        UpdateScoreText();
    }

    public void BigCoin() {
        bigCoins += 1;
        score += 1000;
        UpdateScoreText();
    }

    //time
    //score
    //coins collected
    //big coins collected

    public void UpdateLivesText() {

        string update = "Lives :" + lives;
        if (lives <= 0) {
            update = "";
        }
        livesText.text = update;

    }
    public void UpdateScoreText() {

        string update = "Score :" + score;
        if (lives <= 0) {
            update = "";
        }
        scoreText.text = update;

    }
}
