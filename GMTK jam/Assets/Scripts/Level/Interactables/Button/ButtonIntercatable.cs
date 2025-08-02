using System;
using UnityEngine;

namespace Level.Interactables.Button
{
    public class ButtonInteractable : MonoBehaviour, IInteractable
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnInteracted();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnStoppedInteracted();
        }

        public void OnInteracted()
        {
           
        }

        public void OnStoppedInteracted()
        {
            
        }
    }
}