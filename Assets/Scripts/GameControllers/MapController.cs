using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapController : MonoBehaviour
{

    private List<Stage> _stageList;

    public Transform Selector;

    private int _selectorIndex;

    private MenuController _menuController;

    public Canvas StageEntry;

    public TMP_Text LifeText;

    public TMP_Text StageText;

    private bool _stageSelected;
    // Start is called before the first frame update
    void Start()
    {
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _stageList = new List<Stage>();
        foreach (var stage in GetComponentsInChildren<Stage>())
        {
            _stageList.Add(stage);
        }

        foreach (var stageIndex in _menuController.StagesCleared)
        {
            _stageList[stageIndex].GetComponentInParent<SpriteRenderer>().enabled = false;
        }

        _selectorIndex = 0;
        _stageSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            VerifySelection(Direction.Down);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            VerifySelection(Direction.Up);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            VerifySelection(Direction.Right);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            VerifySelection(Direction.Left);
        }


        if (Input.GetKeyDown(KeyCode.X) & _selectorIndex != 0)
        {
            if(!_stageSelected){
                SelectStage();
            }
            else
            {
                EnterStage();
            }
            
        }
    }

    private void EnterStage()
    {
        
        Stage stage = _stageList[_selectorIndex];
        Debug.Log("" + stage.name);
        _menuController.NewScene(stage.SceneName, stage.sceneOptions, _selectorIndex);
    }

    private void VerifySelection(Direction direction)
    {
        if (_stageList[_selectorIndex].fromStage == direction)
        {
            _selectorIndex = _selectorIndex - 1;
            MoveSelector(_selectorIndex);
        }
        if (_stageList[_selectorIndex].toStage == direction)
        {
            _selectorIndex = _selectorIndex + 1;
            MoveSelector(_selectorIndex);
        }
    }

    private void MoveSelector(int stageIndex)
    {
        Selector.position = _stageList[stageIndex].position;
    }


    private void SelectStage()
    {
        SetStageInfo();
        StageEntry.enabled = true;
        _stageSelected = true;

    }

    private void SetStageInfo()
    {
        LifeText.text = "<sprite index=[40]>    <sprite index=[" + _menuController.life + "]>  ";
        StageText.text = "<sprite index=[" + _selectorIndex.ToString() + "]>";

    }
}
