using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bloco : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
     /*   
        Vector3 colPos = collision.transform.position;
        Tilemap tilemap = GetComponent<Tilemap>();
        Tile tile = tilemap.GetTile<Tile>(Vector3Int.FloorToInt(colPos));
        
        tile.color = Color.green;
        
        /*
        
        TileData tileData = new TileData();
        TileBase tileBase = GetComponent<ITilemap>().GetTile(Vector3Int.FloorToInt(colPos));
        tileBase.GetTileData(Vector3Int.FloorToInt(colPos),GetComponent<ITilemap>(), ref tileData);
        
        tileData.color = Color.green;
        */
        
        Debug.Log("Chegou collider");
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        Debug.Log("Chegou trigger");
        Vector3 colPos = collide.transform.position;
        Debug.Log(colPos);
        Tilemap tilemap = GetComponent<Tilemap>();
        Tile tile = tilemap.GetTile<Tile>(Vector3Int.FloorToInt(colPos));
        Debug.Log(tile.color);
        tile.color = Color.green;

    }
}
