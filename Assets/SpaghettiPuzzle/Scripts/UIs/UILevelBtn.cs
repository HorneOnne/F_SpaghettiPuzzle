using UnityEngine;
using UnityEngine.UI;

namespace SpaghettiPuzzle
{
    public class UILevelBtn : MonoBehaviour
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private Button _playBtn;
        private void Start()
        {
            _playBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                GameManager.Instance.PlayingLevelData = _levelData;
                Loader.LoadTweening(Loader.Scene.GameplayScene);
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
        }
    }
}
