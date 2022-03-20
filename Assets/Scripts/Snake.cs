using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sneka
{
    public class Snake : MonoBehaviour
    {
        [SerializeField]
        [Min(1)]
        [Tooltip("The amount of fixedUpdates between each move")]
        private int _slowness = 1;
        [SerializeField]
        private GameObject _prefab;

        private int _nextMove;
        private Vector3 _direction = Vector3.right;

        private Controls _inputs;
        private List<Transform> _transforms = new List<Transform>();

        private void Awake()
        {
            _transforms.Add(transform);
            _nextMove = 1;
            _inputs = new Controls();
            _inputs.Player.MovementX.performed += (context) =>
            {
                if (_direction.x == 0)
                {
                    var value = context.ReadValue<float>();
                    _direction = new Vector3(value, 0, 0);
                }
            };
            _inputs.Player.MovementY.performed += (context) =>
            {
                if (_direction.y == 0)
                {
                    var value = context.ReadValue<float>();
                    _direction = new Vector3(0, value, 0);
                }
            };
            _inputs.Player.Enable();
        }

        private void FixedUpdate()
        {
            if(_nextMove == 1)
            {
                for(int i = _transforms.Count-1; i > 0; i--)
                {
                    _transforms[i].position = _transforms[i - 1].position;
                }
                _transforms[0].position += _direction;
                _nextMove = _slowness;
            }
            else
            {
                --_nextMove;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IEdible col;
            if (collision.TryGetComponent(out col))
            {
                Grow(col.Consume());
            }
        }

        private void Grow(int amount)
        {
            if(amount < 0)
            {
                int removed = 0;
                while(_transforms.Count > 2 && removed > amount)
                {
                    --removed;
                    var target = _transforms[_transforms.Count - 1];
                    _transforms.Remove(target);
                    Destroy(target.gameObject);
                }
            }
            else
            {
                for (int i = 0; i < amount; ++i)
                {
                    _transforms.Add(Instantiate(_prefab, _transforms[0].position, Quaternion.identity).transform);
                }
            }
        }
    }
}
