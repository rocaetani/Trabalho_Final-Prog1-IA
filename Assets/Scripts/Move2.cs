
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Move2 : MonoBehaviour
{
    // Start is called before the first frame update

    public float walkSpeed = 1000000f;
    Rigidbody2D rigidbody;
    private int xpos = 10;
    private GUIStyle guiStyle = new GUIStyle();
    private bool isMoving;
    private Direction _movingDirection;
    private Vector2 target;

    private Vector2 _velocity;
   
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.blue;
        target = new Vector2(transform.position.x, transform.position.y);
        _velocity = new Vector2(0f,0f);

    }

    void Update()
    {
        Vector2 characterPosition = new Vector2(transform.position.x, transform.position.y);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 1f & _movingDirection == Direction.None & ValidatePosition(characterPosition.y))
        {
            if (Input.GetAxis("Horizontal") >= 1f)
            {
                _movingDirection = Direction.Right;
            }
            else
            {
                _movingDirection = Direction.Left;
            }
            _velocity = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, 0);
            isMoving = true;
        } 
        else if (Mathf.Abs(Input.GetAxis("Vertical")) >= 1f & _movingDirection == Direction.None & ValidatePosition(characterPosition.x))
        {
            if (Input.GetAxis("Vertical") >= 1f)
            {
                _movingDirection = Direction.Up;
            }
            else
            {
                _movingDirection = Direction.Down;
            }
            _velocity = new Vector2(0, Input.GetAxis("Vertical") * walkSpeed);
            isMoving = true;
        }
        else if (isMoving)
        {
            switch (_movingDirection)
            {
                case Direction.Right: target = new Vector2(Mathf.RoundToInt(characterPosition.x + 1f), characterPosition.y);
                    break;
                case Direction.Left: target = new Vector2(Mathf.RoundToInt(characterPosition.x - 1f), characterPosition.y);
                    break;
                case Direction.Up: target = new Vector2(characterPosition.x, Mathf.RoundToInt(characterPosition.y + 1f));
                    break;
                case Direction.Down: target = new Vector2(characterPosition.x, Mathf.RoundToInt(characterPosition.y - 1f));
                    break;
                    
            }
            isMoving = false;
            /*
            if (_movingDirection == Direction.Right)
            {
                target = new Vector2(Mathf.RoundToInt(characterPosition.x + 1f), characterPosition.y);
                isMoving = false;
            }
            if (_movingDirection == Direction.Right)
            {
                target = new Vector2(Mathf.RoundToInt(characterPosition.x + 1f), characterPosition.y);
                isMoving = false;
            }
            if (_movingDirection == Direction.Right)
            {
                target = new Vector2(Mathf.RoundToInt(characterPosition.x + 1f), characterPosition.y);
                isMoving = false;
            }
            if (_movingDirection == Direction.Right)
            {
                target = new Vector2(Mathf.RoundToInt(characterPosition.x + 1f), characterPosition.y);
                isMoving = false;
            }*/
        }
        else if (!characterPosition.Equals(target))
        {
            float distance = Mathf.Abs(characterPosition.x - target.x) + Mathf.Abs(characterPosition.y - target.y);
            if (distance < 0.2)
            {
                transform.position = target;
                _velocity = new Vector2(0, 0);
                _movingDirection = Direction.None;
            }
        }

/*
        if (_velocity.x != 0 & (_movingDirection == Direction.Up || _movingDirection == Direction.Down))
        {
            return;
        }
        if (_velocity.y != 0 & (_movingDirection == Direction.Right || _movingDirection == Direction.Left))
        {
            return;
        }*/
        rigidbody.velocity = _velocity;
    }

    private bool ValidatePosition(float value)
    {
        if (Mathf.Abs(value - Mathf.RoundToInt(value)) < 0.08)
        {
            return true;
        }
        return false;
    }
    
    


    private void OnCollisionStay2D(Collision2D other)
    {
        _movingDirection = Direction.None;
        /*
        Vector3 adjustPosition = Vector3Int.FloorToInt(transform.position);
        transform.position = adjustPosition;
        */
    }

    private void OnCollision2D(Collision2D collision)
    {
        _movingDirection = Direction.None;
    }

    void OnGUI() {
        GUI.backgroundColor = Color.yellow;
        string[] strings = {
            "Horiz.: " + Input.GetAxis("Horizontal") ,
            "Vert. : " + Input.GetAxis("Vertical"),
            "M. Pos: " + Input.mousePosition,
            "Character Pos: " + transform.position.x + ", " + transform.position.y
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(xpos, ypos, 100, 20), str, guiStyle);
            ypos += 20;
        }
    }
    
    
    

}
