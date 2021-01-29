using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit: MonoBehaviour
{
    public GameObject PrefabDGood;
    
    private GameObject _character;
    private GridController _gridController;
    private MenuController _menuController;
    private bool _diamondsCollected;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //_tilemap = GetComponent<Tilemap>();
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _gridController = transform.parent.GetComponent<GridController>();
        _character = GameObject.FindGameObjectWithTag("Character");
        _diamondsCollected = false;
        _audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_menuController.DiamondsLeft() <= 0)
        {
            _diamondsCollected = true;
        }

        if (_diamondsCollected)
        {
            Vector3 characterPosition = _character.transform.position;
            if (Input.GetAxis("Horizontal") > 0.5f)
            {
                Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x + 0.5f),
                    Mathf.RoundToInt(characterPosition.y));
                Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
                if (objectPosition.Equals(movePosition))
                {
                    GoToExit();
                }
            }

            if (Input.GetAxis("Horizontal") < -0.5f)
            {
                Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x - 0.5f),
                    Mathf.RoundToInt(characterPosition.y));
                Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
                if (objectPosition.Equals(movePosition))
                {
                    GoToExit();
                }
            }

            if (Input.GetAxis("Vertical") > 0.5f)
            {
                Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x),
                    Mathf.RoundToInt(characterPosition.y + 0.5f));
                Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
                if (objectPosition.Equals(movePosition))
                {
                    GoToExit();
                }
            }

            if (Input.GetAxis("Vertical") < -0.5f)
            {
                Vector2Int movePosition = new Vector2Int(Mathf.RoundToInt(characterPosition.x),
                    Mathf.RoundToInt(characterPosition.y - 0.5f));
                Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
                if (objectPosition.Equals(movePosition))
                {
                    GoToExit();
                }
            }
        }
    }

    private void GoToExit()
    {

        Instantiate(PrefabDGood, transform.position + Vector3.up, Quaternion.identity);
        _menuController.Win();
        
        _audioSource.Play();

        Destroy(gameObject);
    }

}
