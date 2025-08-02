using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelView levelView;
        [SerializeField] private LevelSO levelSO;
        private LevelController levelController;
        
        public LevelManager()
        {
            levelController = new LevelController(levelView, levelSO);
        }
    }
}