using System;
using UnityEngine;

namespace Sumo.Movements
{
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float turnSpeed;
        [SerializeField] protected float gravityScale;
        
        protected Rigidbody rigidbody;
        protected Vector3 direction;

        protected Vector3 velocity;
        
        public bool CanMove { get; set; }
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        protected void Start()
        {
            CanMove = true;
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }
        
        protected virtual void Update()
        {
            if (!CanMove) return;

            direction = CalculateDirection();
            Rotate(direction);
        }

        protected void Move()
        {
            if (!CanMove) return;

             velocity = transform.forward * moveSpeed;
             velocity -= Vector3.down * gravityScale;
             rigidbody.velocity = velocity;
        }

        protected virtual void Rotate(Vector3 direction)
        {
            if (Vector3.Distance(direction, Vector3.zero) < 0.1f) return;

            transform.forward = Vector3.Lerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }

        protected abstract Vector3 CalculateDirection();
    }
}
