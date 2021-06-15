using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyMoneyScript : MonoBehaviour
{
    Text txt;


    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();

        MyMoneyRefresh();

    }

    public void MyMoneyRefresh()
    {
        txt.text = PlayerPrefs.GetInt("MyMoney") + "";
    }
}
