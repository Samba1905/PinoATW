using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Singleton
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("PoolManager null");

            }
            return _instance;
        }
    }
    #endregion
    [SerializeField] GameObject [] enemies;




    private void Awake()
    {
        #region Singleton
        if (_instance)
        {
            Destroy(gameObject);
        }
        else _instance = this;

        DontDestroyOnLoad(this);
        #endregion
    }
}
