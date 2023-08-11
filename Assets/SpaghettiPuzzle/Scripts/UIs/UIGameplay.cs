using UnityEngine;
using TMPro;
using System.Collections;

namespace SpaghettiPuzzle
{
    public class UIGameplay : CustomCanvas
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _scoreText;

        // Cached
        private TimerManager _timerManager;
        private WaitForSeconds _updateTimeUI = new WaitForSeconds(0.05f);

        private void OnEnable()
        {
            GameManager.OnScoreUp += UpdateScoreUI;
        }

        private void OnDisable()
        {
            GameManager.OnScoreUp -= UpdateScoreUI;
        }


        private void Start()
        {
            _timerManager = TimerManager.Instance;
            StartCoroutine(PerformUpdateTimeUI());           
        }


        private void UpdateScoreUI()
        {
            _scoreText.text = $"M {_timerManager.TimeToText()}";
        }

        private IEnumerator PerformUpdateTimeUI()
        {
            while(true)
            {
                yield return _updateTimeUI;
                UpdateScoreUI();
            }
            
        }
    }
}
