using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    
    Animator animator;
    private MenuController _menuController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _menuController = GameObject.FindGameObjectWithTag("MenuController").GetComponent<MenuController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!_menuController.StopMove)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                animator.SetInteger("CharacterAnimationController", 1);
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                animator.SetInteger("CharacterAnimationController", 2);
            }

            if (Input.GetAxis("Vertical") > 0f)
            {
                animator.SetInteger("CharacterAnimationController", 3);
            }

            if (Input.GetAxis("Vertical") < 0f)
            {
                animator.SetInteger("CharacterAnimationController", 4);
            }

            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            {
                animator.SetInteger("CharacterAnimationController", 0);
            }
        }
    }
        
        
}
