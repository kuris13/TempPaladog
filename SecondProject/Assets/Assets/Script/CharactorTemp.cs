using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CharactorTemp : MonoBehaviour
{
    Button btn;
    string UnitName;


    public void OnClickButton()
    {
        UnitName = name;

        Debug.Log(UnitName);
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);
    }

    
}
