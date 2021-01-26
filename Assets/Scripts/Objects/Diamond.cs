using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private GameObject _character;
    private GridController _gridController;
    private MenuController _menuController;
    
    // Start is called before the first frame update
    void Start()
    {
        //_tilemap = GetComponent<Tilemap>();
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _gridController = transform.parent.GetComponent<GridController>();
        _character = GameObject.FindGameObjectWithTag("Character");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterPosition = _character.transform.position;
        if (Input.GetAxis("Horizontal") > 0.5f)
        {
            Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x + 0.5f), Mathf.RoundToInt(characterPosition.y));
            Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
            if (objectPosition.Equals(movePosition))
            {
                GetDiamond();
            }
        }
        if (Input.GetAxis("Horizontal") < -0.5f)
        {
            Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x - 0.5f), Mathf.RoundToInt(characterPosition.y));
            Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
            if (objectPosition.Equals(movePosition))
            {
                GetDiamond();
            }
        }
        if (Input.GetAxis("Vertical") > 0.5f)
        {
            Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x), Mathf.RoundToInt(characterPosition.y + 0.5f));
            Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
            if (objectPosition.Equals(movePosition))
            {
                GetDiamond();
            }
        }
        if (Input.GetAxis("Vertical") < -0.5f)
        {
            Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x), Mathf.RoundToInt(characterPosition.y - 0.5f));
            Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);            
            if (objectPosition.Equals(movePosition))
            {
                GetDiamond();
            }
        }
    }

    private void GetDiamond()
    {
        _menuController.PickDiamond();
        Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
        _gridController.RemoveObject(objectPosition);
        Destroy(gameObject);
    }

    /*
     private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    */

}
