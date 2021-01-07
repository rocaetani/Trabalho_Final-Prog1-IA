using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Stone : MonoBehaviour
{
    public GameObject charcater;
    private Tilemap _tilemap;
    
    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterPosition = charcater.transform.position;
        if (Input.GetAxis("Horizontal") > 0)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(characterPosition.x + 0.5f), Mathf.RoundToInt(characterPosition.y),0);
            if (_tilemap.HasTile(tilePosition))
            {
                _tilemap.SetTile(tilePosition, null);
            }
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(characterPosition.x - 0.5f), Mathf.RoundToInt(characterPosition.y),0);
            if (_tilemap.HasTile(tilePosition))
            {
                _tilemap.SetTile(tilePosition, null);
            }
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(characterPosition.x), Mathf.RoundToInt(characterPosition.y + 0.5f),0);
            if (_tilemap.HasTile(tilePosition))
            {
                _tilemap.SetTile(tilePosition, null);
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Vector3Int tilePosition = new Vector3Int(Mathf.RoundToInt(characterPosition.x), Mathf.RoundToInt(characterPosition.y - 0.5f),0);
            if (_tilemap.HasTile(tilePosition))
            {
                _tilemap.SetTile(tilePosition, null);
            }
        }
    }

    /*
     private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    */
    
}
