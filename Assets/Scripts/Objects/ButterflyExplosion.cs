using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyExplosion : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject prefabStar;
    public GameObject prefabBackground;
    public GameObject prefabDiamond;
    private MenuController _menuController;
    private GridController _gridController;

    private SpriteRenderer _spriteRenderer;




    void Start()
    {
        
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
        Vector2Int upPosition = VectorTransformer.Vector2IntUp(objectPosition);

        if (Input.GetKey(KeyCode.L))
        {
            Explode();
        }

        if (_gridController.HasGridObjectAt(Vector2Int.up))
        {
            Transform upObject = _gridController.GetObject(upPosition);
            if (upObject.CompareTag("Rock"))
            {
                Debug.Log("Explodiu");
                Explode();
            }
        }
    }




    public void Explode()
    {
        _spriteRenderer.enabled = false;
        _gridController.RemoveObject(VectorTransformer.Vector3ToVector2Int(transform.position));
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

    IEnumerator DestroyStar(GameObject star, GameObject background, Vector3 position)
    {
        yield return new WaitForSeconds(0.4f);
        
        Destroy(star);
        Destroy(background);
        if(_gridController.CellIsEmpty(VectorTransformer.Vector3ToVector2Int(position))){
            GameObject diamond = Instantiate(prefabDiamond, position, Quaternion.identity);
            _gridController.AddObjectToGrid(diamond.transform);
        }

    }

    private void CreateStar(Vector3 position)
    {
        GameObject background = Instantiate(prefabBackground, position, Quaternion.identity);
        GameObject star = Instantiate(prefabStar, position, Quaternion.identity);
        StartCoroutine(DestroyStar(star, background, position));

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            if(Mathf.RoundToInt(other.gameObject.transform.position.y) > Mathf.RoundToInt(transform.position.y))
            {
                Explode();
            }
        }
    }
}
