using UnityEngine;

namespace SpaghettiPuzzle
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance { get; private set; }
        public static event System.Action OnStateChanged;
        public static event System.Action OnPlaying;
        public static event System.Action OnWin;
        public static event System.Action OnGameOver;

        public enum GameState
        {
            WAITING,
            PLAYING,
            WIN,
            GAMEOVER,
            PAUSE,
            EXIT,
        }


        [Header("Properties")]
        [SerializeField] private GameState _currentState;
        [SerializeField] private float waitTimeBeforePlaying = 0.5f;



        #region Properties
        public GameState CurrentState { get => _currentState;  }
        #endregion


        #region Init & Events
        private void Awake()
        {
            Instance = this;

        }

        private void OnEnable()
        {
            OnStateChanged += SwitchState;
        }

        private void OnDisable()
        {
            OnStateChanged -= SwitchState;
        }

        private void Start()
        {
            ChangeGameState(GameState.WAITING);
        }
        #endregion



        public void ChangeGameState(GameState state)
        {
            _currentState = state;
            OnStateChanged?.Invoke();
        }


        private void SwitchState()
        {
            switch (_currentState)
            {
                default: break;
                case GameState.WAITING:
                    
                    break;
                case GameState.PLAYING:
                    UIGameplayManager.Instance.DisplayGuidesMenu(false);
                    OnPlaying?.Invoke();             
                    break;
                case GameState.WIN:

                    OnWin?.Invoke();
                    break;
                case GameState.GAMEOVER:
                    StartCoroutine(Utilities.WaitAfter(0.5f, () =>
                    {
                        SoundManager.Instance.PlaySound(SoundType.GameOver, false);
                        GameManager.Instance.SetRecord(TimerManager.Instance.Time);

                        UIGameplayManager.Instance.CloseAll();
                        UIGameplayManager.Instance.DisplayGameoverMenu(true);
                        OnGameOver?.Invoke();
                    }));                                       
                    break;
                case GameState.PAUSE:
                    Time.timeScale = 0.0f;
                    break;
                case GameState.EXIT:
                    Time.timeScale = 1.0f;
                    break;
            }
        }
    }
}
