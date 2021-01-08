
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;



public class Move3 : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D _rigidbody;
    private float _lastPositionX;
    private float _andou;
    private int _xpos = 10;
    private GUIStyle _guiStyle = new GUIStyle();

    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _lastPositionX = transform.position.x;
        _andou = 0;
        _guiStyle.fontSize = 20;
        _guiStyle.normal.textColor = Color.blue;
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
    
        Vector2 vel = new Vector2(10, 0);
        _rigidbody.velocity = vel;

        _andou = transform.position.x - _lastPositionX;
        _lastPositionX = transform.position.x;
        Debug.Log("Character Pos: " + transform.position.x + ", " + transform.position.y);
        Debug.Log("Last Pos: " + _lastPositionX);
        Debug.Log("Andou: " + _andou);
        Debug.Log("Tempo: " + Time.deltaTime);


    }
    



    void OnGUI() {
        string[] strings = {
            "Horiz.: " + Input.GetAxis("Horizontal") ,
            "Vert. : " + Input.GetAxis("Vertical"),
            "Character Pos: " + transform.position.x + ", " + transform.position.y,
            "Last Pos: "  + _lastPositionX,
            "Andou: " + _andou,
            "Tempo: " + Time.deltaTime
            
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(_xpos, ypos, 100, 20), str, _guiStyle);
            ypos += 20;
        }
    }
    
    
    

}
