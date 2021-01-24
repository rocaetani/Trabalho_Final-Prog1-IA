using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public TMP_Text DiamondsText;
    public TMP_Text TimeText;




    private int _diamondsLeft;
    private int _diamondsTotal;
    private int _initialTime;
    // Start is called before the first frame update
    void Start()
    {
        _diamondsTotal = 10;
        _initialTime = 180;
        _diamondsLeft = _diamondsTotal;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Seconds: " + Time.time);
        int _time = _initialTime - (int)Time.time;
        //Debug.Log("Seconds: " + _time);
        TimeText.text = _time.ToString().PadLeft(3,'0');
        DiamondsText.text = _diamondsLeft.ToString().PadLeft(3,'0');
    }

    public void AddDimond()
    {
        _diamondsLeft = _diamondsLeft - 1;
    }

    public void EraseDimonds()
    {
        _diamondsLeft = _diamondsLeft;
    }
}
