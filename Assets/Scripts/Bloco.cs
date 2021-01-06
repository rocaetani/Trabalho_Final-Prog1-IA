using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bloco : MonoBehaviour
{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        Tilemap tilemap = GetComponent<Tilemap>();
        //Debug.Log(tilemap.name);
        //Debug.Log(collision.contactCount);
        Grid grid = tilemap.layoutGrid;
        //Vector3 tilePosition = grid.WorldToCell(collision.GetContact(0).point);
        Vector3Int tilePosition = Vector3Int.FloorToInt(new Vector3(collision.transform.position.x, collision.transform.position.y,0));
        Debug.Log(tilePosition);
        tilemap.SetTile(tilePosition, null);
        
        /*    
        TileBase tileBase = tilemap.GetTile(Vector3Int.FloorToInt(tilePosition));
        TileBase tile = tilemap.GetTile(Vector3Int.FloorToInt(tilePosition));
        tile.RefreshTile(Vector3Int.FloorToInt(tilePosition),tilemap);

            Debug.Log(tile);
        //Debug.Log(collision.GetContact(0).point);
        /*
        ContactPoint2D[] contacts = collision.contacts;
        foreach (var contact in contacts)
        {
            Debug.Log(contact.point);
        }
        */
        //Tile tile = tilemap.GetTile<Tile>(tilePosition.);




    }
}


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