using UnityEngine;
using UnityEngine.UI;


namespace SpaghettiPuzzle
{
    public class UIBackground : CustomCanvas
    {
        [Header("Texts")]
        [SerializeField] private Image _background;


        private void Start()
        {
            SetBackground();
        }

        private void SetBackground()
        {
            _background.sprite = GameManager.Instance.PlayingLevelData.Background;
        }
    }
}
