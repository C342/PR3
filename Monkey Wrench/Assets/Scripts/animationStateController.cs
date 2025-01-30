using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);

        if (Input.GetKey("w"))
        {
            animator.SetBool(isWalkingHash, true);
        }

        else if (Input.GetKey("a"))
        {
            animator.SetBool(isWalkingHash, true);
        }

        else if (Input.GetKey("s"))
        {
            animator.SetBool(isWalkingHash, true);
        }

        else if (Input.GetKey("d"))
        {
            animator.SetBool(isWalkingHash, true);
        }

        else
        {
            animator.SetBool(isWalkingHash, false);
        }


        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isThrowing", true);
        }

        if (!Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isThrowing", false);
        }
    }
}
