using UnityEngine;

namespace SpaghettiPuzzle
{
    public class LevelLoader : MonoBehaviour
    {
        public static LevelLoader Instance { get; private set; }

        private Vector2 _centerPoint = Vector2.zero;

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            Instantiate(GameManager.Instance.PlayingLevelData.Prefab, _centerPoint, Quaternion.identity);
        }
    }
}
