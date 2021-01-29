using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPaused : MonoBehaviour
{
    public bool isPaused;
    
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
