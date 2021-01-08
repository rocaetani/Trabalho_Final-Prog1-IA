using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{

    private List<Tilemap> _tilemapList; // Tilemap Controller

    private Dictionary<Vector2Int, Transform> _gridObjects; // No Tilemap Controller

    public GameObject character;
    
    public Vector2Int characterPosition;
    // Start is called before the first frame update
    void Start()
    {
        _tilemapList = new List<Tilemap>();
        _gridObjects = new Dictionary<Vector2Int, Transform>();
        
        for (int index = 0; index < transform.childCount; index++)
        {
            Transform child = transform.GetChild(index);
            
            Tilemap tilemapComponent = child.GetComponent<Tilemap>();
            if (tilemapComponent == null)
            {
                //Add no tilemap Object
                AddObjectToGrid(child);
                Debug.Log("Add in Objects: " + child.name);
            }
            else
            {
                //Add tilemap Object
                
                if (child.tag.Equals("SolidTile"))
                {
                    _tilemapList.Add(child.GetComponent<Tilemap>());
                    Debug.Log("Add in Tilemaps: " + child.name);
                }

                
            }
        }

        
    }

    void Update()
    {
        characterPosition = VectorTransformer.Vector3ToVector2Int(character.transform.position);
    }

    public void AddObjectToGrid(Transform newObject)
    {
        Vector2Int childPosition = VectorTransformer.Vector3ToVector2Int(newObject.position);
        _gridObjects.Add(childPosition, newObject);
    }

    public bool CellIsEmpty(Vector2Int position)
    {
        if (characterPosition.Equals(position))
        {
            return false;
        }

        //Verify on Objects
        if (_gridObjects.ContainsKey(position))
        {
            return false;
        }

        //Verify on Tilemaps
        foreach (Tilemap tilemap in _tilemapList)
        {
            if (tilemap.HasTile(VectorTransformer.Vector2IntToVector3Int(position)))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsCharacterOnCell(Vector2Int position)
    {
        if (characterPosition.Equals(position))
        {
            return true;
        }

        return false;
    }

    public void MoveObject(Vector2Int fromPosition, Vector2Int toPosition)
    {
        Transform movedObject;
        _gridObjects.TryGetValue(fromPosition, out movedObject);
        _gridObjects.Remove(fromPosition);
        _gridObjects.Add(toPosition,movedObject);
    }

    public Transform GetObject(Vector2Int position)
    {
        _gridObjects.TryGetValue(position, out var gridObject);
        return gridObject;
    }

    public bool HasGridObjectAt(Vector2Int position)
    {
        if (_gridObjects.ContainsKey(position))
        {
            return true;
        }
        return false;
    }
    
}
