using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    //Canvas Parameters
    private Canvas _menuCanvas;
    public TMP_Text DiamondsText;
    public TMP_Text TimeText;
    public TMP_Text ScoreText;
    public RawImage GameOverMessage;
    public RawImage PauseMessage;
    public RawImage TimeIsUpMessage;

    public Image DiamondsPanel;

    public GameObject prefabTimeUp;
    //Stage Parameters
    public List<int> StagesCleared;
    private int _currentStage;

    //Life Parameters
    public int life;
    
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
    public String IsInScene;
    //private bool _nextSceneControl;
    private bool _isPaused;

    public bool StopMove;
    
    private bool _timeUpControl;
    

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("MenuController").Length > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        StagesCleared = new List<int>();
        life = 3;
        _maxTime = 180;
        _diamondsLeft = 10;
        //_nextSceneControl = true;
        _timeStageStart = 0;
        _menuCanvas = GetComponent<Canvas>();
        IsInScene = "Map";
        _isPaused = false;
        _currentStage = 0;
        _timeUpControl = true;
        StopMove = false;
    }
    
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    
    void Update()
    {
        
        //Calculate Time Laft
        int timeRemaining = _maxTime - (int)Time.time + _timeStageStart;

        if (timeRemaining == 0 & _timeUpControl)
        {
            _timeUpControl = false;
            TimeIsUp();
        }

        
        
        //Show values on Menu
        TimeText.text = TransformToSpriteAsset(timeRemaining.ToString().PadLeft(3,'0'));
        DiamondsText.text = TransformToSpriteAsset(_diamondsLeft.ToString().PadLeft(3,'0'));
        ScoreText.text = TransformToSpriteAsset(_totalScore.ToString().PadLeft(6,'0'));
        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isPaused)
            {
                UnPause();
            }
            else
            {
                Pause(true);
            }

        }
    }
    
    public bool PickDiamond()
    {
        int diamondsStart = _diamondsLeft;
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

        if (diamondsStart == 1 & _diamondsLeft == 0)
        {
            DiamondsPanel.color = Color.green;
            return true;
        }

        return false;
    }

    private void TimeIsUp()
    {
        
        ShowTimeIsUp();
        StopMove = true;
        Lose();
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

    public void NewScene(String sceneName, SceneOptions newSceneOption, int newStage)
    {
        _currentStage = newStage;
        _menuCanvas.enabled = true;
        IsInScene = sceneName;
        SceneManager.LoadScene(sceneName);
        //_nextSceneControl = true;
        LoadSceneOptions(newSceneOption);
    }

    public void Win()
    {
        StagesCleared.Add(_currentStage);
        AddTimeToScore();
        GoToMap();
    }

    private void GameOver()
    {
        _menuCanvas.enabled = false;
        
        _totalScore = 0;
        life = 3;
        ShowGameOver();
        SceneManager.LoadScene("TitleScreen");
        HideGameOver();
        HideTimeIsUp();
    }

    public void Lose()
    {
        Debug.Log("Lose");
        if (life == 0)
        {
            //Game Over
            
            GameOver();
        }
        else
        {
            life = life - 1;
            StartCoroutine(WaitGotoMap(3f));
        }
    }

    public void GoToMap()
    {
        IsInScene = "Map";
        _menuCanvas.enabled = false;
        _timeUpControl = true;
        SceneManager.LoadScene("Map");
        HideGameOver();
        HideTimeIsUp();
        DiamondsPanel.color = Color.white;
        StopMove = false;
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

    private void ShowGameOver()
    {
        GameOverMessage.enabled = true;
    }
    
    private void HideGameOver()
    {
        GameOverMessage.enabled = false;
    }
    
    
    
    IEnumerator WaitGotoMap(float timeSeconds)
    {

        yield return new WaitForSeconds(timeSeconds);
        GoToMap();
 

    }

    private String TransformToSpriteAsset(String text)
    {
        char[] charArr = text.ToCharArray();
        String textReturn = "";
        foreach (char value in charArr)
        {
            textReturn = textReturn + "<sprite index=[" + value + "]>";
        }

        return textReturn;
    }

    public int DiamondsLeft()
    {
        return _diamondsLeft;
    }

    private void Pause(bool show)
    {
        _isPaused = true;
        if (show)
        {
            ShowPause();
        }

        Time.timeScale = 0;
    }

    private void UnPause()
    {
        HidePause();
        Time.timeScale = 1;
        _isPaused = false;
    }
    
    private void ShowPause()
    {
        PauseMessage.enabled = true;
    }
    
    private void HidePause()
    {
        PauseMessage.enabled = false;
    }
    
    private void ShowTimeIsUp()
    {
        TimeIsUpMessage.enabled = true;
    }
    
    private void HideTimeIsUp()
    {
        TimeIsUpMessage.enabled = false;
    }

    private void AddTimeToScore()
    {
        _totalScore = _totalScore + _maxTime - (int)Time.time + _timeStageStart;
    }

}
