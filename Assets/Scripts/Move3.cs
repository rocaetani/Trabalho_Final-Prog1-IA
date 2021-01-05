
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;



public class Move3 : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rigidbody;
    private float lastPositionX;
    private float andou;
    private int xpos = 10;
    private GUIStyle guiStyle = new GUIStyle();

    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastPositionX = transform.position.x;
        andou = 0;
        guiStyle.fontSize = 20;
        guiStyle.normal.textColor = Color.blue;
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
        rigidbody.velocity = vel;

        andou = transform.position.x - lastPositionX;
        lastPositionX = transform.position.x;
        Debug.Log("Character Pos: " + transform.position.x + ", " + transform.position.y);
        Debug.Log("Last Pos: " + lastPositionX);
        Debug.Log("Andou: " + andou);
        Debug.Log("Tempo: " + Time.deltaTime);


    }
    



    void OnGUI() {
        string[] strings = {
            "Horiz.: " + Input.GetAxis("Horizontal") ,
            "Vert. : " + Input.GetAxis("Vertical"),
            "Character Pos: " + transform.position.x + ", " + transform.position.y,
            "Last Pos: "  + lastPositionX,
            "Andou: " + andou,
            "Tempo: " + Time.deltaTime
            
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(xpos, ypos, 100, 20), str, guiStyle);
            ypos += 20;
        }
    }
    
    
    

}
