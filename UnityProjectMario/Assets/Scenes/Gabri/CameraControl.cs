using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Transform followTarget;

    void Update() {
        var p = transform.position;
        p.x = followTarget.position.x;
        transform.position = p;

    }
}
