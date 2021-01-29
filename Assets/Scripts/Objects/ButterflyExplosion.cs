using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyExplosion : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject prefabStar;
    public GameObject prefabBackground;
    
    private MenuController _menuController;
    private GridController _gridController;




    void Start()
    {
        
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();

    }




    public void Explode()
    {
        Vector3 position = transform.position;
        Vector2Int positionVector2Int = VectorTransformer.Vector3ToVector2Int(position);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.down);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.up);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.left);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.right);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.down + Vector2Int.left);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.up + Vector2Int.left);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.down + Vector2Int.right);
        _gridController.DestroyObjectAt(positionVector2Int + Vector2Int.up + Vector2Int.right);
            
            
        
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

    IEnumerator DestroyStar(GameObject star, GameObject background)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(star);
        Destroy(background);
        
    }

    private void CreateStar(Vector3 position)
    {
        GameObject background = Instantiate(prefabBackground, position, Quaternion.identity);
        GameObject star = Instantiate(prefabStar, position, Quaternion.identity);
        StartCoroutine(DestroyStar(star, background));

    }
}
