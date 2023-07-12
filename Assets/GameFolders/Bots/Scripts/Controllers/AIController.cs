using System.Collections.Generic;
using Sumo.GamePlay;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.AI
{
    public class AIController : MonoBehaviour, ITrigger
    {
        private HitController _hitController;
        private Rigidbody _rigidbody;

        private readonly List<ITriggerAction> _iTriggerActions = new List<ITriggerAction>();

        private void Awake()
        {
            _hitController = GetComponent<HitController>();
            _rigidbody = GetComponent<Rigidbody>();
            _iTriggerActions.AddRange(GetComponents<ITriggerAction>());
            _iTriggerActions.AddRange(GetComponentsInChildren<ITriggerAction>());
        }

        private void FixedUpdate()
        {
            if (_hitController.OnHit) return;

            _rigidbody.velocity = new Vector3(0f, 0f, 0f);
        }

        public void OnTrigger()
        {
            foreach (ITriggerAction iTriggerAction in _iTriggerActions)
            {
                iTriggerAction.Action();
            }
        }
    }
}
