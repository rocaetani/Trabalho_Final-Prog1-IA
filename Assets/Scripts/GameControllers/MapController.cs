using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    private List<Stage> _stageList;

    public Transform Selector;

    private int _selectorIndex;

    private MenuController _menuController;
    // Start is called before the first frame update
    void Start()
    {
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _stageList = new List<Stage>();
        foreach (var stage in GetComponentsInChildren<Stage>())
        {
            _stageList.Add(stage);
        }

        _selectorIndex = 0;
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

        if (Input.GetButton("Submit1") & _selectorIndex != 0)
        {
            Debug.Log("Vai para Cena: " + _stageList[_selectorIndex].SceneName);
            _menuController.NewScene(_stageList[_selectorIndex].SceneName);
        }
        if (Input.GetKey(KeyCode.X) & _selectorIndex != 0)
        {
            Debug.Log("Vai para Cena: " + _stageList[_selectorIndex].SceneName);
            _menuController.NewScene(_stageList[_selectorIndex].SceneName);
        }
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
}
