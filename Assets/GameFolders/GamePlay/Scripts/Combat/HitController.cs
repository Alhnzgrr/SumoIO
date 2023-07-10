using System.Collections;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class HitController : MonoBehaviour
    {
        [SerializeField] private int force;
        [SerializeField] private int tiltAngle;
        
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Vector3 _direction;
        private Vector3 _rotationAxis;

        public int Force
        {
            get => force;
            set => force = value;
        }

        public bool OnHit;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out HitController hitController))
            {
                _direction = _transform.position - hitController.transform.position;
                _direction.Normalize();

                _rigidbody.velocity = Vector3.zero;

                float dotProduct = Vector3.Dot(_direction, _transform.forward);
                float multiplier = (dotProduct < -0.5f) ? 1f : 2f;
                
                _rigidbody.AddForce(_direction * hitController.Force * multiplier, ForceMode.Impulse);

                StartCoroutine(OnHitController());
            }
        }

        private IEnumerator OnHitController()
        {
            OnHit = true;
            
            _rotationAxis = new Vector3(_direction.z, 0f, -_direction.x);
            Quaternion rotation = Quaternion.AngleAxis(tiltAngle, _rotationAxis);
            _transform.rotation *= rotation;

            yield return new WaitForSeconds(.5f);

            _transform.rotation = Quaternion.Euler(Vector3.zero);

            OnHit = false;
        }
    }
}
