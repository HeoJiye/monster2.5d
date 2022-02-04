using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts
{
    public class BasicCharacter : MonoBehaviour
    {
        #region Constants

        private static readonly int WALK_PROPERTY = Animator.StringToHash("Walk");

        #endregion


        #region Inspector

        [SerializeField]
        private float speed = 2f;

        [Header("Relations")]
        [SerializeField]
        private Animator animator = null;

        [SerializeField]
        private Rigidbody physicsBody = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;

        #endregion


        #region Fields

        private Vector3 _movement;

        #endregion
        public bool isAttacked = false;
        public int direction = 0;

        void Start()
        {
            Invoke("Think", 3);
        }

        void Think()
        {
            if (!isAttacked)
                direction = Random.Range(-2, 3);

            float nextThinkTime = Random.Range(2f, 5f);
            Invoke("Think", nextThinkTime);
        }

        #region MonoBehaviour

        private void Update()
        {
            // Vertical
            float inputY = 0;
            if (direction == -1)
                inputY = 1;
            else if (direction == 1)
                inputY = -1;

            // Horizontal
            float inputX = 0;
            if (direction == -2)
            {
                inputX = 1;
                spriteRenderer.flipX = true;
            }
            else if (direction == 2)
            {
                inputX = -1;
                spriteRenderer.flipX = false;
            }

            // Normalize
            _movement = new Vector3(inputX, 0, inputY).normalized;

            animator.SetBool(WALK_PROPERTY,
                             Math.Abs(_movement.sqrMagnitude) > Mathf.Epsilon);
        }

        private void FixedUpdate()
        {
            physicsBody.velocity = _movement * speed;
        }

        #endregion
    }
}