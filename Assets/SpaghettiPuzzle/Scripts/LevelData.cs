using UnityEngine;


namespace SpaghettiPuzzle
{
    [CreateAssetMenu(fileName = "LevelData_", menuName = "SpaghettiPuzzle/LevelData", order = 51)]
    public class LevelData : ScriptableObject
    {
        [Header("Level")]
        public int Level;
        public Sprite Background;
        public GameObject Prefab;
        public GameObject MaskPrefab;
        
    }
}
