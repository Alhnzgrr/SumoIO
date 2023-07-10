using Sumo.GamePlay;
using UnityEngine;

namespace Sumo.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float turnSpeed;

        private HitController _hitController;
        private Rigidbody _rigidbody;
        private Vector3 _movement;

        private float _horizontal;
        private float _vertical;

        private void Awake()
        {
            _hitController = GetComponent<HitController>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_hitController.OnHit) return;

            _movement = new Vector3(_horizontal, 0f, _vertical);
            _movement *= moveSpeed;
            
            _rigidbody.velocity = new Vector3(_movement.x, _rigidbody.velocity.y, _movement.z);
        }

        private void Update()
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical = Input.GetAxis("Vertical");
        }
    }
}
