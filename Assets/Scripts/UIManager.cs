using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Sneka {
    public class UIManager : MonoBehaviour
    {
        private Animator _barAnimator;
        private Animator _animator;
        private TMP_Text _score;
        private Controls _inputs;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _barAnimator = transform.GetChild(0).GetComponent<Animator>();
            _score = transform.GetChild(2).GetComponent<TMP_Text>();
            _inputs = new Controls();
            _inputs.Menu.Retry.performed += (a) => Retry();
            _inputs.Menu.Quit.performed += (a) => Quit();
        }

        public void ActiveBar(float duration)
        {
            _barAnimator.SetFloat("Duration", duration);
            _barAnimator.SetTrigger("Active");
        }

        public void SetScore(int value)
        {
            _score.text = value.ToString("00000000");
        }

        public void GameOverUI()
        {
            _inputs.Menu.Enable();
            _animator.SetTrigger("GameOver");
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            SceneManager.LoadScene("Tittle");
        }
    }
}
