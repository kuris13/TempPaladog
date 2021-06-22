using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopUp : MonoBehaviour
{
    RectTransform m_RectTransform;

    float y;
    public int count = 0;

    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        y = m_RectTransform.anchoredPosition.y;

        StartCoroutine(UpgradePopUpCor());
    }

    IEnumerator UpgradePopUpCor()
    {


        while (count < 10)
        {
            ++count;

            y += 2;

            m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x, y);

            yield return new WaitForSecondsRealtime(0.05f);
        }

        //필요하다면 오브젝트 풀로 바꾸기
        Destroy(gameObject);

    }
}
