using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
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

    [SerializeField]
    GameObject container;
    [SerializeField]
    GameObject _lightning;
    [SerializeField]
    List<GameObject> listLightning;




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

    private void Start()
    {
        container = GameObject.FindGameObjectWithTag("Target");
        listLightning = GeneratePrefab(3);
    }

    List<GameObject> GeneratePrefab(int quantity) //Genera il numero di oggetti in una lista
    {
        for(int i = 0; i < quantity; i++)
        {
            GameObject lightning = Instantiate(_lightning);
            lightning.transform.parent = container.transform;
            lightning.SetActive(false);
            listLightning.Add(lightning);
        }
        return listLightning;
    }
    public GameObject LightningCall() //Genera l'oggetto quando chiamato
    {
        foreach (var prefab in listLightning)
        {
            if(prefab.activeInHierarchy == false)
            {
                prefab.SetActive(true);
                return prefab;
            }
        }

        GameObject newPrefab = Instantiate(_lightning);
        newPrefab.transform.parent = container.transform;
        listLightning.Add(newPrefab);
        return newPrefab;
    }
}
