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
            //외부에서 데이터를 불러오도록 만들어야함
            //0. Lock,
            //1. HP, 2. ATK, 3. SPD, 4. DELAY,
            //5. LV,
            //6. PCOST, 7. UCost,
            //8. MaxHp, 9. MaxAtk, 10. MaxSpd, 11. MaxDly 
            SetUnitStatus("Unit1", 1,  0, 0, 0, 0,   0,    0,  1,    100,  50,  20,  20);
            SetUnitStatus("Unit2", 0,  0, 0, 0, 0,   0,    10, 2,    200,  50,  40,  30);
            SetUnitStatus("Unit3", 0,  0, 0, 0, 0,   0,    20, 3,    300, 100,  20,  40);
            SetUnitStatus("Unit4", 0,  0, 0, 0, 0,   0,    40, 4,    500,   5, 100,  50);
            SetUnitStatus("Unit5", 0,  0, 0, 0, 0,   0,    60, 5,    500,  50,  20,  60);
            SetUnitStatus("Unit6", 0,  0, 0, 0, 0,   0,   100, 6,    600,  60,  30,  70);
            SetUnitStatus("Unit7", 0,  0, 0, 0, 0,   0,   200, 7,   1000,  70,  40,  80);
            SetUnitStatus("Unit8", 0,  0, 0, 0, 0,   0,   300, 8,   1000,  80,  50,  90);
            SetUnitStatus("Unit9", 0,  0, 0, 0, 0,   0,   400, 9,   2000,  90,  60, 100);

            PlayerPrefs.SetInt("MyMoney", 1000);

            PlayerPrefs.SetInt("isStarted", 1);
        }




    }
    #endregion



    public string FocusUnit = null;

    public void SetUnitStatus(string UnitName,
        int Lock ,
        int HP, int ATK, int SPEED, int DELAY,
        int Lv, int PCost, int UCost,
        int MaxHp, int MaxAtk, int MaxSpd, int MaxDly )
    {
        //게임을 첫 번째로 실행했다면 기본 정보 만들기
        //임시로 유닛3까지만 만듬
        int[] number = new int[12];

        number[0] = Lock;
        number[1] = HP; 
        number[2] = ATK;
        number[3] = SPEED;
        number[4] = DELAY;
        number[5] = Lv;
        number[6] = PCost;
        number[7] = UCost;
        number[8] = MaxHp;
        number[9] = MaxAtk;
        number[10] = MaxSpd;
        number[11] = MaxDly;
        

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
