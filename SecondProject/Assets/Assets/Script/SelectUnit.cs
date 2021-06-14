using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectUnit : MonoBehaviour
{
    

    Button btn;
    string UnitName;



    public void OnClickButton()
    {
        UnitName = name;


        //UnitName을 이용하여 유닛 설명판 띄우기


        //Unit의 해금 상태를 구하기 -> Unit의 정보 불러오기


        //해금 상태에 따라
        //0 : 해금되어 있지 않은 유닛은 터치 불가능!!
        //1 : 해금 할 수 있으면 해금하기
        //2 : 해금이 되어 있다면 렙 올리기
        






        Debug.Log(UnitName);
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);
    }
}
