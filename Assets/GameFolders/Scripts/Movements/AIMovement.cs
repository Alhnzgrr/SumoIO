using System;
using System.Linq;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.Movements
{
    public class AIMovement : Movement, ITriggerAction
    {
        [SerializeField] private float outDistance;
        [SerializeField] private float maxTargetDistance;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float searchTime;
        
        private Transform _target;

        private float _lastSearchTime;
        
        private bool IsTargetNull => _target == null;
        private bool CanSearch => Time.time > _lastSearchTime + searchTime;
        
        protected override void Update()
        {
            if (!CanSearch) return;

            _lastSearchTime = Time.time;
            
            if (IsTargetNull)
            {
                AssignNewTarget();
                return;
            }

            if (Vector3.Distance(_target.position, transform.position) > outDistance)
            {
                AssignNewTarget();
            }
            
            base.Update();
        }

        private void AssignNewTarget()
        {
            Collider[] attachedColliders = Physics.OverlapSphere(transform.position, maxTargetDistance * 2, layerMask);

            if (attachedColliders is not { Length: > 1 }) return;
            
            Collider[] orderedColliders = attachedColliders.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToArray();
            _target = orderedColliders[1]?.transform;
        }
        
        protected override Vector3 CalculateDirection()
        {
            if (IsTargetNull) return Vector3.zero;

            Vector3 calculatingDirection = _target.position - transform.position;
            calculatingDirection.y = 0;
            calculatingDirection.Normalize();
            return calculatingDirection;
        }

        public void Action()
        {
            AssignNewTarget();
        }
    }
}
