using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace Alex.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private InputAction dashAction;
        private InputAction aimAction;
        private Vector2 aimDirection;
        private Rigidbody2D rb;
        public float dashForce = 10f;
        public float dashCooldown = 2f;
        private float lastDashTime = Mathf.NegativeInfinity;
        public Transform aimingIndicator;

        private void OnEnable()
        {
            dashAction = new InputAction("GamePlay/Dash");
            aimAction = new InputAction("GamePlay/Aim");
            dashAction.started += ctx => OnDash(ctx);
            dashAction.Enable();
            aimAction.Enable();
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

        public void OnAim(InputAction.CallbackContext ctx)
        {
            aimDirection = ctx.ReadValue<Vector2>();
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            aimingIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (Time.time - lastDashTime > dashCooldown)
            {
                if (aimDirection != Vector2.zero)
                {
                    Debug.Log("Dashing");
                    rb.velocity = Vector2.zero;
                    rb.AddForce(aimDirection * dashForce, ForceMode2D.Impulse);
                    lastDashTime = Time.time; 
                }
            }
            else Debug.Log("Dash on Cooldown");
        }
    }
}