using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectUnit : MonoBehaviour
{
    

    Button btn;
    string UnitName;
    int[] UnitStatus;
    public GameObject NextUnit;



    public void UnLock()
    {
        LoadData();

        UnitStatus[0] = 1;
        GameManager.instance.SetUnitStatus(UnitName, UnitStatus);

        //이미지 바꿔주기
        SetImage();
        GetComponent<Image>().raycastTarget = true;


        
    }

    public void LoadData()
    {
        string[] dataArr = PlayerPrefs.GetString(UnitName).Split(',');

        UnitStatus = new int[dataArr.Length];

        for (int i = 0; i < dataArr.Length; i++)
        {
            UnitStatus[i] = System.Convert.ToInt32(dataArr[i]);
        }
    }


    public void OnClickButton()
    {
        LoadData();



        //UnitName을 이용하여 유닛 설명판 띄우기


        //Unit의 해금 상태를 구하기 -> Unit의 정보 불러오기


        //해금 상태에 따라
        //0 : 해금되어 있지 않은 유닛은 터치 불가능!!
        if (UnitStatus[0] ==0 )
        {
            //빨간 이미지 상태

        }
        //1 : 구매 할 수 있으면 구매하기
        else if (UnitStatus[0] == 1)
        {
            //회색 이미지 상태
            //내가 가진 돈이 요구치 보다 많다면 해금 
            if(PlayerPrefs.GetInt("MyMoney") > 100 )
            {

                transform.Find("Text").GetComponent<Text>().text = ++UnitStatus[5] + "/20";
                Debug.Log("해금됨");

                //유닛스텟 업데이트 하기
                UnitStatus[0] = 2;

                //스프라이트 바꾸기
                //transform.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                //transform.Find("Unit_img").GetComponent<Image>().color = new Color(255, 255, 255, 255);
                
                //다음 유닛 언락하기
                if(NextUnit != null)
                    NextUnit.GetComponent<SelectUnit>().UnLock();


                GameManager.instance.SetUnitStatus(UnitName,UnitStatus);
            }

        }
        //2 : 구매 되어 있다면 렙 올리기
        else if (UnitStatus[0] == 2)
        {
            //업글 가능 상태

        }

        Debug.Log(UnitName);
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);

        UnitName = name;

        SetImage();

    }

    void SetImage()
    {
        LoadData();

        //UnLock
        if(UnitStatus[0] == 1)
        {
            GetComponent<Image>().color = new Color(100, 100, 100, 255);
            transform.Find("Unit_img").GetComponent<Image>().color = new Color(100, 100, 100, 255);
        }
        /*
        else if(UnitStatus[0] == 2)
        {
            GetComponent<Image>().color = new Color(255, 255, 255, 255);
            transform.Find("Unit_img").GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }

         */

    }




}
