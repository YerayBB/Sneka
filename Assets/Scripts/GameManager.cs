using UnityEngine;

namespace Sneka
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private UIManager _ui;
        [SerializeField]
        private GameObject[] _fruitList;
        [SerializeField]
        private int _fruitInterval = 5;

        private BoxCollider2D _area;
        private int _nextSpawn;
        private int _score = 0;


        #region MonoBehaviourCalls

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            _area = GetComponent<BoxCollider2D>();
            _nextSpawn = _fruitInterval;
        }


        private void Start()
        {
            Food.OnFoodEaten += () =>
            {
                if (_nextSpawn == 1)
                {
                    _nextSpawn = _fruitInterval;
                    SpawnFruit();
                }
                else
                {
                    --_nextSpawn;
                }
            };
            _ui.SetScore(_score);
        }

        #endregion

        /// <returns>A random position inside the playground</returns>
        public Vector3 GetRandomPosition()
        {
            return new Vector3(Mathf.Round(Random.Range(_area.bounds.min.x, _area.bounds.max.x)),Mathf.Round(Random.Range(_area.bounds.min.y, _area.bounds.max.y)));
        }

        public void AddScore(int value)
        {
            _score += value;
            _ui.SetScore(_score);
        }

        public void GameOver()
        {
            _ui.GameOverUI();
        }

        private void SpawnFruit()
        {
            var fruit = Instantiate(_fruitList[Random.Range(0, _fruitList.Length)], GetRandomPosition(), Quaternion.identity);
            _ui.ActiveBar(1f / fruit.GetComponent<Fruit>().duration);
        }
    }
}