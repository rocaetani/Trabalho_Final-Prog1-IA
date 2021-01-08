using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class Rock : MonoBehaviour
{
    private GridController _gridController;
    private List<Vector2Int> _fallList;
    private bool _isFalling;
    private bool _downReallyFree;
    private Vector2Int _startFallFrom;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gridController = transform.parent.GetComponent<GridController>();
        _fallList = new List<Vector2Int>();
        _isFalling = false;
        _downReallyFree = true;
        _startFallFrom = VectorTransformer.NullPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
        Debug.Log(objectPosition + "-" + Time.frameCount);
        Vector2Int downPosition = VectorTransformer.Vector2IntDown(objectPosition);
        if (_gridController.CellIsEmpty(downPosition))
        {
            if (!_isFalling & _downReallyFree)
            {
                if (!AlreadyFallTo(downPosition))
                {
                    _isFalling = true;
                    Debug.Log("Fall" + "-" + Time.frameCount);
                    //Debug.Log(transform.name + ": " + "_isFalling: " + _isFalling);
                    StartCoroutine(WaitFall(objectPosition, downPosition));
                    //Debug.Log(_gridController.HasGridObjectAt(downPosition));
                    //Debug.Log("Fall" + "-" + Time.frameCount);
                    _startFallFrom = objectPosition;
                }
            }
        }
        //Se o objeto abaixo deste é Slider e na posição do lado e lado baixo estão vazias a pedra escorrega  
        else if (_gridController.HasGridObjectAt(downPosition)) 
        {
            //verifica se o de baixo é slider
            Transform downObject = _gridController.GetObject(downPosition);
            if (downObject.tag.Equals("SlideObject"))
            {
                if (_gridController.CellIsEmpty(VectorTransformer.Vector2IntDown(downPosition)))
                {
                    _downReallyFree = false;
                    StartCoroutine(WaitHalfSecond());
                    return;
                }

                //Verifica lado esquerdo
                Vector2Int leftPosition = VectorTransformer.Vector2IntLeft(objectPosition);
                if(_gridController.CellIsEmpty(leftPosition))
                {
                    //Verifica esquerdo baixo
                    if (_gridController.CellIsEmpty(VectorTransformer.Vector2IntDown(leftPosition)))
                    {
                        //Desliza
                        StartCoroutine(WaitSlide(objectPosition, leftPosition));
                        return;
                    }
                }
                //Verifica lado direito
                Vector2Int rightPosition = VectorTransformer.Vector2IntRight(objectPosition);
                if(_gridController.CellIsEmpty(rightPosition))
                {
                    //Verifica direita baixo
                    if (_gridController.CellIsEmpty(VectorTransformer.Vector2IntDown(rightPosition)))
                    {
                        //Desliza
                        StartCoroutine(WaitSlide(objectPosition, rightPosition));
                        return;
                    }
                }
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
        transform.position = VectorTransformer.Vector2IntToVector3Int(toPosition);
        _fallList.Add(toPosition);
        _gridController.MoveObject(fromPosition, toPosition);
    }

    private bool VerifyCharacterBeforeFall(Vector2Int position)
    {
        if (_gridController.IsCharacterOnCell(position))
        {
            return false;
        }

        return true;
    }

    IEnumerator WaitFall(Vector2Int fromPosition, Vector2Int toPosition)
    {
        yield return new WaitForSeconds(0.5f);
        if (VerifyCharacterBeforeFall(toPosition) & !AlreadyFallTo(toPosition))
        {
            Fall(fromPosition, toPosition);
        }
        _isFalling = false;
        //Debug.Log(transform.name + ": " + "WaitFall: " + _isFalling);
    }

    private bool AlreadyFallTo(Vector2Int toPosition)
    {
        if (_fallList.Contains(toPosition))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        if (collison.gameObject.CompareTag("Character"))
        {
            Vector2Int characterPosition = _gridController.characterPosition;
            if (_startFallFrom.y > characterPosition.y + 1)
            {
                Debug.Log("Caiu na Cabeça");
            }
        }

        _startFallFrom = VectorTransformer.NullPoint;

    }
    
    IEnumerator WaitHalfSecond()
    {
        yield return new WaitForSeconds(0.5f);
        _downReallyFree = true;
        //Debug.Log(transform.name + ": " + "WaitHalfSecond: " + _downReallyFree);
    }
    
}
