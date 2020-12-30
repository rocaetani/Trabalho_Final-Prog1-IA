
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


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.black;

    }

    void Update()
    {
        Vector2 vel = new Vector2(0f,0f);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= 1f)
        {    
            vel = new Vector2(Input.GetAxis("Horizontal") * walkSpeed, 0);
        } 
        else if (Mathf.Abs(Input.GetAxis("Vertical")) >= 1f)
        {
            vel = new Vector2(0, Input.GetAxis("Vertical") * walkSpeed);
        }

        rigidbody.velocity = vel;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colidiu");
        Debug.Log(collision.collider.name);
        Debug.Log(collision.otherCollider.name);
         
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
            "Character Pos: " + transform.position.x + ", " + transform.position.y
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(xpos, ypos, 100, 20), str, guiStyle);
            ypos += 20;
        }
    }
    
    
    

}
