using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private Canvas _menuCanvas;
    public TMP_Text DiamondsText;
    public TMP_Text TimeText;


    private int _diamondsLeft;
    private int _initialTime;

    private bool _nextSceneControl;

    private int _timeStageStart;

    private String _isInScene;
    
    void Start()
    {
        _initialTime = 180;
        _diamondsLeft = 10;
        _nextSceneControl = true;
        _timeStageStart = 0;
        _menuCanvas = GetComponent<Canvas>();
        _isInScene = "Map";
    }
    
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    void Update()
    {
        
        if (_nextSceneControl & !_isInScene.Equals("Map"))
        {
            _nextSceneControl = false;
            SceneOptions newSceneOption = GameObject.FindGameObjectWithTag("SceneOptions").GetComponent<SceneOptions>();
            _diamondsLeft = newSceneOption.diamondsNeeded;
            _initialTime = newSceneOption.maxTime;
            _timeStageStart = (int)Time.time;
        }
        
        int _time = _initialTime - (int)Time.time + _timeStageStart;
        
        if (_time == 0)
        {
            Lose();
        }

        
        
        //Debug.Log("Seconds: " + _time);
        TimeText.text = _time.ToString().PadLeft(3,'0');
        DiamondsText.text = _diamondsLeft.ToString().PadLeft(3,'0');
        
        if (Input.GetKey(KeyCode.A))
        {
            Win();
        }
        if (Input.GetKey(KeyCode.S))
        {
            Lose();
        }
    }
    
    public void PickDiamond()
    {
        _diamondsLeft = _diamondsLeft - 1;
    }
    
    public void NewScene(String sceneName)
    {
        _menuCanvas.enabled = true;
        _isInScene = sceneName;
        SceneManager.LoadScene(sceneName);
        _nextSceneControl = true;
    }

    public void Win()
    {
        Debug.Log("Win");
        GoToMap();
    }

    public void Lose()
    {
        Debug.Log("Lose");
        GoToMap();
    }

    public void GoToMap()
    {
        _isInScene = "Map";
        _menuCanvas.enabled = false;
        SceneManager.LoadScene("Map");
    }
}
