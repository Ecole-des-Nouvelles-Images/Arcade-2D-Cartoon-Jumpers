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
        private InputAction shootAction;
        private InputAction aimDashAction;
        private InputAction aimShootAction;
        private Vector2 aimDashDirection;
        private Vector2 aimShootDirection;
        private Rigidbody2D rb;
        public float dashForce = 10f;
        public float dashCooldown = 2f;
        private float lastDashTime = Mathf.NegativeInfinity;
        public Transform aimingDashIndicator;
        public Transform aimingShootIndicator;

        private void OnEnable()
        {
            dashAction = new InputAction("GamePlay/Dash");
            aimDashAction = new InputAction("GamePlay/AimDash");
            aimShootAction = new InputAction("GamePlay/AimShoot");
            shootAction = new InputAction("GamePlay/Shoot");
            dashAction.started += ctx => OnDash(ctx);
            dashAction.Enable();
            aimDashAction.Enable();
            aimShootAction.Enable();
            shootAction.Enable();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            dashAction.Disable();
            aimDashAction.Disable();
            aimShootAction.Disable();
            shootAction.Disable();
        }

        public void OnAimDash(InputAction.CallbackContext ctx)
        {
            aimDashDirection = ctx.ReadValue<Vector2>();
            float aimAngle = Mathf.Atan2(aimDashDirection.y, aimDashDirection.x) * Mathf.Rad2Deg;
            aimingDashIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, aimAngle));
        }

        public void OnAimShoot(InputAction.CallbackContext ctx)
        {
            aimShootDirection = ctx.ReadValue<Vector2>();
            float shootAngle = Mathf.Atan2(aimShootDirection.y, aimShootDirection.x) * Mathf.Rad2Deg;
            aimingShootIndicator.rotation = Quaternion.Euler(new Vector3(0, 0, shootAngle));
        }

        public void OnDash(InputAction.CallbackContext ctx)
        {
            if (Time.time - lastDashTime > dashCooldown)
            {
                if (aimDashDirection != Vector2.zero)
                {
                    Debug.Log("Dashing");
                    rb.velocity = Vector2.zero;
                    rb.AddForce(aimDashDirection * dashForce, ForceMode2D.Impulse);
                    lastDashTime = Time.time; 
                }
            }
            else Debug.Log("Dash on Cooldown");
        }

        public void OnShoot(InputAction.CallbackContext ctx)
        {
            Debug.Log("Shot");
        }
        
    }
}