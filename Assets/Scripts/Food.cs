using UnityEngine;

namespace Sneka
{
    public class Food : MonoBehaviour, IEdible
    {
        public static event System.Action OnFoodEaten;

        private Transform _transform;

        #region MonoBehaviourCalls

        private void Awake()
        {
            _transform = transform;
            OnFoodEaten = null;
        }

        private void Start()
        {
            _transform.position = GameManager.Instance.GetRandomPosition();
        }

        #endregion

        public int Consume()
        {
            GameManager.Instance.AddScore(1);
            _transform.position = GameManager.Instance.GetRandomPosition();
            OnFoodEaten?.Invoke();
            return 1;
        }
    }
}
