using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaghettiPuzzle
{
    public class UIGameover : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _homeBtn;
        [SerializeField] private Button _replayBtn;
        

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _recordText;

        [Space(10)]
        [SerializeField] private RectTransform _maskRoot;
        [SerializeField] private GameOverFadeInCanvasGroup _fadeInCanvasGroup;

        // Cached
        private GameManager _gameManager;
 
        private void OnEnable()
        {
            GameplayManager.OnGameOver += LoadScore;
            GameplayManager.OnGameOver += LoadRecord;
            GameplayManager.OnGameOver += LoadMaskUI;           
        }

        private void OnDisable()
        {
            GameplayManager.OnGameOver -= LoadScore;
            GameplayManager.OnGameOver -= LoadRecord;
            GameplayManager.OnGameOver -= LoadMaskUI;
        }


        private void Start()
        {
            _gameManager = GameManager.Instance;
            LoadScore();
            LoadRecord();


            _replayBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.LoadTweening(Loader.Scene.GameplayScene);
            });

            _homeBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                Loader.LoadTweening(Loader.Scene.MenuScene);
            });
        }

        private void OnDestroy()
        {
            _replayBtn.onClick.RemoveAllListeners();
            _homeBtn.onClick.RemoveAllListeners();
        }

        private void LoadScore()
        {
            _scoreText.text = $"M {TimerManager.Instance.TimeToText()}";
        }

        private void LoadRecord()
        {
            _recordText.text = $"M {TimerManager.Instance.TimeToText(GameManager.Instance.Record)}";
        }

        private void LoadMaskUI()
        {
            Instantiate(GameManager.Instance.PlayingLevelData.MaskPrefab, _maskRoot.transform);
        }
    }
}
