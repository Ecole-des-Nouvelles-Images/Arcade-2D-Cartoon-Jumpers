using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction dashAction;
    private InputAction aimAction;
    private Vector2 aimDirection;
    private Rigidbody2D rb;
    public float dashForce = 10f;

    private void OnEnable()
    {
        dashAction = new InputAction("Dash");
        aimAction = new InputAction("Aim");
        dashAction.performed += ctx => Dash();
       // aimAction.performed += ctx => SetAimDirection(ctx.ReadValue<Vector2>());
        dashAction.Enable();
      //  aimAction.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        dashAction.Disable();
        aimAction.Disable();
    }

    private void Update()
    {
        aimDirection = aimAction.ReadValue<Vector2>();
    }
    

    private void Dash()
    {
        Debug.Log("Dashing");
        rb.velocity = Vector2.zero;
        rb.AddForce(aimDirection * dashForce, ForceMode2D.Impulse);
    }
}
