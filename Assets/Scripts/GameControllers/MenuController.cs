using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    //Canvas Parameters
    private Canvas _menuCanvas;
    public TMP_Text DiamondsText;
    public TMP_Text TimeText;
    public TMP_Text ScoreText;

    //Score Parameters
    private int _totalScore;
    private int _firstDiamondScore;
    private int _secondDiamondScore;

    //Diamond Parameters
    private int _diamondsLeft;
    
    //Time Parameters
    private int _maxTime;
    private int _timeStageStart;

    //Control Parameters
    public String _isInScene;
    private bool _nextSceneControl;
    
    void Start()
    {
        _maxTime = 180;
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
        //If is a Stage Load SceneOptions once
        /*
        if (_nextSceneControl & !_isInScene.Equals("Map"))
        {
            _nextSceneControl = false;
            //LoadSceneOptions();
        }
        */
        
        //Calculate Time Laft
        int timeRemaining = _maxTime - (int)Time.time + _timeStageStart;
        
        if (timeRemaining == 0)
        {
            Lose();
        }

        
        
        //Show values on Menu
        TimeText.text = timeRemaining.ToString().PadLeft(3,'0');
        DiamondsText.text = _diamondsLeft.ToString().PadLeft(3,'0');
        ScoreText.text = _totalScore.ToString().PadLeft(6,'0');
        
        //Test
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
        if (!DiamondThresholdPass())
        {
            _diamondsLeft = _diamondsLeft - 1;
            Score(_firstDiamondScore);
        }
        else
        {
            Score(_secondDiamondScore);
            //Make Menu Shine
            //Free Exit
        }
    }

    private bool DiamondThresholdPass()
    {
        if (_diamondsLeft == 0)
        {
            return true;
            
        }

        return false;
    }

    private void Score(int value)
    {
        _totalScore = _totalScore + value;
    }

    public void NewScene(String sceneName, SceneOptions newSceneOption)
    {
        _menuCanvas.enabled = true;
        _isInScene = sceneName;
        SceneManager.LoadScene(sceneName);
        //_nextSceneControl = true;
        LoadSceneOptions(newSceneOption);
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
        //_menuCanvas.enabled = false;
        SceneManager.LoadScene("Map");
    }

    private void LoadSceneOptions(SceneOptions newSceneOption)
    {
        //SceneOptions newSceneOption = GameObject.FindGameObjectWithTag("SceneOptions").GetComponent<SceneOptions>();
        
        //Score Parameters

        _firstDiamondScore = newSceneOption.firstDiamondScore;
        _secondDiamondScore = newSceneOption.secondDiamondScore;

        //Diamond Parameters
        _diamondsLeft = newSceneOption.diamondsNeeded;
    
        //Time Parameters
        _maxTime = newSceneOption.maxTime;
        _timeStageStart = (int)Time.time;
        
    }
}
