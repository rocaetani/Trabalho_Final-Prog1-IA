using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public String SceneName;

    public Direction toStage;

    public Direction fromStage;

    public Vector3 position;

    public SceneOptions sceneOptions;

    void Start()
    {
        position = transform.position;
        
        sceneOptions = GetComponent<SceneOptions>();
    }
}