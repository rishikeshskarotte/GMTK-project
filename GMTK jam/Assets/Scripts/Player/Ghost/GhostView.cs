using System;
using Player.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Ghost
{
    public class GhostView : MonoBehaviour
    {
        private GhostController ghostController;
        public Rigidbody2D GhostRB;
        

        private void Update()
        {
            ghostController.GetInput();
        }

        private void FixedUpdate()
        {
            ghostController.Move();
        }

        public void SetController(GhostController ghostController)
        {
            this.ghostController = ghostController;
        }

        public void SetGhost()
        {
            throw new NotImplementedException();
        }
    }
}