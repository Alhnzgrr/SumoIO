using System;
using UnityEngine;

namespace Sumo.Movements
{
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float turnSpeed;
        
        protected Rigidbody rigidbody;
        protected Vector3 direction;
        protected bool canMove;
        
        protected virtual void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        protected void Start()
        {
            canMove = true;
        }

        protected virtual void FixedUpdate()
        {
            if (!canMove) return;

            Move();
        }
        
        protected virtual void Update()
        {
            if (!canMove) return;

            direction = CalculateDirection();
            Rotate(direction);
        }

        protected void Move()
        {
            rigidbody.velocity = transform.forward * moveSpeed;
        }

        protected virtual void Rotate(Vector3 direction)
        {
            if (Vector3.Distance(direction, Vector3.zero) < 0.1f) return;

            transform.forward = Vector3.Lerp(transform.forward, direction, turnSpeed * Time.deltaTime);
        }

        protected abstract Vector3 CalculateDirection();
    }
}
