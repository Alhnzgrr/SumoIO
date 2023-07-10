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
        private Vector3 _direction;

        private void Awake()
        {
            _hitController = GetComponent<HitController>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_hitController.OnHit) return;

            _rigidbody.velocity = transform.forward * moveSpeed;
        }

        private void Update()
        {
            _horizontal = JoystickInput.Instance.GetHorizontal();
            _vertical = JoystickInput.Instance.GetVertical();

            if (Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) > 0.05f)
            {
                _direction = new Vector3(_horizontal, 0f, _vertical);
            }

            transform.forward = Vector3.Lerp(transform.forward, _direction, turnSpeed * Time.deltaTime);
        }
    }
}
