using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PushRock : MonoBehaviour
{

    public Grid Grid;

    private Tuple<Vector2Int, Direction> _pushFrom;
    private GridController _gridController;
    // Start is called before the first frame update
    void Start()
    {
        _gridController = Grid.GetComponent<GridController>();
        _pushFrom = new Tuple<Vector2Int, Direction>(VectorTransformer.NullPoint, Direction.None);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int position = VectorTransformer.Vector3ToVector2Int(transform.position);
        if (Input.GetAxis("Horizontal") >= 1f)
        {
            Vector2Int positionRight = position + Vector2Int.right;
            
            if(_gridController.HasGridObjectAt(positionRight) & _gridController.CellIsEmpty(positionRight + Vector2Int.right))
           {
               if (VectorTransformer.IsAlmostEqualVector3Int(transform.position))
               {
                   if (_pushFrom.Item2 != Direction.Right || !_pushFrom.Item1.Equals(position))
                   {
                       _pushFrom = new Tuple<Vector2Int, Direction>(position, Direction.Right);
                       _gridController.GetObject(positionRight).transform.position += new Vector3(1,0,0);
                       _gridController.MoveObject(positionRight, positionRight + Vector2Int.right);
                   }
               }
           }
        }
        if (Input.GetAxis("Horizontal") <= 1f)
        {
            Vector2Int positionLeft = position + Vector2Int.left;
            if(_gridController.HasGridObjectAt(positionLeft) & _gridController.CellIsEmpty(positionLeft + Vector2Int.left))
            {
                if (VectorTransformer.IsAlmostEqualVector3Int(transform.position))
                {
                    if (_pushFrom.Item2 != Direction.Left || !_pushFrom.Item1.Equals(position))
                    {
                        _pushFrom = new Tuple<Vector2Int, Direction>(position, Direction.Left);
                        _gridController.GetObject(positionLeft).transform.position += new Vector3(-1,0,0);
                        _gridController.MoveObject(positionLeft, positionLeft + Vector2Int.left);
                    }
                }
            }
        }
        
    }


}
