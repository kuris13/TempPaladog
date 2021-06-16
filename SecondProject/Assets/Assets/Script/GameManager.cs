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


        if (PlayerPrefs.GetInt("isStarted")  != 1) 
        {
            //0Lock, 1HP, 2ATK, 3SPD, 4DELAY, 5LV, 6PCOST 7UCost
            SetUnitStatus("Unit1", 2,  1,  1,  1,  1, 1,  0 ,1);
            SetUnitStatus("Unit2", 1, 10, 10, 10, 10, 0, 10 , 2);
            SetUnitStatus("Unit3", 0, 20, 20, 20, 20, 0, 20, 3);
            SetUnitStatus("Unit4", 0, 30, 30, 30, 30, 0, 30, 4);
            SetUnitStatus("Unit5", 0, 40, 30, 30, 30, 0, 40, 5);
            SetUnitStatus("Unit6", 0, 50, 30, 30, 30, 0, 100, 6);
            SetUnitStatus("Unit7", 0, 60, 30, 30, 30, 0, 200, 7);
            SetUnitStatus("Unit8", 0, 70, 30, 30, 30, 0, 500, 8);
            SetUnitStatus("Unit9", 0, 80, 30, 30, 30, 0, 1000, 9);

            PlayerPrefs.SetInt("MyMoney", 1000);

            PlayerPrefs.SetInt("isStarted", 1);
        }




    }
    #endregion



    public string FocusUnit = null;

    public void SetUnitStatus(string UnitName,int Lock ,int HP, int ATK, int SPEED, int DELAY, int Lv, int PCost, int UCost)
    {
        //게임을 첫 번째로 실행했다면 기본 정보 만들기
        //임시로 유닛3까지만 만듬
        int[] number = new int[8];

        number[0] = Lock;
        number[1] = HP; 
        number[2] = ATK;
        number[3] = SPEED;
        number[4] = DELAY;
        number[5] = Lv;
        number[6] = PCost;
        number[7] = UCost;

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
<<<<<<< Updated upstream
            
=======
        
>>>>>>> Stashed changes






         */
    }


}
