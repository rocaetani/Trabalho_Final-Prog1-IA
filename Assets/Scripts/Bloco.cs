using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bloco : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        TileData tileData = new TileData();
        TileBase tileBase = GetComponent<ITilemap>().GetTile(Vector3Int.FloorToInt(collision.transform.position));
        tileBase.GetTileData(Vector3Int.FloorToInt(collision.transform.position),GetComponent<ITilemap>(), ref tileData);
        
        tileData.color = Color.green;
    }
}
