using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    class Input : MonoBehaviour
    {
        private InputMaster controls;
        [SerializeField] private Gun gun = null;
        private bool isMoving;

        void Awake()
        {
            controls = new InputMaster();
            controls.Player.Shoot.performed += ctx => Shoot();
            controls.Player.Movement.performed += ctx => isMoving = true;
            controls.Player.Movement.canceled += ctx => isMoving = false;
        }

        private void Update()
        {
            if (isMoving)
            {
                Vector2 v2 = controls.Player.Movement.ReadValue<Vector2>();
                Move(v2);
            }
        }

        private void Shoot()
        {
            gun.Shoot();
        }

        private void Move(Vector2 direction)
        {
            Vector3 v3 = new Vector3(direction.x, direction.y, 0) * Time.deltaTime;
            this.transform.position += v3;
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}
