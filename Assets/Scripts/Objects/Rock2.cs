using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class Rock2 : MonoBehaviour
{
    private GridController _gridController;
    private Vector2Int _startFallFrom;
    private Rigidbody2D _rigidbody2D;
    private RigidbodyConstraints2D _normalConstraints;
    private bool _fallControl;

    // Start is called before the first frame update
    void Start()
    {
        _gridController = transform.parent.GetComponent<GridController>();
        _startFallFrom = VectorTransformer.Vector3ToVector2Int(transform.position);
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //_rigidbody2D.velocity = new Vector2(0,-2);
        _normalConstraints = _rigidbody2D.constraints;
        _fallControl = false;

    }
    
    void Update()
    {

        Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
        Vector2Int downPosition = VectorTransformer.Vector2IntDown(objectPosition);

        if (!_fallControl)
        {
            _fallControl = true;
            StartCoroutine(WaitFall(objectPosition, downPosition));
        }

        if (!_gridController.CellIsEmptyWithoutCharacter(downPosition))
        {
            _startFallFrom = objectPosition;
        }

        //Se o objeto abaixo deste é Slider e na posição do lado e lado baixo estão vazias a pedra escorrega  
        if (_gridController.HasGridObjectAt(downPosition)) 
        {
            Transform downObject = _gridController.GetObject(downPosition);
            if (downObject.CompareTag("SlideObject"))//verifica se o de baixo é slider
            {
                if (!_gridController.HasNextFreeSpace(objectPosition,Direction.Down))
                {
                    Vector2Int leftPosition = VectorTransformer.Vector2IntLeft(objectPosition);
                    if(_gridController.CellIsEmpty(leftPosition)) //Verifica lado esquerdo
                    {
                        if (_gridController.CellIsEmpty(VectorTransformer.Vector2IntDown(leftPosition))) //Verifica esquerdo baixo
                        {
                            //Desliza
                            StartCoroutine(WaitSlide(objectPosition, leftPosition));
                            return;
                        }
                    }
                    Vector2Int rightPosition = VectorTransformer.Vector2IntRight(objectPosition); //Verifica lado direito
                    if(_gridController.CellIsEmpty(rightPosition))
                    {
                        if (_gridController.CellIsEmpty(VectorTransformer.Vector2IntDown(rightPosition))) //Verifica direita baixo
                        {
                            //Desliza
                            StartCoroutine(WaitSlide(objectPosition, rightPosition));
                            return;
                        }
                    }
                }
            }
        }
        if (_gridController.characterPosition.Equals(downPosition))
        {
            if (!_startFallFrom.Equals(VectorTransformer.NullPoint) & _startFallFrom.y > _gridController.characterPosition.y + 1)
            {
                _gridController.character.GetComponent<Lose>().InstantiateLost();
            }
        }
    }


    private void Slide(Vector2Int fromPosition, Vector2Int toPosition)
    {
        transform.position = VectorTransformer.Vector2IntToVector3Int(toPosition);
        _gridController.MoveObject(fromPosition, toPosition);
    }

    private bool VerifyCharacterBeforeSlide(Vector2Int position)
    {
        if (_gridController.IsCharacterOnCell(position) ||
            _gridController.IsCharacterOnCell(VectorTransformer.Vector2IntDown(position)))
        {
            return false;
        }

        return true;
    }

    IEnumerator WaitSlide(Vector2Int fromPosition, Vector2Int toPosition)
    {
        yield return new WaitForSeconds(0.5f);
        if (VerifyCharacterBeforeSlide(toPosition) & !AlreadySlideTo(toPosition))
        {
            Slide(fromPosition, toPosition);
        }
    }

    private bool AlreadySlideTo(Vector2Int toPosition)
    {
        if (toPosition.x == Mathf.FloorToInt(transform.position.x))
        {
            return true;
        }
        return false;
    }



    
    private void Fall(Vector2Int fromPosition, Vector2Int toPosition)
    {
        if (_gridController.CellIsEmpty(toPosition))
        {
            if(!_startFallFrom.Equals(VectorTransformer.NullPoint))
            {
                _startFallFrom = VectorTransformer.Vector3ToVector2Int(transform.position);
            }
            _gridController.MoveObject(fromPosition, toPosition);
            transform.position = VectorTransformer.Vector2IntToVector3Int(toPosition);
        }
    }
    
    IEnumerator WaitFall(Vector2Int fromPosition, Vector2Int toPosition)
    {
        yield return new WaitForSeconds(0.5f);
        Fall(fromPosition, toPosition);
        _fallControl = false;
    }


}
