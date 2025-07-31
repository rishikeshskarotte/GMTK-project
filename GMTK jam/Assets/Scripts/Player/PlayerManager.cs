using System;
using UnityEngine;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerPool playerPool;
        
        private void Awake()
        {
            playerPool = new PlayerPool();
            
        }
    }
}