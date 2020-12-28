
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

    public float walkSpeed = 10f;
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
        if (Input.GetAxis("Horizontal") > 0f)
        {
            _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x + 1), transform.position.y, 0);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            _currentTarget = new Vector3(Mathf.RoundToInt(transform.position.x - 1), transform.position.y, 0);
        }
        if (Input.GetAxis("Vertical") > 0f)
        {
            _currentTarget = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y + 1), 0);
        }
        if (Input.GetAxis("Vertical") < 0f)
        {
            _currentTarget = new Vector3(transform.position.x, Mathf.RoundToInt(transform.position.y - 1), 0);
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
    
    
    

}
