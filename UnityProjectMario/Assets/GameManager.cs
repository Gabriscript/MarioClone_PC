using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour {
    PlayerMover Player;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI coinText;
    public int coins;
    public int lives = 5;
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
        UpdateCoinText();
    }

    public void BigCoin() {
        bigCoins += 1;
        score += 1000;
        UpdateScoreText();
        UpdateCoinText();
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
        if (score <= 0) {
            update = "";
        }
        scoreText.text = update;

    }
    public void UpdateCoinText() {

        string update = "Coin :" + coins;
        if (coins <= 0) {
            update = "";
        }
        if (coins == 100) {
            lives++;
            UpdateLivesText();
            coins = 0;
            update = "";
        }
        coinText.text = update;

    }
}
