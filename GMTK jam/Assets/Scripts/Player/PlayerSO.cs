using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName ="Player", menuName = "ScriptableObjects/player")]
    public class PlayerSO : ScriptableObject    
    {
       [SerializeField]private float playerSpeed;
       
       public float PlayerSpeed => playerSpeed;
    }
}