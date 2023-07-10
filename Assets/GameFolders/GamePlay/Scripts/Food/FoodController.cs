using System;
using UnityEngine;

namespace Sumo.GamePlay
{
    public class FoodController : MonoBehaviour
    {
        [SerializeField] private int foodPower;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out HitController hitController))
            {
                hitController.Force += foodPower;
                gameObject.SetActive(false);
            }
        }
    }
}
