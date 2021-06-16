using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsTxtScript : MonoBehaviour
{
    RectTransform m_RectTransform;
    float x, y;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    IEnumerator PlusTxtCor()
    {
        int count =0;
        x += 5;
        y += 5;

        m_RectTransform.anchoredPosition = new Vector2(x,y);


        yield return new WaitForSecondsRealtime(0.1f);
    }

}
