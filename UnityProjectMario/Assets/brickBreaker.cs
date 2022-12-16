using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickBreaker : MonoBehaviour
{
    Vector2 playerSize;
    public LayerMask platform;
    GridController gc;
    GameObject grid;
    GridLayout gl;
    PlayerMover pm;
    

    void Start()
    {
        playerSize = GetComponent<BoxCollider2D>().size;
        pm = GetComponent<PlayerMover>();
        grid = GameObject.Find("Grid");
        gc = grid.GetComponent<GridController>();
        gl = grid.GetComponent<GridLayout>();
    }

    void Update()
    {
        var coordCheckHead = Physics2D.Raycast(transform.position + new Vector3(0, 0.2f, 0), Vector2.up, 1.5f, platform);
        var coordCheckButt = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, platform);
        Debug.DrawRay(transform.position + new Vector3(0, 0.2f, 0), Vector2.up * 1.5f, Color.blue);
        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.red);
        Vector3Int cellPosition = gl.WorldToCell(coordCheckHead.point);
        Vector3Int cellPosition2 = gl.WorldToCell(coordCheckButt.point);
        if (pm.jumpPressed == true) {
            gc.BumpTile(cellPosition);
        } else if (pm.buttSlumpPressed == true) {
            gc.BumpTile(cellPosition2);
        }
    }
}
