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

    [SerializeField]
    GameObject container;
    [SerializeField]
    GameObject _lightning, _projectile, _explosion, _fireball;
    [SerializeField]
    List<GameObject> listLightning, listProjectile, listExplosion, listFireball;

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
        listLightning = GeneratePrefabLight(3);
        listProjectile = GeneratePrefabProj(2);
        listExplosion = GeneratePrefabExplosion(3);
        listFireball = GeneratePrefabFireball(2);
    }

    List<GameObject> GeneratePrefabLight(int quantity) //Genera il numero di fulmini per il warrior in una lista
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

    List<GameObject> GeneratePrefabProj(int quantity) //Genera il numero di Proiettili per il mago
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject projectile = Instantiate(_projectile);
            projectile.transform.parent = container.transform;
            projectile.transform.position = new Vector3(container.transform.position.x, container.transform.position.y + 0.5f, container.transform.position.z);
            projectile.SetActive(false);
            listProjectile.Add(projectile);
        }
        return listProjectile;
    }

    List<GameObject> GeneratePrefabExplosion(int quantity) //Genera il numero di esplosioni per il mago
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject explosion = Instantiate(_explosion);
            explosion.transform.parent = container.transform;
            explosion.SetActive(false);
            listExplosion.Add(explosion);
        }
        return listExplosion;
    }

    List<GameObject> GeneratePrefabFireball(int quantity) //Genera il numero di fireball per il mago
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject fireball = Instantiate(_fireball);
            fireball.transform.parent = container.transform;
            fireball.transform.position = new Vector3(container.transform.position.x, container.transform.position.y + 0.5f, container.transform.position.z);
            fireball.SetActive(false);
            listExplosion.Add(fireball);
        }
        return listFireball;
    }

    public GameObject LightningCall() //Genera l'oggetto quando chiamato
    {
        foreach (var prefab in listLightning)
        {
            if(prefab.activeInHierarchy == false)
            {
                prefab.transform.position = PlayerNew.TargetPosition();
                prefab.SetActive(true);
                return prefab;
            }
        }

        GameObject newPrefab = Instantiate(_lightning);
        newPrefab.transform.parent = container.transform;
        listLightning.Add(newPrefab);
        return newPrefab;
    }

    public GameObject CastProjectile() //spara proiettile
    {
        foreach(var prefab in listProjectile)
        {
            if(prefab.activeInHierarchy == false)
            {
                prefab.SetActive(true);
                return prefab;
            }
        }

        GameObject newPreFab = Instantiate(_projectile);
        newPreFab.transform.parent = container.transform;
        listProjectile.Add(newPreFab);
        return newPreFab;
    }

    public GameObject CastExplosion() //spara proiettile
    {
        foreach (var prefab in listExplosion)
        {
            if (prefab.activeInHierarchy == false)
            {
                prefab.transform.position = PlayerNew.TargetPosition() + Vector3.up;
                prefab.SetActive(true);
                return prefab;
            }
        }

        GameObject newPreFab = Instantiate(_explosion);
        newPreFab.transform.parent = container.transform;
        listExplosion.Add(newPreFab);
        return newPreFab;
    }

    public GameObject CastFireball() //spara fireball
    {
        foreach (var prefab in listFireball)
        {
            if (prefab.activeInHierarchy == false)
            {
                prefab.SetActive(true);
                return prefab;
            }
        }

        GameObject newPreFab = Instantiate(_fireball);
        newPreFab.transform.parent = container.transform;
        listFireball.Add(newPreFab);
        return newPreFab;
    }
}
