using System;
using System.Collections;
using Sumo.Data;
using Sumo.Interface;
using Sumo.Movements;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class HitController : MonoBehaviour, ITriggerAction
    {
        [SerializeField] private HitData data;
        [SerializeField] private int tiltAngle;
        [SerializeField] private Transform visualObjectTransform;
        
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Movement _movement;
        private Vector3 _direction;
        private Vector3 _rotationAxis;

        private float _power;
        
        public float Power
        {
            get => _power;
            set => _power = value;
        }

        public bool OnHit;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = GetComponent<Transform>();
            _movement = GetComponent<Movement>();
        }

        private void Start()
        {
            _power = data.StartPower;
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
                    damage *= data.BackMultiplier;
                }
                else // Çarpışma yanlarda 
                {
                    if (Vector3.Dot(myDirection, transform.forward) <= -0.5) // Çarpışmaya arka yanımla girdim
                    {
                        damage *= data.SideBackMultiplier;
                    }
                    else if (Vector3.Dot(myDirection, transform.forward) > -0.5) // Çarpışmaya ön yanımla girdim
                    { 
                        // 1
                    }
                }

                // Burada hesaplamalar sonucunda hasar alıcam
                
                _rigidbody.AddForce(-myDirection * damage, ForceMode.Impulse);

                StartCoroutine(OnHitController());
            }
        }

        public float GetTrueForce(Vector3 direction)
        {
            float multiplierForce = _power;
            
            if (Vector3.Dot(direction, transform.forward) > 0.9f) // Çarpışmaya önümle girdim
            {
                multiplierForce *= data.FrontMultiplier;
            }

            return multiplierForce;
        }

        private IEnumerator OnHitController()
        {
            _movement.CanMove = false;
            
            // _rotationAxis = new Vector3(_direction.z, 0f, -_direction.x);
            // Quaternion rotation = Quaternion.AngleAxis(tiltAngle, _rotationAxis);
            // visualObjectTransform.rotation *= rotation;

            yield return new WaitForSeconds(.5f);

            //visualObjectTransform.rotation = Quaternion.Euler(Vector3.zero);

            _movement.CanMove = true;
        }

        public void Action() // Food yedik, gücü arttır
        {
            Power += data.PowerIncreaseCoefficient;
        }
    }
}
