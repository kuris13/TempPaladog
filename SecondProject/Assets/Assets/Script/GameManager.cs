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
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //유닛들의 기본 정보 가져와서 뿌려주기
        /*







         */
    }


}
