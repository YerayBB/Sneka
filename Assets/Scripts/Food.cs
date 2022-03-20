using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneka
{
    public class Food : MonoBehaviour, IEdible
    {
        public static event System.Action OnFoodEaten;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            OnFoodEaten = null;
        }

        private void Start()
        {
            _transform.position = GameManager.Instance.GetRandomPosition();
        }

        public int Consume()
        {
            _transform.position = GameManager.Instance.GetRandomPosition();
            OnFoodEaten?.Invoke();
            return 1;
        }
    }
}
