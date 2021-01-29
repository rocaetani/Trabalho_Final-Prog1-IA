using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly2 : MonoBehaviour
{

    private Direction _directionFrom;

    private GridController _gridController;

    private bool _coroutineControl;

    private List<Vector2Int> _wasGluedAt;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _wasGluedAt = new List<Vector2Int>();
        _directionFrom = Direction.None;
        _gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        _coroutineControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_coroutineControl)
        {
            _coroutineControl = false;
            StartCoroutine(Walk());
        }
    }
    

    IEnumerator Walk()
    {
        Vector2Int position = VectorTransformer.Vector3ToVector2Int(transform.position);
        IsCharacterNeighbor(position);
        
        Direction nextDirection = Direction.None;
        int nextNeighborPoints = -200;
        int actualNeighborPoints;

        List<Vector2Int> isGluedAt = VerifyWhereIsGlued(position);
        Vector2Int actualNeighborPosition = position + Vector2Int.left;
        if (_gridController.CellIsEmptyWithoutCharacter(actualNeighborPosition))
        {
            
            actualNeighborPoints = _gridController.VerifyNeighbor(actualNeighborPosition);
            if (_directionFrom == Direction.Right)
            {
                actualNeighborPoints = actualNeighborPoints - 100;
            }


            int isGluedAtPoints = IsInSameWall(actualNeighborPosition, isGluedAt);
            int wasGluedAtPoints = IsInSameWall(actualNeighborPosition, _wasGluedAt);

            actualNeighborPoints = actualNeighborPoints + isGluedAtPoints + wasGluedAtPoints;
            
            if (actualNeighborPoints > nextNeighborPoints)
            {
                nextNeighborPoints = actualNeighborPoints;
                nextDirection = Direction.Left;
            }
            
        }
        
        
        actualNeighborPosition = position + Vector2Int.up;
        if (_gridController.CellIsEmptyWithoutCharacter(actualNeighborPosition))
        {
            
            actualNeighborPoints = _gridController.VerifyNeighbor(actualNeighborPosition);
            if (_directionFrom == Direction.Down)
            {
                actualNeighborPoints = actualNeighborPoints - 100;
            }

            

            int isGluedAtPoints = IsInSameWall(actualNeighborPosition, isGluedAt);
            int wasGluedAtPoints = IsInSameWall(actualNeighborPosition, _wasGluedAt);

            actualNeighborPoints = actualNeighborPoints + isGluedAtPoints + wasGluedAtPoints;
            
            if (actualNeighborPoints > nextNeighborPoints)
            {
                nextNeighborPoints = actualNeighborPoints;
                nextDirection = Direction.Up;
            }
            
        }
        
        actualNeighborPosition = position + Vector2Int.right;
        if (_gridController.CellIsEmptyWithoutCharacter(actualNeighborPosition))
        {
            
            actualNeighborPoints = _gridController.VerifyNeighbor(actualNeighborPosition);
            if (_directionFrom == Direction.Left)
            {
                actualNeighborPoints = actualNeighborPoints - 100;
            }


            int isGluedAtPoints = IsInSameWall(actualNeighborPosition, isGluedAt);
            int wasGluedAtPoints = IsInSameWall(actualNeighborPosition, _wasGluedAt);

            actualNeighborPoints = actualNeighborPoints + isGluedAtPoints + wasGluedAtPoints;
            
            if (actualNeighborPoints > nextNeighborPoints)
            {
                nextNeighborPoints = actualNeighborPoints;
                nextDirection = Direction.Right;
            }
            
        }
        
        actualNeighborPosition = position + Vector2Int.down;
        if (_gridController.CellIsEmptyWithoutCharacter(actualNeighborPosition))
        {
            
            actualNeighborPoints = _gridController.VerifyNeighbor(actualNeighborPosition);
            if (_directionFrom == Direction.Up)
            {
                actualNeighborPoints = actualNeighborPoints - 100;
            }
            
            int isGluedAtPoints = IsInSameWall(actualNeighborPosition, isGluedAt);
            int wasGluedAtPoints = IsInSameWall(actualNeighborPosition, _wasGluedAt);

            actualNeighborPoints = actualNeighborPoints + isGluedAtPoints + wasGluedAtPoints;
            
            if (actualNeighborPoints > nextNeighborPoints)
            {
                nextNeighborPoints = actualNeighborPoints;
                nextDirection = Direction.Down;
            }
            
        }
        
        

        Vector2Int toPosition = VectorTransformer.DirectionToVector2Int(position, nextDirection);
        _gridController.MoveObject(position, toPosition);
        transform.position = VectorTransformer.Vector2IntToVector3Int(toPosition);
        _directionFrom = nextDirection;

        _wasGluedAt = isGluedAt;

            yield return new WaitForSeconds(0.5f);
        _coroutineControl = true;
    }

    public void IsCharacterNeighbor(Vector2Int position)
    {
        if (_gridController.characterPosition == position + Vector2Int.left)
        {
            _gridController.character.GetComponent<Lose>().StageLose();
        }
        if (_gridController.characterPosition == position + Vector2Int.right)
        {
            _gridController.character.GetComponent<Lose>().StageLose();
        }
        if (_gridController.characterPosition == position + Vector2Int.up)
        {
            _gridController.character.GetComponent<Lose>().StageLose();
        }
        if (_gridController.characterPosition == position + Vector2Int.down)
        {
            _gridController.character.GetComponent<Lose>().StageLose();
        }
    }

    public int IsInSameWall(Vector2Int toPositon , List<Vector2Int> listOfGlued)
    {
        int points = 0;
        foreach (Vector2Int glued in listOfGlued)
        {
            if (IsNeighborOf(toPositon, glued))
            {
                points = points + 10;
            }
        }

        return points;
    }
    
    /*

    private bool IsNeighborOf(Vector2Int position, Vector2Int neighbor)
    {
        if ((position + Vector2Int.left).Equals(neighbor))
        {
            return true;
        }
        if (position + Vector2Int.right == neighbor)
        {
            return true;
        }
        if (position + Vector2Int.down == neighbor)
        {
            return true;
        }
        if (position + Vector2Int.up == neighbor)
        {
            return true;
        }
        
        if (position + Vector2Int.down + Vector2Int.left == neighbor)
        {
            return true;
        }
        if (position + Vector2Int.down + Vector2Int.right== neighbor)
        {
            return true;
        }
        
        if (position + Vector2Int.up + Vector2Int.left == neighbor)
        {
            return true;
        }
        if (position + Vector2Int.up + Vector2Int.right== neighbor)
        {
            return true;
        }

        return false;
    }
    */
    private bool IsNeighborOf(Vector2Int position, Vector2Int neighbor)
    {
        if (Mathf.Abs(position.x - neighbor.x) > 1 || Mathf.Abs(position.y - neighbor.y) > 1)
        {
            return false;
        }

        return true;
    }

    public List<Vector2Int> VerifyWhereIsGlued(Vector2Int position)
    {
        List<Vector2Int> listOfGlued = new List<Vector2Int>();
        Vector2Int wallPosition = position + Vector2Int.left;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        wallPosition = position + Vector2Int.right;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        wallPosition = position + Vector2Int.down;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        wallPosition = position + Vector2Int.up;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        
        wallPosition = position + Vector2Int.down + Vector2Int.left;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        wallPosition = position + Vector2Int.up + Vector2Int.left;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        
        wallPosition = position + Vector2Int.down + Vector2Int.right;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }
        wallPosition = position + Vector2Int.up+ Vector2Int.right;
        if (!_gridController.CellIsEmptyWithoutCharacter(wallPosition))
        {
            listOfGlued.Add(wallPosition);
        }

        return listOfGlued;
    }




}
