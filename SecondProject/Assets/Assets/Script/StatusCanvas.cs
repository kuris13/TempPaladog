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
            
            UpgradeStatus(UnitName);

            //내 돈 소비
            PlayerPrefs.SetInt("MyMoney", (PlayerPrefs.GetInt("MyMoney") - UnitStatus[7]));

            //돈 갱신 
            myMoneyScript.MyMoneyRefresh();

        }
    }

    public void UpgradeStatus(string _UnitName)
    {
        LoadUnitStatus(_UnitName);

        //유닛의 레벨업과 텍스트 바꾸기
        GameObject.Find(_UnitName).transform.Find("Text").GetComponent<Text>().text = ++UnitStatus[5] + "/20";

        //스텟 업그레이드
        UnitStatus[1] += UnitStatus[8]  / 20;
        UnitStatus[2] += UnitStatus[9]  / 20;
        UnitStatus[3] += UnitStatus[10] / 20;
        UnitStatus[4] += UnitStatus[11] / 20;

        Debug.Log("set "+ _UnitName +"  : hp" + UnitStatus[1] + ", atk" + UnitStatus[2] + ", spd" + UnitStatus[3] + ", dly" + UnitStatus[4] );

        //변경된 스텟 저장
        GameManager.instance.SetUnitStatus(_UnitName, UnitStatus);

        RefreshStatus();

        UpgradeStatusPopUpTxt(UnitStatus[8] / 20, UnitStatus[9] / 20, UnitStatus[10] / 20, UnitStatus[11] / 20);

    }


    public void LoadUnitStatus(string _UnitName)
    {
        UnitName = _UnitName;

        Debug.Log(UnitName +" "+ _UnitName);

        string[] dataArr = PlayerPrefs.GetString(UnitName).Split(',');

        UnitStatus = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            UnitStatus[i] = System.Convert.ToInt32(dataArr[i]);
        }

        Debug.Log("loadStatus " + UnitName + "  : hp" + UnitStatus[1] + ", atk" + UnitStatus[2] + ", spd" + UnitStatus[3] + ", dly" + UnitStatus[4]);


        RefreshStatus();

    }

    public void RefreshStatus()
    {
        Debug.Log("Refresh " + UnitName + "  : hp" + UnitStatus[1] + ", atk" + UnitStatus[2] + ", spd" + UnitStatus[3] + ", dly" + UnitStatus[4]);


        Hp.text = "" + UnitStatus[1];
        Atk.text = "" + UnitStatus[2];
        Spd.text = "" + UnitStatus[3];
        Delay.text = "" + UnitStatus[4];
        UpCost.text = "" + UnitStatus[7];

        HpBar.fillAmount  = UnitStatus[1] / UnitStatus[8];
        AtkBar.fillAmount = UnitStatus[2] / UnitStatus[9];
        SpdBar.fillAmount = UnitStatus[3] / UnitStatus[10];
        DlyBar.fillAmount = UnitStatus[4] / UnitStatus[11];

        
    }

    void UpgradeStatusPopUpTxt(int Hp, int Atk, int Spd, int Dly)
    {
        var HpPlus = Instantiate(PlusTxt, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        HpPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 620);
        HpPlus.GetComponent<Text>().text = "+"+Hp;


        var AtkPlus = Instantiate(PlusTxt, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        AtkPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 490);
        AtkPlus.GetComponent<Text>().text = "+"+Atk;

        var SpdPlus = Instantiate(PlusTxt, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        SpdPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 355);
        SpdPlus.GetComponent<Text>().text = "+"+Spd;

        var DlyPlus = Instantiate(PlusTxt, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
        DlyPlus.GetComponent<RectTransform>().anchoredPosition = new Vector2(1230, 215);
        DlyPlus.GetComponent<Text>().text = "+"+Dly;

    }




}
