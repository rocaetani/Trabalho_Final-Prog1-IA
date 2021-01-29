using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject prefabStar;
    public GameObject prefabBackground;

    public GameObject prefabCharacterLose;
    private Transform _characterLose;

    private MenuController _menuController;
    private GridController _gridController;
    private bool _loseControl;

    private Vector3 _jumpTo;
    public float jumpSpeed;
    private bool _startFall;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    

    void Start()
    {
        
        _loseControl = true;
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();
        _gridController = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridController>();
        _startFall = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (_menuController.IsInScene.Equals("Map"))
        {
            _loseControl = true;
        }
        if(!_loseControl)
        {
            if (_characterLose.position.y > _jumpTo.y)
            {
                _startFall = true;
            }

            if (!_startFall)
            {
                _characterLose.transform.position += new Vector3(0, 1 * Time.deltaTime * (jumpSpeed - 3), 0);
            }
            else
            {
                _characterLose.transform.position += new Vector3(0, -1 * Time.deltaTime * (jumpSpeed + 3), 0);
            }

        }
        
    }

    // This script will simply instantiate the Prefab when the game starts.
    public void StageLose()
    {
        
        if (_loseControl)
        {
            Explode();
            _menuController.Lose();
            _loseControl = false;
            _jumpTo = transform.position + new Vector3(0,3,0);
        }

    }

    public void Explode()
    {
        _audioSource.Play();
        _spriteRenderer.enabled = false;
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

        _characterLose = Instantiate(prefabCharacterLose, position, Quaternion.identity).transform;

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
