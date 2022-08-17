using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            animator.SetBool("isDriving", true);
        }
        else
        {
            animator.SetBool("isDriving", false);
        }

        if (Input.GetKey("left"))
        {
            animator.SetBool("isLeftTurning", true);
        }
        else
        {
            animator.SetBool("isLeftTurning", false);
        }

        if (animator.GetBool("isLeftTurning")==true&& animator.GetBool("isDriving") == true)
        {
            animator.SetBool("isDriving", false);
        }
        else
        {
            //animator.SetBool("isLeftTurning", false);
        }

        if (Input.GetKey("right"))
        {
            animator.SetBool("isRightTurning", true);
        }
        else
        {
            animator.SetBool("isRightTurning", false);
        }

        if (animator.GetBool("isRightTurning") == true && animator.GetBool("isDriving") == true)
        {
            animator.SetBool("isDriving", false);
        }
        else
        {
            //animator.SetBool("isLeftTurning", false);
        }

        if (Input.GetKey("space"))
        {
            animator.SetBool("isDriving", false);
            animator.SetBool("isLeftTurning", false);
            animator.SetBool("isRightTurning", false);
        }


    }
}
