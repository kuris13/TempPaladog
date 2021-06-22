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
    Text UnitNameTxt;
    Text UnitDesHeader, UnitDesContainer;

    Image HpBar, AtkBar, SpdBar, DlyBar;
    Image UnitImage;
    MyMoneyScript myMoneyScript;

    public GameObject PlusTxt;
    public GameObject PopUpPanel;
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

        UnitNameTxt = transform.GetChild(0).Find("UnitNameTxt").GetComponent<Text>();
        UnitImage = transform.GetChild(0).Find("UnitImg").GetComponent<Image>();

        UnitDesHeader = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        UnitDesContainer = transform.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>();



        LoadUnitStatus("Unit1");


    }

    public void Upgrade()
    {
        //업글 가능 상태
        //내 돈이 업그레이드 비용보다 크다면
        if (PlayerPrefs.GetInt("MyMoney") >= UnitStatus[7] * UnitStatus[5])
        {
            if(UnitStatus[5] < 20)
            {
                UpgradeStatus(UnitName);
            }
            else
            {
                //이미 만렙입니다.
                var PopUp = Instantiate(PopUpPanel, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
                PopUp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                PopUp.transform.GetChild(0).GetComponent<Text>().text = "- 이미 Max Lv에 도달했습니다 -";

            }
            
        }
        else
        {
            //골드가 부족합니다.
            var PopUp = Instantiate(PopUpPanel, new Vector3(0f, 0f, 0f), Quaternion.identity, transform);
            PopUp.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            PopUp.transform.GetChild(0).GetComponent<Text>().text = "- 골드가 부족합니다 -";
        }
        
    }

    public void UpgradeStatus(string _UnitName)
    {
        LoadUnitStatus(_UnitName);

        
        //스텟 업그레이드
        UnitStatus[1] += UnitStatus[8]  / 20;
        UnitStatus[2] += UnitStatus[9]  / 20;
        UnitStatus[3] += UnitStatus[10] / 20;
        UnitStatus[4] += UnitStatus[11] / 20;

        UnitStatus[0] = 2;

        //내 돈 소비
        PlayerPrefs.SetInt("MyMoney", (PlayerPrefs.GetInt("MyMoney") - UnitStatus[7] * UnitStatus[5]));

        //돈 갱신 
        myMoneyScript.MyMoneyRefresh();

        Debug.Log("업글 전 Unit Lv" + UnitStatus[5]);

        //유닛의 레벨업과 텍스트 바꾸기
        if(UnitStatus[5] < 19)
        {
            GameObject.Find(_UnitName).transform.Find("Text").GetComponent<Text>().text = ++UnitStatus[5] + "/20";
        }
        else if(UnitStatus[5] == 19)
        {
            ++UnitStatus[5];
            GameObject.Find(_UnitName).transform.Find("Text").GetComponent<Text>().text = "MAX Lv";
        }

        Debug.Log("업글 후  Unit Lv" + UnitStatus[5]);
        //변경된 스텟 저장
        GameManager.instance.SetUnitStatus(_UnitName, UnitStatus);

        RefreshStatus();

        UpgradeStatusPopUpTxt(UnitStatus[8] / 20, UnitStatus[9] / 20, UnitStatus[10] / 20, UnitStatus[11] / 20);

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

    public void RefreshStatus()
    {
        
        //스텟 정보 리프레시
        Hp.text = "" + UnitStatus[1];
        Atk.text = "" + UnitStatus[2];
        Spd.text = "" + UnitStatus[3];
        Delay.text = "" + UnitStatus[4];
        UpCost.text = "" + UnitStatus[7] * UnitStatus[5];

        HpBar.fillAmount = UnitStatus[1] / 1000f;
        AtkBar.fillAmount = UnitStatus[2] / 100f;
        SpdBar.fillAmount = UnitStatus[3] / 100f;
        DlyBar.fillAmount = UnitStatus[4] / 100f;

        //유닛 이름 & 그림 리프레시
        UnitNameTxt.text = UnitName;

        string ImgName = "UnitImg/" + UnitName;

        UnitImage.sprite = Resources.Load<Sprite>(ImgName) as Sprite;

        //유닛 설명 리프레시


        string a = "UnitDesHeader" + UnitStatus[12];

        string UnitDesHeaderTxt =   PlayerPrefs.GetString("UnitDesHeader" + UnitStatus[12]);

        UnitDesHeader.text = UnitDesHeaderTxt;



        string UnitDesContainerTxt = PlayerPrefs.GetString("UnitDesContainer" + UnitStatus[13]);

        UnitDesContainer.text = UnitDesContainerTxt;



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


    int FibonacciSequence(int n)
    {
        if (n == 0)
            return 0;

        int one = 1;
        int two = 1;

        if (n == one)
            return one;
        else if (n == 2)
            return two;
        else
            return FibonacciSequence(n - 1) + FibonacciSequence(n - 2);
    }

}
