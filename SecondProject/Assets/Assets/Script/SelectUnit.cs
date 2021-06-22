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
    MyMoneyScript myMoneyScript;
    StatusCanvas statusCanvas;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);
        myMoneyScript = GameObject.Find("Money").transform.GetChild(0).gameObject.GetComponent<MyMoneyScript>();
        statusCanvas = GameObject.Find("StatusCanvas").gameObject.GetComponent<StatusCanvas>();


        UnitName = name;

        SetImage();

    }


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
            if(PlayerPrefs.GetInt("MyMoney") >= UnitStatus[6] )
            {
                //유닛 레벨업과 텍스트 바꾸기
                //transform.Find("Text").GetComponent<Text>().text = ++UnitStatus[5] + "/20";
                statusCanvas.UpgradeStatus(UnitName);

                //스프라이트 바꾸기
                ChageSpriteColor(255, 255, 255, 255);

                //다음 유닛 언락하기
                if (NextUnit != null)
                    NextUnit.GetComponent<SelectUnit>().UnLock();

                //내 돈 소비
                PlayerPrefs.SetInt("MyMoney", (PlayerPrefs.GetInt("MyMoney") - UnitStatus[6]));

                //돈 갱신
                myMoneyScript.MyMoneyRefresh();

            }

            //상세창 갱신
            if(GameManager.instance.FocusUnit != UnitName)
            {
                GameManager.instance.FocusUnit = UnitName;
                //Debug.Log("aaaaa" + UnitName);
                //statusCanvas.LoadUnitStatus(UnitName);
            }

        }
        //2 : 구매 되어 있다면 렙 올리기
        else if (UnitStatus[0] == 2)
        {
            
            //상세창 갱신
            if (GameManager.instance.FocusUnit != UnitName)
            {
                GameManager.instance.FocusUnit = UnitName;
                statusCanvas.LoadUnitStatus(UnitName);
            }
            

        }

    }

    

    void SetImage()
    {
        LoadData();

        //UnLock
        if(UnitStatus[0] == 1)
        {
            ChageSpriteColor(100, 100, 100, 255);

            transform.Find("Text").GetComponent<Text>().text = ""+UnitStatus[6];

            GetComponent<Image>().raycastTarget = true;
            
        }
        else if(UnitStatus[0] == 2)
        {
            ChageSpriteColor(255, 255, 255, 255);

            if(UnitStatus[5] == 20)
            {
                transform.Find("Text").GetComponent<Text>().text = "Max Lv";
            }else
            {
                transform.Find("Text").GetComponent<Text>().text = UnitStatus[5] + "/20";
            }
            
            GetComponent<Image>().raycastTarget = true;

        }




    }

    void ChageSpriteColor(float r, float g, float b, float a)
    {
        GetComponent<Image>().color = new Color(r / 255f, g / 255f, b /255, a/255f);
        transform.Find("Unit_img").GetComponent<Image>().color = new Color(r / 255f, g / 255f, b /255, a/255f);
        transform.Find("Text").GetComponent<Text>().color = new Color(1,1,1,1);
    }

    


}
