
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None,
    Up,
    Down,
    Right,
    Left
}

public class Move : MonoBehaviour
{
    // Start is called before the first frame update

    public float walkSpeed = 10f;
    //Rigidbody2D rigidbody;
    float _deltaX = 0;
    private int _xpos = 10;
    private GUIStyle _guiStyle = new GUIStyle();
    

    private Vector3 _currentTarget;
    private bool _isColiding;

    private bool _isMoving;
    private int _counter;
    private float _distanceMovedThisFrame;
    private float _distanceToTarget;
    private Direction _moveDirection;
    private Vector3 _lastPosition;
    
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
        _lastPosition = transform.position;
        _currentTarget = _lastPosition;
        _guiStyle.fontSize = 30;
        _guiStyle.normal.textColor = Color.blue;
        _isColiding = false;
        _isMoving = false;
        _counter = 0;
        _distanceMovedThisFrame = 0;
        _distanceToTarget = 0;
        _moveDirection = Direction.None;
        

    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            counter++;
            Vector3 direction = new Vector3(1,0,0);
            MoveCharacter(walkSpeed * direction * Time.deltaTime);
            quantoAndou = walkSpeed * Time.deltaTime;
        }
    }*/
    
    void Update()
    {


            if (Input.GetAxis("Horizontal") >= 1f &
                (_moveDirection == Direction.Right || _moveDirection == Direction.None))
            {
                _isMoving = true;
                _moveDirection = Direction.Right;
                _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x + 1f), transform.position.y, 0);
            }

            if (Input.GetAxis("Horizontal") <= -1f &
                (_moveDirection == Direction.Left || _moveDirection == Direction.None))
            {
                _isMoving = true;
                _moveDirection = Direction.Left;
                _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x - 1f), transform.position.y, 0);
            }

            if (Input.GetAxis("Vertical") >= 1f & (_moveDirection == Direction.Up || _moveDirection == Direction.None))
            {
                _isMoving = true;
                _moveDirection = Direction.Up;
                _currentTarget = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y + 1f), 0);
            }

            if (Input.GetAxis("Vertical") <= -1f & (_moveDirection == Direction.Down || _moveDirection == Direction.None))
            {
                _isMoving = true;
                _moveDirection = Direction.Down;
                _currentTarget = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y - 1f), 0);
            }



            if (_isMoving)
            {
                Vector3 direction = _currentTarget - transform.position;
                _distanceToTarget = direction.magnitude;
                direction.Normalize();



                _distanceMovedThisFrame = walkSpeed * Time.deltaTime;


                if (Mathf.Abs(Input.GetAxis("Horizontal")) < 1f & (_distanceMovedThisFrame >= _distanceToTarget))
                {
                    MoveCharacter(_currentTarget, _distanceToTarget);
                }

                if (_currentTarget == transform.position
                ) //(Input.GetAxis("Horizontal") == 0 & Input.GetAxis("Vertical") == 0)
                {
                    _isMoving = false;
                    _moveDirection = Direction.None;
                }
                else
                {
                    MoveCharacter(_currentTarget, _distanceMovedThisFrame);
                }
            

        }
        /*
        else
        {
            Vector3 repositionCharacter;
            if (moveDirection == Direction.Right)
            {
                Debug.Log("entrou aqui ajuste colide");
                repositionCharacter = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, 0);
                _currentTarget = repositionCharacter;
                PlaceCharacter(repositionCharacter);
            }
            
            isColiding = false;
        }
        */




    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        _isColiding = true;
        Debug.Log(collision.transform.position);
        _lastPosition = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, 0);

    }

    

    void MoveCharacter(Vector3 target, float step)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        //transform.position += frameMovement;
    }
    void PlaceCharacter(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    
    
    
    void OnGUI() {
        string[] strings = {
            "Horiz.: " + Input.GetAxis("Horizontal") ,
            "Vert. : " + Input.GetAxis("Vertical"),
            "Character Pos: " + transform.position.x + ", " + transform.position.y,
            "Target: " + _currentTarget,
            "Last Position: " + _lastPosition
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(_xpos, ypos, 100, 20), str, _guiStyle);
            ypos += 20;
        }
    }
    
    
    

}
