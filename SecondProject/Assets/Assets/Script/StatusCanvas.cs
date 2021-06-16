using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 status Text 그냥 나눠버리기
 */


public class StatusCanvas : MonoBehaviour
{
    int[] UnitStatus;

    Text Hp, Atk, Spd, Delay, UpCost;
    Image HpBar, AtkBar, SpdBar, DlyBar;
    MyMoneyScript myMoneyScript;

    public GameObject PlusTxt;

    string UnitName;

    private void Start()
    {

        myMoneyScript = GameObject.Find("Money").transform.GetChild(0).gameObject.GetComponent<MyMoneyScript>();

        Hp = transform.Find("UnitStatusPanel").Find("HPPanel").Find("HPNum").GetComponent<Text>();
        Atk = transform.Find("UnitStatusPanel").Find("ATTACKPanel").Find("ATTACKNum").GetComponent<Text>();
        Spd = transform.Find("UnitStatusPanel").Find("SPEEDPanel").Find("SPEEDNum").GetComponent<Text>();
        Delay = transform.Find("UnitStatusPanel").Find("DELAYPanel").Find("DELAYNum").GetComponent<Text>();
        UpCost = transform.Find("UnitStatusPanel").Find("UpCostImg").Find("UpCostTxt").GetComponent<Text>();

        HpBar = transform.GetChild(1).Find("HPPanel").GetChild(2).GetChild(0).GetComponent<Image>();
        AtkBar = transform.GetChild(1).Find("ATTACKPanel").GetChild(2).GetChild(0).GetComponent<Image>();
        SpdBar = transform.GetChild(1).Find("SPEEDPanel").GetChild(2).GetChild(0).GetComponent<Image>();
        DlyBar = transform.GetChild(1).Find("DELAYPanel").GetChild(2).GetChild(0).GetComponent<Image>();


        LoadUnitStatus("Unit1");

    }

    public void Upgrade()
    {
        //업글 가능 상태
        //내 돈이 업그레이드 비용보다 크다면
        if (PlayerPrefs.GetInt("MyMoney") >= UnitStatus[7])
        {
            //유닛의 레벨업과 텍스트 바꾸기
            GameObject.Find(UnitName).transform.Find("Text").GetComponent<Text>().text = ++UnitStatus[5] + "/20";

            {
                //temp value
                UnitStatus[1] += 2;
                UnitStatus[2] += 5;
                UnitStatus[3] += 1;
                UnitStatus[4] += 3;
            }


            //변경된 스텟 저장
            GameManager.instance.SetUnitStatus(UnitName, UnitStatus);

            //내 돈 소비
            PlayerPrefs.SetInt("MyMoney", (PlayerPrefs.GetInt("MyMoney") - UnitStatus[7]));

            //돈 갱신 
            myMoneyScript.MyMoneyRefresh();

            RefreshStatus();


        }
    }


    public void LoadUnitStatus(string _UnitName)
    {
        UnitName = _UnitName;
        string[] dataArr = PlayerPrefs.GetString(UnitName).Split(',');

        UnitStatus = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            UnitStatus[i] = System.Convert.ToInt32(dataArr[i]);
        }

        RefreshStatus();

    }

    void RefreshStatus()
    {
        Hp.text = "" + UnitStatus[1];
        Atk.text = "" + UnitStatus[2];
        Spd.text = "" + UnitStatus[3];
        Delay.text = "" + UnitStatus[4];
        UpCost.text = "" + UnitStatus[7];

        HpBar.fillAmount = UnitStatus[1] / 300f;
        AtkBar.fillAmount = UnitStatus[2] / 300f;
        SpdBar.fillAmount = UnitStatus[3] / 300f;
        DlyBar.fillAmount = UnitStatus[4] / 300f;

        var HpPlus = Instantiate(PlusTxt, new Vector3(0f,0f,0f), Quaternion.identity, transform);
        HpPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 620);

        var AtkPlus = Instantiate(PlusTxt, new Vector3(0f,0f,0f), Quaternion.identity, transform);
        AtkPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 490);

        var SpdPlus = Instantiate(PlusTxt, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        SpdPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 355);

        var DlyPlus = Instantiate(PlusTxt, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        DlyPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 215);

    }

}
