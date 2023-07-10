using Sumo.GamePlay;
using UnityEngine;

namespace Sumo.Bot
{
    public class BotController : MonoBehaviour
    {
        private HitController _hitController;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _hitController = GetComponent<HitController>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_hitController.OnHit) return;

            _rigidbody.velocity = new Vector3(0f, 0f, 0f);
        }

    }
}
