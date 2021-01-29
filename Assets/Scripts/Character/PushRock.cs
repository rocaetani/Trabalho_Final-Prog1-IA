using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PushRock : MonoBehaviour
{

    public Grid Grid;

    private Tuple<Vector2Int, Direction> _pushFrom;
    private Tuple<Vector2Int, Direction> _pushFromCoroutine;
    private GridController _gridController;

    

    private bool _coroutineIsRunning;
    // Start is called before the first frame update
    void Start()
    {
        _gridController = Grid.GetComponent<GridController>();
        _pushFrom = new Tuple<Vector2Int, Direction>(VectorTransformer.NullPoint, Direction.None);
        _pushFromCoroutine = new Tuple<Vector2Int, Direction>(VectorTransformer.NullPoint, Direction.None);
        _coroutineIsRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int position = VectorTransformer.Vector3ToVector2Int(transform.position);
        if (Input.GetAxis("Horizontal") >= 1f)
        {
            Vector2Int positionRight = position + Vector2Int.right;
            
            if(CanBePushed(positionRight, Direction.Right))
                //if(_gridController.HasGridObjectAt(positionRight) & _gridController.CellIsEmpty(positionRight + Vector2Int.right))
            {
                if (VectorTransformer.IsAlmostEqualVector3Int(transform.position))
                {
                    if (_pushFrom.Item2 != Direction.Right || !_pushFrom.Item1.Equals(position))
                    {
                        //Save the values of the push
                        _pushFrom = new Tuple<Vector2Int, Direction>(position, Direction.Right);

                        if (!_coroutineIsRunning)
                        {
                            _coroutineIsRunning = true;
                            //chama a corrotina
                            StartCoroutine(WaitPush(position, positionRight, Direction.Right));
                        }
                    }
                }
            }
        }
        if (Input.GetAxis("Horizontal")  <= -1f)
        {
            Vector2Int positionLeft = position + Vector2Int.left;
            if(CanBePushed(positionLeft, Direction.Left))
                //if(_gridController.HasGridObjectAt(positionLeft) & _gridController.CellIsEmpty(positionLeft + Vector2Int.left) )
            {
                if (VectorTransformer.IsAlmostEqualVector3Int(transform.position))
                {
                    //Verify if the last push was from the same position and direction to prevent mutiple pushes from the same place
                    if (_pushFrom.Item2 != Direction.Left || !_pushFrom.Item1.Equals(position))
                    {
                        //Save the values of the push
                        _pushFrom = new Tuple<Vector2Int, Direction>(position, Direction.Left);

                        if (!_coroutineIsRunning)
                        {
                            _coroutineIsRunning = true;
                            //chama a corrotina
                            StartCoroutine(WaitPush(position, positionLeft, Direction.Left));
                        }
                    }
                }
            }
        }
        
    }

    private bool CanBePushed( Vector2Int positionDirection, Direction direction)
    {
        Vector2Int vectorDirection = (direction == Direction.Right) ? Vector2Int.right : Vector2Int.left;
        if(_gridController.HasGridObjectAt(positionDirection)
           & _gridController.CellIsEmpty(positionDirection + vectorDirection)
           & !_gridController.CellIsEmpty(positionDirection + Vector2Int.down))
            
        {
            if (_gridController.GetObject(positionDirection).CompareTag("Rock"))
            {
                return true;
            }
        }

        return false;
    }

    private void Push(Vector2Int position, Vector2Int positionDirection, Direction direction)
    {
        Vector2Int vectorDirection = (direction == Direction.Right) ? Vector2Int.right : Vector2Int.left;


        _gridController.GetObject(positionDirection).transform.position += VectorTransformer.Vector2IntToVector3Int(vectorDirection);

        _gridController.MoveObject(positionDirection, positionDirection + vectorDirection);
    }

    IEnumerator WaitPush(Vector2Int position, Vector2Int positionDirection, Direction direction)
    {
        float isPushing = (direction == Direction.Right) ? 1 : -1;
        _pushFromCoroutine = _pushFrom;
        yield return new WaitForSeconds(0.5f);
        if (_pushFromCoroutine.Equals(_pushFrom) & Input.GetAxis("Horizontal")  == isPushing)
        {
            Push(position, positionDirection, direction);
        }
        _coroutineIsRunning = false;

    }
    


}
