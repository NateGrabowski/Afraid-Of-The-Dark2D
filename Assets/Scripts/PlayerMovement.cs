using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] public float movementSpeed = 2.5f;
    Animator myAnimator;
    Rigidbody2D myRigidbody;
    Vector2 moveInput;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Animations();
    }

    void Animations()
    {
        myAnimator.SetFloat("moveX", moveInput.x);
        myAnimator.SetFloat("moveY", moveInput.y);
        myAnimator.SetFloat("Speed", moveInput.sqrMagnitude); //speed is at 0 if not moving

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            myAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            myAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

    }

    void OnMove(InputValue val)
    {
        moveInput = val.Get<Vector2>();
        myRigidbody.velocity = moveInput * movementSpeed;
    }


}
