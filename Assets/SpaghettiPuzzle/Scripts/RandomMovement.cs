using UnityEngine;


namespace SpaghettiPuzzle
{
    public class RandomMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask _plateLayer;

        public float moveSpeed = 2.0f;
        public float rotationSpeed = 180.0f;

        [Range(0, 360)]
        [SerializeField] private float _randomRotMin = 0;
        [Range(0, 360)]
        [SerializeField] private float _randomRotMax = 360;
        [Range(-360, 360)]
        [SerializeField] private float _rotClickMin = -90;
        [Range(-360, 360)]
        [SerializeField] private float _rotClickMax = 90;

        private Quaternion targetRotation;

        void Start()
        {
            SetRandomRotation(_randomRotMin, _randomRotMax);
        }

        void Update()
        {
            // Move upwards along the local up direction
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.Self);

            // Rotate towards the target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Check if the object has reached the target rotation
            if (transform.rotation == targetRotation)
                SetRandomRotation(_randomRotMin, _randomRotMax);

            if(Input.GetMouseButtonDown(0))
            {
                SetRandomRotation(_rotClickMin, _rotClickMax);
            }
        }

        void SetRandomRotation(float min, float max)
        {
            float randomAngle = Random.Range(min, max);
            targetRotation = Quaternion.Euler(0, 0, randomAngle);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_plateLayer == (_plateLayer | (1 << collision.gameObject.layer)))
            {
                Debug.Log("Game over");
            }
        }
    }
}
