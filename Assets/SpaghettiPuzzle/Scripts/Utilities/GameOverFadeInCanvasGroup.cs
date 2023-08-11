using UnityEngine;
using DG.Tweening;

namespace SpaghettiPuzzle
{
    public class GameOverFadeInCanvasGroup : MonoBehaviour
    {
        public CanvasGroup canvasGroup;
        public float fadeInDuration = 1f;


        private void OnEnable()
        {
            GameplayManager.OnGameOver += FadeIn;
        }

        private void OnDisable()
        {
            GameplayManager.OnGameOver -= FadeIn;
        }

        public void FadeIn()
        {
            // Set the CanvasGroup's alpha to 0 initially
            canvasGroup.alpha = 0f;

            // Fade in the CanvasGroup's alpha smoothly
            canvasGroup.DOFade(1f, fadeInDuration);
        }
    }
}
