using UnityEngine;

namespace Player.Ghost
{
    [CreateAssetMenu(fileName ="Ghost", menuName = "ScriptableObjects/Ghost")]
    public class GhostSO :ScriptableObject
    {
        [SerializeField] private float speed;

        public float Speed => speed;
    }
}