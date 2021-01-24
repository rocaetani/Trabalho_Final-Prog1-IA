using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        showList();
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
    
    public bool CellIsEmptyWithoutCharacter(Vector2Int position)
    {
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

    public bool HasTileAt(Vector2Int position)
    {
        foreach (Tilemap tilemap in _tilemapList)
        {
            if (tilemap.HasTile(VectorTransformer.Vector2IntToVector3Int(position)))
            {
                return true;
            }
        }
        return false;
    }

    public void showList()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (Vector2Int key in _gridObjects.Keys)
            {
                Debug.Log(key);
            }
            Debug.Log("----------------");
        }
    }

    public bool HasNextFreeSpace(Vector2Int fromPosition, Direction direction)
    {
        
        Vector2Int directionValue;
        switch (direction)
        {
            case Direction.Right:
                directionValue = Vector2Int.right;
                break;
            case Direction.Left:
                directionValue = Vector2Int.left;
                break;
            case Direction.Up:
                directionValue = Vector2Int.up;
                break;
            case Direction.Down:
                directionValue = Vector2Int.down;
                break;
            default:
                directionValue = Vector2Int.zero;
                break;
        }

        for (Vector2Int position = fromPosition; true ; position += directionValue)
        {
            if (CellIsEmpty(position))
            {
                return true;
            }

            if (IsCharacterOnCell(position))
            {
                return false;
            }

            if (HasTileAt(position))
            { 
                return false;
            }
        }
    }
    public Vector2Int GetNextFreeSpace(Vector2Int fromPosition, Direction direction)
    {
        
        Vector2Int directionValue;
        switch (direction)
        {
            case Direction.Right:
                directionValue = Vector2Int.right;
                break;
            case Direction.Left:
                directionValue = Vector2Int.left;
                break;
            case Direction.Up:
                directionValue = Vector2Int.up;
                break;
            case Direction.Down:
                directionValue = Vector2Int.down;
                break;
            default:
                directionValue = Vector2Int.zero;
                break;
        }

        for (Vector2Int position = fromPosition; true ; position += directionValue)
        {
            if (CellIsEmpty(position))
            {
                return position;
            }

            if (IsCharacterOnCell(position))
            {
                return VectorTransformer.NullPoint;
            }

            if (HasTileAt(position))
            {
                return VectorTransformer.NullPoint;
            }
        }
    }


    public void RemoveObject(Vector2Int fromPosition)
    {
        _gridObjects.Remove(fromPosition);
    }
}
