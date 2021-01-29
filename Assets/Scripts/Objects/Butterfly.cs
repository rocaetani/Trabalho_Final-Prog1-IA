using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butterfly : MonoBehaviour
{

    public Direction _direction;

    private GridController _gridController;

    private bool _coroutineControl;
    
    public Direction newLeft; 
    
    // Start is called before the first frame update
    void Start()
    {
        _direction = Direction.Up;
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
        //Direction newLeft = TurnLeft(_direction);
        newLeft = TurnLeft(_direction);
        Vector2Int leftPosition = VectorTransformer.DirectionToVector2Int(position, newLeft);
        Vector2Int frontPosition = VectorTransformer.DirectionToVector2Int(position, _direction);
        Debug.Log("Iniciou: " + _direction + '-' + newLeft);
        //Verificar se pode ir para esquerda. Se sim vira e anda
        if (_gridController.CellIsEmpty(leftPosition))
        {
            _direction = newLeft;
            _gridController.MoveObject(position, leftPosition);
            transform.position = VectorTransformer.Vector2IntToVector3Int(leftPosition);
            Debug.Log("Go Left: "+ _direction + "-" + newLeft);
        }
        //Se não anda para frente
        else if (_gridController.CellIsEmpty(frontPosition))
        {
            _gridController.MoveObject(position, frontPosition);
            transform.position = VectorTransformer.Vector2IntToVector3Int(frontPosition);
            Debug.Log("Go Ahead: "+ _direction + "-" + newLeft);
        }
        //Senão puder andar para esquerda nem para frente apenas vira a direita
        else
        {
            _direction = TurnRight(_direction);
            Debug.Log("Turn Right: " + _direction + "-" + newLeft);
        }


        yield return new WaitForSeconds(0.5f);
        _coroutineControl = true;
    }



    private Direction TurnLeft(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return Direction.Left;
            case Direction.Left: return Direction.Down;
            case Direction.Down: return Direction.Right;
            case Direction.Right: return Direction.Up;
        }

        return Direction.None;
    }
    
    private Direction TurnRight(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up: return Direction.Right;
            case Direction.Right: return Direction.Down;
            case Direction.Down: return Direction.Left;
            case Direction.Left: return Direction.Up;
        }
        return Direction.None;
    }




}
