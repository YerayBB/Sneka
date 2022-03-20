using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneka
{
    public class Fruit : MonoBehaviour, IEdible
    {
        [SerializeField]
        private float _duration = 5;
        [SerializeField]
        [Min(2)]
        private int _points = 5;
        [SerializeField]
        private int _nutrients = 1;

        private void Awake()
        {
            Destroy(gameObject, _duration);
        }

        public int Consume()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
            return _nutrients;
        }
    }
}
