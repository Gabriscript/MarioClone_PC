using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public GameObject Player;
    public float offset;//How far you can see from the center of the screen
    public float offsetSmoothing;//camera movement to fallow player facing
    private Vector3 playerPosition;

    void Update() {//CHECK  CINEMACHINE TUTORIAL AND DOWNLOAD IT  TO FOLLOW YOU GAME

        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);


        /*if(Player.transform.localScale.x > 0f) {
             playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
         } else {
             playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
         }

         transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);




     */}
    }

