using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInputs : MonoBehaviour
{
    
    private int _xpos = 10;
    private GUIStyle _guiStyle = new GUIStyle();
    public GameObject target;
    void Start() {
        _guiStyle.fontSize = 20;
        _guiStyle.normal.textColor = Color.yellow;
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
            "Character Pos: " + target.transform.position.x + ", " + target.transform.position.y,
            "Character velocity: " + target.GetComponent<Rigidbody2D>().velocity.x
        };
        int ypos = 10;
        foreach(string str in strings) {
            GUI.Label(new Rect(_xpos, ypos, 100, 20), str, _guiStyle);
            ypos += 20;
        }
    }

}
