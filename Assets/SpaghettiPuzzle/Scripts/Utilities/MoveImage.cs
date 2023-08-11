using DG.Tweening;
using UnityEngine;

namespace SpaghettiPuzzle
{
    public class MoveImage : MonoBehaviour
    {
        private RectTransform _imageRectTransform;
        public float MoveDistance = 200f;
        public float MmoveDuration = 1f;


        private void Awake()
        {
            _imageRectTransform = GetComponent<RectTransform>();
        }
        private void Start()
        {
            // Set initial position
            Vector2 initialPosition = _imageRectTransform.anchoredPosition;

            // Move left and right continuously using Loop
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_imageRectTransform.DOAnchorPosX(initialPosition.x - MoveDistance, MmoveDuration));
            sequence.Append(_imageRectTransform.DOAnchorPosX(initialPosition.x, MmoveDuration));
            sequence.SetLoops(-1, LoopType.Restart); // Infinite loop

            // Start the sequence
            sequence.Play();
        }
    }
}
