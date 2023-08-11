using System.Collections.Generic;
using UnityEngine;


namespace SpaghettiPuzzle
{
    public class LineDrawer : MonoBehaviour
    {
        private LineRenderer _lr;
        [SerializeField] private HashSet<Vector2> _pointsSet;
        private Vector2 _mousePosition;
        [SerializeField] private Transform _mover;


        private Color _startColor;
        private GameplayManager _gameplayManager;

        private struct LineSegment
        {
            public Vector2 StartPoint;
            public Vector2 EndPoint;
        }

        private void Awake()
        {
            _lr = GetComponent<LineRenderer>();
            _pointsSet = new HashSet<Vector2>();
            _startColor = _lr.startColor;
        }
        private void Start()
        {
            _gameplayManager = GameplayManager.Instance;
        }

        private void Update()
        {
            if (_gameplayManager.CurrentState != GameplayManager.GameState.PLAYING) return;
            
            if (_pointsSet.Contains(_mover.position) == false)
            {
                _pointsSet.Add(_mover.position);
                _lr.positionCount = _pointsSet.Count;
                _lr.SetPosition(_pointsSet.Count - 1, _mover.position);

                if (IsLineSegmentCollided())
                {
                    SoundManager.Instance.PlaySound(SoundType.Hit, false);
                    GameplayManager.Instance.ChangeGameState(GameplayManager.GameState.GAMEOVER);
                }
            }
        }

        #region Utilities
        private bool IsLineSegmentCollided()
        {
            if (_pointsSet.Count < 15)
                return false;

            Vector2[] pointsArray = new Vector2[_pointsSet.Count];
            _pointsSet.CopyTo(pointsArray);

            for (int i = 0; i < pointsArray.Length - 15; i++) // Corrected index range
            {
                //if (IsLinesIntersect(pointsArray[i], pointsArray[i + 1], pointsArray[pointsArray.Length - 2], pointsArray[pointsArray.Length - 1]))
                //    return true;

                Vector2[] rectPoints = CalculateRectangleCorners(pointsArray[pointsArray.Length - 1], 0.7f, 0.7f);
                if (IsPointInsideRectangle(pointsArray[i], rectPoints[3], rectPoints[2], rectPoints[1], rectPoints[0], 0f))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsLinesIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            // Use Unity's Vector2.Dot and Vector2.Cross for line intersection checks
            float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);
            if (denominator == 0)
                return false;

            float ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            float ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            return (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1);
        }


        public static bool IsPointInsideRectangle(Vector2 point, Vector2 pA, Vector2 pB, Vector2 pC, Vector2 pD, float offset)
        {
            // Find the minimum and maximum x and y coordinates of the rectangle with offset
            float minX = Mathf.Min(pA.x, pB.x, pC.x, pD.x) - offset;
            float maxX = Mathf.Max(pA.x, pB.x, pC.x, pD.x) + offset;
            float minY = Mathf.Min(pA.y, pB.y, pC.y, pD.y) - offset;
            float maxY = Mathf.Max(pA.y, pB.y, pC.y, pD.y) + offset;

            // Check if the point is inside the rectangle with offset
            return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY;
        }

        private Vector2[] CalculateRectangleCorners(Vector2 center, float rectWidth, float rectHeight)
        {
            Vector2[] cornerPoints = new Vector2[4];
            float halfWidth = rectWidth / 2f;
            float halfHeight = rectHeight / 2f;

            cornerPoints[0] = center + new Vector2(-halfWidth, -halfHeight); // Bottom-left
            cornerPoints[1] = center + new Vector2(halfWidth, -halfHeight);  // Bottom-right
            cornerPoints[2] = center + new Vector2(halfWidth, halfHeight);   // Top-right
            cornerPoints[3] = center + new Vector2(-halfWidth, halfHeight);  // Top-left

            return cornerPoints;
        }
        #endregion
    }
}
