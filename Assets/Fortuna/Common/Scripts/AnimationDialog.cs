using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationDialog : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private int m_layer;
    readonly int m_animatorIsOpen = Animator.StringToHash("IsOpen");
    public bool GetIsOpen()
    {
        return gameObject.activeSelf;
    }
    private bool m_isTransition;
    public bool GetIsTransition()
    {
        return m_isTransition;
    }
    private void SetIsTransition(bool value)
    {
        m_isTransition = value;
    }

    public void Open()
    {
        Debug.Log("Open");
        if (GetIsOpen() || GetIsTransition())
        {
            Debug.Log("OpenLog");
            Debug.Log("GetIsOpen: " + GetIsOpen());
            Debug.Log("GetIsTransition: " + GetIsTransition());
            return;
        }
        gameObject.SetActive(true);
        m_animator.SetBool(m_animatorIsOpen, true);
        StartCoroutine(WaitAnimation("Shown"));
    }

    public void Close()
    {
        if (!GetIsOpen() || GetIsTransition())
        {
            return;
        }
        m_animator.SetBool(m_animatorIsOpen, false);
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        SetIsTransition(true);
        yield return new WaitUntil(() =>
        {
            var state = m_animator.GetCurrentAnimatorStateInfo(m_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        SetIsTransition(false);
        onCompleted?.Invoke();
    }

    // ************************************デバッグ用
    private string GetCurrentAnimationStateName()
    {
        string currentStateName = "";
        if (m_animator.GetCurrentAnimatorStateInfo(m_layer).IsName("Shown"))
        {
            currentStateName = "Shown";
        }
        if (m_animator.GetCurrentAnimatorStateInfo(m_layer).IsName("Hidden"))
        {
            currentStateName = "Hidden";
        }
        return currentStateName;
    }

}
