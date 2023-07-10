using System.Collections;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class HitController : MonoBehaviour
    {
        [SerializeField] private int force;
        [SerializeField] private int tiltAngle;
        [SerializeField] private Transform visualObjectTransform;
        
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
            HitController otherHitController = collision.gameObject.GetComponent<HitController>();

            if (otherHitController != null)
            {
                Vector3 myDirection = collision.transform.position - transform.position;
                Vector3 otherDirection = transform.position - collision.transform.position;
                myDirection.Normalize();
                otherDirection.Normalize();

                float damage = otherHitController.GetTrueForce(otherDirection);

                if (Vector3.Dot(myDirection, transform.forward) > 0.9f) // Çarpışmaya önümle girdim
                {
                    // 
                }
                else if (Vector3.Dot(myDirection, transform.forward) < -0.9f) // Çarpışmaya arkamla giridm
                {
                    damage *= 4;
                }
                else // Çarpışma yanlarda 
                {
                    if (Vector3.Dot(myDirection, transform.forward) <= -0.5) // Çarpışmaya arka yanımla girdim
                    {
                        damage *= 2;
                    }
                    else if (Vector3.Dot(myDirection, transform.forward) > -0.5) // Çarpışmaya ön yanımla girdim
                    { 
                        // 1
                    }
                }

                // Burada hesaplamalar sonucunda hasar alıcam
                
                _rigidbody.AddForce(-myDirection * damage, ForceMode.Impulse);
            }
        }

        public float GetTrueForce(Vector3 direction)
        {
            float multiplierForce = force;
            
            if (Vector3.Dot(direction, transform.forward) > 0.9f) // Çarpışmaya önümle girdim
            {
                multiplierForce *= 2;
            }

            return multiplierForce;
        }

        private IEnumerator OnHitController()
        {
            OnHit = true;
            
            _rotationAxis = new Vector3(_direction.z, 0f, -_direction.x);
            Quaternion rotation = Quaternion.AngleAxis(tiltAngle, _rotationAxis);
            visualObjectTransform.rotation *= rotation;

            yield return new WaitForSeconds(.5f);

            visualObjectTransform.rotation = Quaternion.Euler(Vector3.zero);

            OnHit = false;
        }
    }
}
