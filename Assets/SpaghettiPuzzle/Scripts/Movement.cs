using UnityEngine;


namespace SpaghettiPuzzle
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private LayerMask _plateLayer;

        [SerializeField] private float _moveSpeed = 2.0f;
        [SerializeField] private float _rotationSpeed = 180.0f;

        [Range(0, 360)]
        [SerializeField] private float _randomRotMin = 0;
        [Range(0, 360)]
        [SerializeField] private float _randomRotMax = 360;
        [Range(-360, 360)]
        [SerializeField] private float _rotClickMin = -90;
        [Range(-360, 360)]
        [SerializeField] private float _rotClickMax = 90;

        private Quaternion _targetRotation;
        private GameplayManager _gameplayManager;
        private SoundManager _soundManager;



        void Start()
        {
            _gameplayManager = GameplayManager.Instance;
            SetRandomRotation(_randomRotMin, _randomRotMax);

            _soundManager = SoundManager.Instance;
        }

        void Update()
        {
            if(_gameplayManager.CurrentState == GameplayManager.GameState.WAITING)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _gameplayManager.ChangeGameState(GameplayManager.GameState.PLAYING);
                }
            }
            else if(_gameplayManager.CurrentState == GameplayManager.GameState.PLAYING)
            {
                _soundManager.PlaySound(SoundType.Move, false);

                if (Input.GetMouseButtonDown(0))
                {
                    _soundManager.PlaySound(SoundType.Rotate, false);
                    SetRandomRotation(_rotClickMin, _rotClickMax);
                }

                // Move upwards along the local up direction
                transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime, Space.Self);

                // Rotate towards the target rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

                // Check if the object has reached the target rotation
                if (transform.rotation == _targetRotation)
                    SetRandomRotation(_randomRotMin, _randomRotMax);
            }      
        }


        void SetRandomRotation(float min, float max)
        {
            float randomAngle = Random.Range(min, max);
            _targetRotation = Quaternion.Euler(0, 0, randomAngle);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_plateLayer == (_plateLayer | (1 << collision.gameObject.layer)))
            {
                if (_gameplayManager.CurrentState == GameplayManager.GameState.PLAYING)
                {
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                    _soundManager.PlaySound(SoundType.Hit, false);
                }
                    
            }
        }
    }
}
