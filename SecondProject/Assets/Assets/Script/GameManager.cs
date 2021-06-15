using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        if (!isStarted)
        {
            SetUnitStatus("Unit1", 2, 1, 1, 1, 1, 0);
            SetUnitStatus("Unit2", 1, 10, 10, 10, 10, 0);
            SetUnitStatus("Unit3", 0, 20, 20, 20, 20, 0);
            SetUnitStatus("Unit4", 0, 30, 30, 30, 30, 0);
            SetUnitStatus("Unit5", 0, 30, 30, 30, 30, 0);
            SetUnitStatus("Unit6", 0, 30, 30, 30, 30, 0);
            SetUnitStatus("Unit7", 0, 30, 30, 30, 30, 0);
            SetUnitStatus("Unit8", 0, 30, 30, 30, 30, 0);
            SetUnitStatus("Unit9", 0, 30, 30, 30, 30, 0);

            PlayerPrefs.SetInt("MyMoney", 1000);


        }




    }
    #endregion

    bool isStarted = false;


    public void SetUnitStatus(string UnitName,int Lock ,int HP, int ATK, int SPEED, int DELAY, int Lv)
    {
        //게임을 첫 번째로 실행했다면 기본 정보 만들기
        //임시로 유닛3까지만 만듬
        int[] number = new int[6];

        number[0] = Lock;
        number[1] = HP; 
        number[2] = ATK;
        number[3] = SPEED;
        number[4] = DELAY;
        number[5] = Lv;

        string strArr = "";

        //배열과 , 를 번갈아가면서 저장
        for (int i = 0; i < number.Length; i++)
        {
            strArr = strArr + number[i];

            if (i < number.Length - 1) // 마지막에는 ,를 넣지 않음
            {
                strArr = strArr + ",";
            }
        }

        PlayerPrefs.SetString(UnitName, strArr);
    }

    public void SetUnitStatus(string UnitName, int[] UnitStatus)
    {
        //게임을 첫 번째로 실행했다면 기본 정보 만들기
        //임시로 유닛3까지만 만듬

        string strArr = "";

        //배열과 , 를 번갈아가면서 저장
        for (int i = 0; i < UnitStatus.Length; i++)
        {
            strArr = strArr + UnitStatus[i];

            if (i < UnitStatus.Length - 1) // 마지막에는 ,를 넣지 않음
            {
                strArr = strArr + ",";
            }
        }

        PlayerPrefs.SetString(UnitName, strArr);
    }


    // Start is called before the first frame update
    void Start()
    {
        

        //유닛들의 기본 정보 가져와서 뿌려주기
        /*
            






         */
    }


}
