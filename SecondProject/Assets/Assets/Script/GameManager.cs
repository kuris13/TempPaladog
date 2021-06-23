using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    // AWS IAM
    CognitoAWSCredentials credentials;

    // DynamoDB connect
    DynamoDBContext context;
    AmazonDynamoDBClient DBclient;

    void Awake()
    {
        #region Singleton

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        // AWS IAM
        UnityInitializer.AttachToGameObject(this.gameObject);
        credentials = new CognitoAWSCredentials("ap-northeast-2:1ea7b175-b0a2-4f54-ab28-1db51221f9eb",
            RegionEndpoint.APNortheast2);

        // DynamoDB
        DBclient = new AmazonDynamoDBClient(credentials, RegionEndpoint.APNortheast2);
        context = new DynamoDBContext(DBclient);

        CreateUnit();
        FindItem();

        #region PlayerPrefs

        if (PlayerPrefs.GetInt("isStarted") != 1)
        {
            //외부에서 데이터를 불러오도록 만들어야함
            //0. Lock,
            //1. HP, 2. ATK, 3. SPD, 4. DELAY,
            //5. LV,
            //6. PCOST, 7. UCost,
            //8. MaxHp, 9. MaxAtk, 10. MaxSpd, 11. MaxDly 
            SetUnitStatus("Unit1", 1, 0, 0, 0, 0, 0, 0, 1, 100, 50, 20, 20, 1, 1);
            SetUnitStatus("Unit2", 0, 0, 0, 0, 0, 0, 10, 2, 200, 50, 40, 30, 2, 2);
            SetUnitStatus("Unit3", 0, 0, 0, 0, 0, 0, 20, 3, 300, 100, 20, 40, 3, 3);
            SetUnitStatus("Unit4", 0, 0, 0, 0, 0, 0, 40, 4, 500, 20, 100, 50, 4, 4);
            SetUnitStatus("Unit5", 0, 0, 0, 0, 0, 0, 60, 5, 500, 50, 20, 60, 5, 5);
            SetUnitStatus("Unit6", 0, 0, 0, 0, 0, 0, 100, 6, 600, 60, 30, 70, 6, 6);
            SetUnitStatus("Unit7", 0, 0, 0, 0, 0, 0, 200, 7, 1000, 70, 40, 80, 7, 7);
            SetUnitStatus("Unit8", 0, 0, 0, 0, 0, 0, 300, 8, 1000, 80, 50, 90, 8, 8);
            SetUnitStatus("Unit9", 0, 0, 0, 0, 0, 0, 400, 9, 2000, 90, 60, 100, 9, 9);

            PlayerPrefs.SetInt("MyMoney", 1000);

            PlayerPrefs.SetInt("isStarted", 1);

            //임시 유닛 설명문
            //나중에 바꿀 예정
            PlayerPrefs.SetString("UnitDesHeader1", "Unit1의 설명문 제목");
            PlayerPrefs.SetString("UnitDesContainer1", "Unit 설명문의 컨테이너입니다. \n 두 번쨰 줄입니다. ");

            PlayerPrefs.SetString("UnitDesHeader2", "Unit2의 설명문 제목");
            PlayerPrefs.SetString("UnitDesContainer2", "Unit2 설명문의 컨테이너입니다. \n 두 번쨰 줄입니다. ");

            PlayerPrefs.SetString("UnitDesHeader3", "Unit3의 설명문 제목");
            PlayerPrefs.SetString("UnitDesContainer3", "Unit3 설명문의 컨테이너입니다. \n 두 번쨰 줄입니다. ");

            PlayerPrefs.SetString("UnitDesHeader4", "Unit4의 설명문 제목");
            PlayerPrefs.SetString("UnitDesContainer4", "Unit4 설명문의 컨테이너입니다. \n 두 번쨰 줄입니다. ");
        }


        #endregion

    }


    [DynamoDBTable("Unit_Info")]
    public class Unit
    {
        [DynamoDBHashKey] // Hash Key
        public string UnitName { get; set; }
        [DynamoDBProperty]
        public int Lock { get; set; }
        [DynamoDBProperty]
        public int Hp { get; set; }
        [DynamoDBProperty]
        public int Atk { get; set; }
        [DynamoDBProperty]
        public int Spd { get; set; }
        [DynamoDBProperty]
        public int Dly { get; set; }
        [DynamoDBProperty]
        public int Lv { get; set; }
        [DynamoDBProperty]
        public int PCost { get; set; }
        [DynamoDBProperty]
        public int UCost { get; set; }

    }

    private void CreateUnit()
    {
        Unit unit1 = new Unit
        {
            UnitName = "Unit10",
            Lock = 0,
            Hp = 1,
            Atk = 2,
            Spd = 3,
            Dly = 4,
            Lv = 5,
            PCost = 2,
            UCost = 2
        };

        context.SaveAsync(unit1, (result) =>
        {
            if (result.Exception == null)
                Debug.Log("Success!");
            else
                Debug.Log(result.Exception);

        });
    }

    public void FindItem()
    {
        Unit unit;
        context.LoadAsync<Unit>("Unit1", (AmazonDynamoDBResult<Unit> AmazonDynamoDBResult) =>
         {
             if(AmazonDynamoDBResult.Exception != null)
             {
                 Debug.LogException(AmazonDynamoDBResult.Exception);
                 return;
             }

             unit = AmazonDynamoDBResult.Result;
             Debug.Log(unit.UnitName);
             Debug.Log(unit.Atk);
             Debug.Log(unit.Hp);


         }, null);
    }

    public string FocusUnit = null;

    public void SetUnitStatus(string UnitName,
        int Lock,
        int HP, int ATK, int SPEED, int DELAY,
        int Lv, int PCost, int UCost,
        int MaxHp, int MaxAtk, int MaxSpd, int MaxDly,
        int UnitDesHeader, int UnitDesContainer)
    {
        //게임을 첫 번째로 실행했다면 기본 정보 만들기
        //임시로 유닛3까지만 만듬
        int[] number = new int[14];

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
        number[12] = UnitDesHeader;
        number[13] = UnitDesContainer;





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


}
