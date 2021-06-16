using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBtn : MonoBehaviour
{
    Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        transform.parent.parent.GetComponent<StatusCanvas>().Upgrade();
    }

}
