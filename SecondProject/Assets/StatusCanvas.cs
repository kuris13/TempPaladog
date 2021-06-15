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
    Button btn;
    MyMoneyScript myMoneyScript;

    string UnitName;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);
        myMoneyScript = GameObject.Find("Money").transform.GetChild(0).gameObject.GetComponent<MyMoneyScript>();

        Hp = transform.Find("UnitStatusPanel").Find("HPPanel").Find("HP").GetComponent<Text>();
        Atk = transform.Find("UnitStatusPanel").Find("ATTACKPanel").Find("ATTACK").GetComponent<Text>();
        Spd = transform.Find("UnitStatusPanel").Find("SPEEDPanel").Find("SPEED").GetComponent<Text>();
        Delay = transform.Find("UnitStatusPanel").Find("DELAYPanel").Find("DELAY").GetComponent<Text>();
        UpCost = transform.Find("UnitStatusPanel").Find("UpCostImg").Find("UpCostTxt").GetComponent<Text>();


        LoadUnitStatus("Unit1");

    }

    public void OnClickButton()
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

            refreshStatus();


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

        refreshStatus();

    }

    void refreshStatus()
    {
        Hp.text = "H P     " + UnitStatus[1];
        Atk.text = "ATTACK  " + UnitStatus[2];
        Spd.text = "SPEED  " + UnitStatus[3];
        Delay.text = "DELAY  " + UnitStatus[4];
        UpCost.text = "" + UnitStatus[7];
    }

}
