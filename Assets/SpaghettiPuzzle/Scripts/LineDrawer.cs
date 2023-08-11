using System.Collections.Generic;
using UnityEngine;


namespace SpaghettiPuzzle
{
    public class LineDrawer : MonoBehaviour
    {
        private LineRenderer _lr;
        private bool _isMousePressed;
        [SerializeField] private HashSet<Vector2> _pointsSet;
        private Vector2 _mousePosition;


        private Camera _mainCam;
        private Color _startColor;

        private struct LineSegment
        {
            public Vector2 StartPoint;
            public Vector2 EndPoint;
        }

        private void Awake()
        {
            _lr = GetComponent<LineRenderer>();
            _isMousePressed = false;
            _pointsSet = new HashSet<Vector2>();
            _mainCam = Camera.main;

            _startColor = _lr.startColor;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isMousePressed = true;
                _lr.positionCount = 0;
                _pointsSet.Clear();

                _lr.startColor = _startColor;
                _lr.endColor = _startColor;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isMousePressed = false;
            }

            // Drawing line
            if (_isMousePressed)
            {
                _mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
                if (_pointsSet.Contains(_mousePosition) == false)
                {
                    _pointsSet.Add(_mousePosition);
                    _lr.positionCount = _pointsSet.Count;
                    _lr.SetPosition(_pointsSet.Count - 1, _mousePosition);

                    if (IsLineSegmentCollided())
                    {
                        _isMousePressed = false;
                        _lr.startColor = Color.red;
                        _lr.endColor = Color.red;
                    }
                }
            }
        }

       

        private bool IsLineSegmentCollided()
        {
            if (_pointsSet.Count < 2)
                return false;

            Vector2[] pointsArray = new Vector2[_pointsSet.Count];
            _pointsSet.CopyTo(pointsArray);

            for (int i = 0; i < pointsArray.Length - 3; i++) // Corrected index range
            {
                if (isLinesIntersect(pointsArray[i], pointsArray[i + 1], pointsArray[pointsArray.Length - 2], pointsArray[pointsArray.Length - 1]))
                    return true;
            }
            return false;
        }

        private bool isLinesIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            // Use Unity's Vector2.Dot and Vector2.Cross for line intersection checks
            float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);
            if (denominator == 0)
                return false;

            float ua = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            float ub = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            return (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1);
        }
    }
}
