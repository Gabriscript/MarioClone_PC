using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour {
    private Grid grid;
    GameManager gm;
    [SerializeField] Tilemap map = null;
    [SerializeField] Tile pathTile = null;
    [SerializeField] Tile breakable = null;
    [SerializeField] Tile oneCoin = null;
    [SerializeField] Tile manyCoins = null;
    [SerializeField] Tile empty = null;
    Dictionary<Vector3Int, int> coinsSqueezed = new Dictionary<Vector3Int, int>();
    int maxCoins = 5;

    private Vector3Int previousMousePos = new Vector3Int();

    void Start() {
        grid = gameObject.GetComponent<Grid>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        // Mouse over
        Vector3Int mousePos = GetMousePosition();

        // Left mouse click -> add path tile
        if (Input.GetMouseButtonDown(0)) {
            //map.SetTile(mousePos, pathTile);
            BumpTile(mousePos);
        }

    }

    Vector3Int GetMousePosition() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    void BumpTile(Vector3Int coords) {
        var tile = map.GetTile(coords);
        if (tile == breakable) {
            gm.score += 10;
            map.SetTile(coords, null);
        }
        if (tile == oneCoin) {
            gm.Coin();
            map.SetTile(coords, empty);
        }
        if (tile == manyCoins){
            gm.Coin();
            coinsSqueezed.TryAdd(coords, 0);
            coinsSqueezed[coords]++;
            if (coinsSqueezed[coords] == maxCoins) {
                map.SetTile(coords, empty);
            }
        }
    } 
}