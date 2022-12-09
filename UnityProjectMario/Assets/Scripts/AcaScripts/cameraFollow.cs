using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform player;
    levelZoneScript current;
    public LayerMask levelZone;
    float camY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var zoneCollider = Physics2D.OverlapPoint(player.position, levelZone);

        if (zoneCollider != null) {
            var newZone = zoneCollider.GetComponent<levelZoneScript>();

            if (newZone != current) {
                current = newZone;
                camY = current.cameraY;
            }
        }

        transform.position = new Vector3(player.position.x, camY,-10);
    }
}
