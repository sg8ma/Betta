using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewPosition : MonoBehaviour
{
    public Transform m_content;
    public ScrollRect m_scroll;

    void Start()
    {
        m_scroll.verticalNormalizedPosition = 0.1f;
    }

    void Update()
    {
        
    }
}
