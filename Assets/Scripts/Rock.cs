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
    private Vector2Int _startFallFrom;
    
    // Start is called before the first frame update
    void Start()
    {
        _gridController = transform.parent.GetComponent<GridController>();
        _fallList = new List<Vector2Int>();
        _isFalling = false;

        _startFallFrom = VectorTransformer.NullPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int objectPosition = VectorTransformer.Vector3ToVector2Int(transform.position);
        Vector2Int downPosition = VectorTransformer.Vector2IntDown(objectPosition);
        //Se o objeto abaixo deste é Slider e na posição do lado e lado baixo estão vazias a pedra escorrega
        if (_gridController.HasGridObjectAt(downPosition))
        {
            //verifica se o de baixo é slider
            Transform downObject = _gridController.GetObject(downPosition);
            if (downObject.tag.Equals("SlideObject"))
            {
                //Verifica lado esquerdo
                Vector2Int leftPosition = VectorTransformer.Vector2IntLeft(objectPosition);
                if(_gridController.CellIsEmpty(leftPosition))
                {
                    //Verifica esquerdo baixo
                    if (_gridController.CellIsEmpty(VectorTransformer.Vector2IntDown(leftPosition)))
                    {
                        //Desliza
                        StartCoroutine(WaitSlide(leftPosition));
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
                        StartCoroutine(WaitSlide(rightPosition));
                    }
                }
            }
        }
        else if(!_isFalling & _gridController.CellIsEmpty(downPosition))
        {
            _isFalling = true;
            StartCoroutine(WaitFall(downPosition));
            //Debug.Log(_gridController.HasGridObjectAt(downPosition));
            Debug.Log(transform.name + ": " + objectPosition + " - " + Time.frameCount);
            _startFallFrom = objectPosition;

        }
    }


    private void Slide(Vector2Int toPosition)
    {
        transform.position = VectorTransformer.Vector2IntToVector3Int(toPosition);
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

    IEnumerator WaitSlide(Vector2Int toPosition)
    {
        yield return new WaitForSeconds(0.5f);
        if (VerifyCharacterBeforeSlide(toPosition) & !AlredySlideTo(toPosition))
        {
            Slide(toPosition);
        }
    }

    private bool AlredySlideTo(Vector2Int toPosition)
    {
        if (toPosition.x == Mathf.FloorToInt(transform.position.x))
        {
            return true;
        }
        return false;
    }
    
    
    
    private void Fall(Vector2Int toPosition)
    {
        transform.position = VectorTransformer.Vector2IntToVector3Int(toPosition);
        _fallList.Add(toPosition);
    }

    private bool VerifyCharacterBeforeFall(Vector2Int position)
    {
        if (_gridController.IsCharacterOnCell(position))
        {
            return false;
        }

        return true;
    }

    IEnumerator WaitFall(Vector2Int toPosition)
    {
        yield return new WaitForSeconds(0.5f);
        if (VerifyCharacterBeforeFall(toPosition) & !AlredyFallTo(toPosition))
        {
            Fall(toPosition);
        }
        _isFalling = false;
    }

    private bool AlredyFallTo(Vector2Int toPosition)
    {
        if (_fallList.Contains(toPosition))
        {
            return true;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collison)
    {
        if (collison.gameObject.tag == "Character")
        {
            Vector2Int characterPosition = _gridController.characterPosition;
            if (_startFallFrom.y > characterPosition.y + 1)
            {
                Debug.Log("Caiu na Cabeça");
            }
        }

        _startFallFrom = VectorTransformer.NullPoint;

    }
    
}
