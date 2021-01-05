
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
    float deltaX = 0;
    private int xpos = 10;
    private GUIStyle guiStyle = new GUIStyle();
    

    private Vector3 _currentTarget;
    private bool isColiding;

    private bool isMoving;
    private int counter;
    private float distanceMovedThisFrame;
    private float distanceToTarget;
    private Direction moveDirection;
    private Vector3 _lastPosition;
    
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
        _lastPosition = transform.position;
        _currentTarget = _lastPosition;
        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.blue;
        isColiding = false;
        isMoving = false;
        counter = 0;
        distanceMovedThisFrame = 0;
        distanceToTarget = 0;
        moveDirection = Direction.None;
        

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
                (moveDirection == Direction.Right || moveDirection == Direction.None))
            {
                isMoving = true;
                moveDirection = Direction.Right;
                _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x + 1f), transform.position.y, 0);
            }

            if (Input.GetAxis("Horizontal") <= -1f &
                (moveDirection == Direction.Left || moveDirection == Direction.None))
            {
                isMoving = true;
                moveDirection = Direction.Left;
                _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x - 1f), transform.position.y, 0);
            }

            if (Input.GetAxis("Vertical") >= 1f & (moveDirection == Direction.Up || moveDirection == Direction.None))
            {
                isMoving = true;
                moveDirection = Direction.Up;
                _currentTarget = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y + 1f), 0);
            }

            if (Input.GetAxis("Vertical") <= -1f & (moveDirection == Direction.Down || moveDirection == Direction.None))
            {
                isMoving = true;
                moveDirection = Direction.Down;
                _currentTarget = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y - 1f), 0);
            }



            if (isMoving)
            {
                Vector3 direction = _currentTarget - transform.position;
                distanceToTarget = direction.magnitude;
                direction.Normalize();



                distanceMovedThisFrame = walkSpeed * Time.deltaTime;


                if (Mathf.Abs(Input.GetAxis("Horizontal")) < 1f & (distanceMovedThisFrame >= distanceToTarget))
                {
                    MoveCharacter(_currentTarget, distanceToTarget);
                }

                if (_currentTarget == transform.position
                ) //(Input.GetAxis("Horizontal") == 0 & Input.GetAxis("Vertical") == 0)
                {
                    isMoving = false;
                    moveDirection = Direction.None;
                }
                else
                {
                    MoveCharacter(_currentTarget, distanceMovedThisFrame);
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
        isColiding = true;
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
            GUI.Label(new Rect(xpos, ypos, 100, 20), str, guiStyle);
            ypos += 20;
        }
    }
    
    
    

}
