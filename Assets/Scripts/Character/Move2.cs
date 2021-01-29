
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

    public bool teste;
    public float walkSpeed = 1000000f;
    Rigidbody2D _rigidbody;
    public bool _isMoving;
    public Direction _movingDirection;
    public Vector2 _target;

    public bool DontMove;

    private Vector2 _velocity;

    private bool _isPaused;

    private MenuController _menuController;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = new Vector2(transform.position.x, transform.position.y);
        _velocity = new Vector2(0f,0f);
        DontMove = false;
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();


    }

    void Update()
    {
        
        if (!_menuController.StopMove)
        {
            Vector2 characterPosition = VectorTransformer.Vector3ToVector2(transform.position);

            if (Input.GetAxis("Horizontal") > 0.5f & _movingDirection == Direction.None &
                ValidatePosition(characterPosition.y))
            {
                _movingDirection = Direction.Right;
                _velocity = new Vector2(1 * walkSpeed, 0);
                _isMoving = true;
            }
            else if (Input.GetAxis("Horizontal") < -0.5f & _movingDirection == Direction.None &
                     ValidatePosition(characterPosition.y))
            {
                _movingDirection = Direction.Left;
                _velocity = new Vector2(-1 * walkSpeed, 0);
                _isMoving = true;
            }
            else if (Input.GetAxis("Vertical") > 0.5f & _movingDirection == Direction.None &
                     ValidatePosition(characterPosition.x))
            {
                _movingDirection = Direction.Up;
                _velocity = new Vector2(0, 1 * walkSpeed);
                _isMoving = true;
            }
            else if (Input.GetAxis("Vertical") < -0.5f & _movingDirection == Direction.None &
                     ValidatePosition(characterPosition.x))
            {
                _movingDirection = Direction.Down;
                _velocity = new Vector2(0, -1 * walkSpeed);
                _isMoving = true;
            }
            else if (_isMoving)
            {
                switch (_movingDirection)
                {
                    case Direction.Right:
                        _target = new Vector2(Mathf.RoundToInt(characterPosition.x + 1f),
                            Mathf.RoundToInt(characterPosition.y));
                        break;
                    case Direction.Left:
                        _target = new Vector2(Mathf.RoundToInt(characterPosition.x - 1f),
                            Mathf.RoundToInt(characterPosition.y));
                        break;
                    case Direction.Up:
                        _target = new Vector2(Mathf.RoundToInt(characterPosition.x),
                            Mathf.RoundToInt(characterPosition.y + 1f));
                        break;
                    case Direction.Down:
                        _target = new Vector2(Mathf.RoundToInt(characterPosition.x),
                            Mathf.RoundToInt(characterPosition.y - 1f));
                        break;

                }

                _isMoving = false;
            }
            else if (!characterPosition.Equals(_target))
            {
                float distance = Mathf.Abs(characterPosition.x - _target.x) +
                                 Mathf.Abs(characterPosition.y - _target.y);
                if (distance < 0.2)
                {
                    transform.position = _target;
                    _velocity = new Vector2(0, 0);
                    _movingDirection = Direction.None;
                }
            }

            _rigidbody.velocity = _velocity;
        }
    }

    private bool ValidatePosition(float value)
    {
        if (Mathf.Abs(value - Mathf.RoundToInt(value)) < 0.08)
        {
            teste = true;
            return true;
            
        }

        teste = false;
        return false;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        _movingDirection = Direction.None;
        Debug.Log("Está Colidinddo");
    }

    private void OnCollision2D(Collision2D collision)
    {
        _movingDirection = Direction.None;
        
    }
    
    

}
