using System;
using Sumo.Interface;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class FoodController : MonoBehaviour
    {
        [SerializeField] private GameObject visualObject;

        private Action<FoodController> _onComplete;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ITrigger iTrigger))
            {
                iTrigger.OnTrigger();
                visualObject.SetActive(false);
                _onComplete?.Invoke(this);
            }
        }

        public void Initialize(Vector3 position, Action<FoodController> onComplete)
        {
            transform.localPosition = position;
            visualObject.SetActive(true);
            _onComplete = onComplete;
        }
        
    }
}
