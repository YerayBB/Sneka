using UnityEngine;

namespace Sneka
{
    public class Fruit : MonoBehaviour, IEdible
    {
        public float duration = 5;
        [SerializeField]
        [Min(2)]
        private int _points = 10;
        [SerializeField]
        private int _nutrients = 1;

        #region MonoBehaviourCalls

        private void Awake()
        {
            Destroy(gameObject, duration);
        }

        #endregion

        public int Consume()
        {
            GameManager.Instance.AddScore(_points);
            transform.position = GameManager.Instance.GetRandomPosition();
            return _nutrients;
        }
    }
}
