using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerUpType {None, ExtraLife, Mushroom, FireFlower};
public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType powerup;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        FindObjectOfType<GameManager>().ActivatePowerup(powerup);

        
        Destroy(gameObject);
    }
   
}
