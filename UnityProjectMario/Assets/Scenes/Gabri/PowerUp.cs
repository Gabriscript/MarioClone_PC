using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    //public enum PowerUpType { None, Big}; TO DEFINE
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) {

        FindObjectOfType<PlayerMover>();

        
        Destroy(gameObject);
    }
   
}
