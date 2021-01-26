using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public String SceneName;

    public Direction toStage;

    public Direction fromStage;

    public Vector3 position;

    void Start()
    {
        position = transform.position;
    }
}