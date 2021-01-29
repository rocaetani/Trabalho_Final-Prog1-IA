using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    public GameObject prefabExit;
    private GameObject _character;
    private GridController _gridController;
    private MenuController _menuController;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    private bool _diamondGot;
    private Rock2 _rock;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //_tilemap = GetComponent<Tilemap>();
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _gridController = transform.parent.GetComponent<GridController>();
        _character = GameObject.FindGameObjectWithTag("Character");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _diamondGot = false;
        _rock = GetComponent<Rock2>();
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
        if (!_diamondGot)
        {
            _diamondGot = true;
            if (_menuController.PickDiamond())
            {
                StartCoroutine(ExitLabel());
            }

            Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
            _gridController.RemoveObject(objectPosition);
            _spriteRenderer.enabled = false;
            _collider2D.enabled = false;
            _rock.enabled = false;
        }
    }

    IEnumerator ExitLabel()
    {
        GameObject exitLabel = Instantiate(prefabExit, transform.position + Vector3.up, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Destroy(exitLabel);
        Destroy(gameObject);

    }

    /*
     private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    */

}
