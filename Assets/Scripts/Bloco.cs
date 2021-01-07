using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Bloco : MonoBehaviour
{
    private Tilemap _tilemap;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        Vector3Int tilePosition = Vector3Int.FloorToInt(new Vector3(collision.transform.position.x, collision.transform.position.y,0));

        tilemap.SetTile(tilePosition, null);
        
    }
}

