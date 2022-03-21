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
        private GameObject _bodyPrefab;

        private Animator _animator;
        private Controls _inputs;

        private List<Transform> _transforms = new List<Transform>();
        private Vector3 _direction = Vector3.right;
        private int _nextMove;


        #region MonoBehaviourCalls
        private void Awake()
        {
            _animator = GetComponent<Animator>();
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
                _animator.SetFloat("DirectionX", _direction.x);
                _animator.SetFloat("DirectionY", _direction.y);
                _nextMove = _slowness;
            }
            else
            {
                --_nextMove;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Obstacle")
            {
                //GameOver
                _animator.SetTrigger("Hit");
                _nextMove = 0;
                GameManager.Instance.GameOver();
                _inputs.Player.Disable();
            }

            IEdible col;
            if (collision.TryGetComponent(out col))
            {
                _animator.SetTrigger("Eat");
                Grow(col.Consume());
            }
        }

        #endregion

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
                    _transforms.Add(Instantiate(_bodyPrefab, new Vector3(40,0,0), Quaternion.identity).transform);
                }
            }
        }
    }
}
