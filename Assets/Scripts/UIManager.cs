using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private Animator _animator;
    private TMP_Text _score;

    private void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _score = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void ActiveBar(float duration)
    {
        _animator.SetFloat("Duration", duration);
        _animator.SetTrigger("Active");
    }

    public void SetScore(int value)
    {
        _score.text = value.ToString("00000000");
    }
}
