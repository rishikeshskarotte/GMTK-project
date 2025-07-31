using System;
using System.Collections.Generic;
using Player.States;
using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName ="Player", menuName = "ScriptableObjects/player")]
    public class PlayerSO : ScriptableObject
    {
        [SerializeField] private List<PlayerStateData> playerStateData = new();
        [SerializeField] private float revivalTime;
            
        private Dictionary<PlayerState, PlayerStateData> stateDataDict;
        
        public Dictionary<PlayerState, PlayerStateData> StateDataDict => stateDataDict;
        public float RevivalTime => revivalTime;
        
        private void OnEnable()
        {
            stateDataDict = new Dictionary<PlayerState, PlayerStateData>();

            foreach (var data in playerStateData)
            {
                if (!stateDataDict.TryAdd(data.PlayerState, data))
                {
                    Debug.LogWarning($"Duplicate PlayerState entry found: {data.PlayerState}");
                }
            }
        }
    }
    
    [Serializable]
    public class PlayerStateData
    {
        public PlayerState PlayerState;
        public SpriteRenderer PlayerSprite;
        public float Speed;
        public float JumpForce;
    }
}