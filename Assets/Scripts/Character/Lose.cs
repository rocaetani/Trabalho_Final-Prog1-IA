using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject prefabStar;
    public GameObject prefabBackground;
    
    // This script will simply instantiate the Prefab when the game starts.
    public void InstantiateLost()
    {
        Vector3 position = transform.position;
        CreateStar(position);
        CreateStar(position + Vector3.down);
        CreateStar(position + Vector3.up);
        CreateStar(position + Vector3.left);
        CreateStar(position + Vector3.right);
        CreateStar(position + Vector3.down + Vector3.left);
        CreateStar(position + Vector3.up + Vector3.left);
        CreateStar(position + Vector3.down + Vector3.right);
        CreateStar(position + Vector3.up + Vector3.right);
    }

    private void CreateStar(Vector3 position)
    {
        Instantiate(prefabStar, position, Quaternion.identity);
        Instantiate(prefabBackground, position, Quaternion.identity);
    }
}
