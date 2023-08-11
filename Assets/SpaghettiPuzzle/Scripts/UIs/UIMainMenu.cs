using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaghettiPuzzle
{
    public class UIMainMenu : CustomCanvas
    {
        [Header("Buttons")]
        [SerializeField] private Button _playBtn;
        [SerializeField] private Button _soundBtn;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI _recordText;

        [Header("Sprite")]
        [SerializeField] private Sprite _unmuteSound;
        [SerializeField] private Sprite _muteSound;


        private void Start()
        {
            LoadRecord();
            UpdateSoundUI();

            _playBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                UIManager.Instance.CloseAll();
                UIManager.Instance.DisplayLevelMenu(true);             
            });

            _soundBtn.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlaySound(SoundType.Button, false);

                ToggleSound();              
            });
        }

        private void OnDestroy()
        {
            _playBtn.onClick.RemoveAllListeners();
            _soundBtn.onClick.RemoveAllListeners();
        }


        private void LoadRecord()
        {
            _recordText.text = $"RECORD {TimeToText(GameManager.Instance.Record)} M";
        }

        private string TimeToText(float value)
        {
            int minutes = Mathf.FloorToInt(value);
            int seconds = Mathf.RoundToInt((value - minutes) * 60);

            return $"{minutes:D1}.{seconds:D1}";
        }

        private void ToggleSound()
        {
            SoundManager.Instance.MuteSoundFX(SoundManager.Instance.isSoundFXActive);
            SoundManager.Instance.isSoundFXActive = !SoundManager.Instance.isSoundFXActive;
            SoundManager.Instance.MuteBackground(SoundManager.Instance.isMusicActive);
            SoundManager.Instance.isMusicActive = !SoundManager.Instance.isMusicActive;

            UpdateSoundUI();
        }

        private void UpdateSoundUI()
        {
            if (SoundManager.Instance.isSoundFXActive)
            {
                _soundBtn.image.sprite = _unmuteSound;
            }
            else
            {
                _soundBtn.image.sprite = _muteSound;
            }
        }
    }
}
