using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Animations : MonoBehaviour
{
    private bool running = false;

    [SerializeField]
    private bool talkingOnQ = false;

    private Animator animator;

    private CharacterController controller;

    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(controller.velocity.magnitude != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        if (Input.GetKeyDown(KeyCode.T) && talkingOnQ){
            animator.SetBool("Talking", !animator.GetBool("Talking"));
        }
    }
}
