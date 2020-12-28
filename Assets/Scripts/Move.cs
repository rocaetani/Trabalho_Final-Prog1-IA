
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

public class Move : MonoBehaviour
{
    // Start is called before the first frame update

    public float walkSpeed = 5f;
    Rigidbody2D rigidbody;
    float deltaX = 0;
    private int xpos = 10;
    private GUIStyle guiStyle = new GUIStyle();

    private Vector3 _currentTarget;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        _currentTarget = transform.position;
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.yellow;

    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") >= 1f)
        {
            _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x + 1), transform.position.y, 0);
        }
        

        Vector3 direction = _currentTarget - transform.position;
        MoveCharacter(walkSpeed * direction * Time.deltaTime);
    }


    void MoveCharacter(Vector3 frameMovement)
    {
        transform.position += frameMovement;
    }
    
    
    
    void OnGUI() {
        string[] strings = {
            "Horiz.: " + Input.GetAxis("Horizontal") ,
            "Vert. : " + Input.GetAxis("Vertical"),
            "Fire1 : " + Input.GetButton("Fire1"),
            "Fire2 : " + Input.GetButton("Fire2"),
            "Fire3 : " + Input.GetButton("Fire3"),
            "MouseX: " + Input.GetAxis("Mouse X"),
            "MouseY: " + Input.GetAxis("Mouse Y"),
            "M. Pos: " + Input.mousePosition,
            "Character Pos: " + transform.position.x + ", " + transform.position.y,
            "Target: " + _currentTarget
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(xpos, ypos, 100, 20), str, guiStyle);
            ypos += 20;
        }
    }
    
    
    
    
    /*
     
    void FixedUpdate()
    {
        //Feito desta maneria poruqe notei que o jogo tem um atraso a começar se mover e a velocidade do personagem é sempre constante
        if (Input.GetAxis("Horizontal") >= 1f)
        {
            Vector2 vel = new Vector2(5, 0);
            rigidbody.velocity = vel;
        }
        else if (!isInteger(transform.position.x))
        {
            if (getMantissa(transform.position.x) > 0.9f)
            {
                Vector2 vel = new Vector2(0, rigidbody.velocity.y);
                rigidbody.velocity = vel;
                transform.position = new Vector2(Mathf.RoundToInt(transform.position.x) + 1f, transform.position.y);
            }
            else
            {
                Vector2 vel = new Vector2(5, 0);
                rigidbody.velocity = vel;
            }
        }
        else
        {
            Vector2 vel = new Vector2(0, rigidbody.velocity.y);
            rigidbody.velocity = vel; 
        }
        /*
        else if (!isInteger(transform.position.x))
        {
            
            isMoving = false;
            newSquare = Mathf.FloorToInt(transform.position.x) + 1;
            
        }
    
            
        
        /*
        if (Input.GetAxis("Horizontal") >= -1f)
        {
            Vector2 vel = new Vector2(deltaX, 0);
            rigidbody.velocity = vel;
            isMoving = Direction.Left;
        }
        else
        {
            Vector2 vel = new Vector2(0, rigidbody.velocity.y);
            rigidbody.velocity = vel; 
        }
        



    }
    
    */

    bool isInteger(float value)
    {
        if (Mathf.RoundToInt(value) == value)
        {
            return true;
        }

        return false;
    }

    float getMantissa(float value)
    {
        return value - Mathf.RoundToInt(value);
    }

}
