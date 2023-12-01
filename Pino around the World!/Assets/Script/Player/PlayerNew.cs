using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerNew : MonoBehaviour
{
    float _healtsPoints, _staminaPoints, _darkEnergyPoints;
    float maxHP = 100f, minHP = 0f;
    float maxSTA = 100f, minSTA = 0f, overSTA = -25;
    float maxDE = 100f, minDE = 0f;
    float timerInvulnerable;
    float timerUI, timerPG;
    int changeUI, changePG;
    Color32 fullColor255, transparent195, transparent135, transparent75;
    Color32 chColorDmg190, chColorDmg125, chColorDmg60, chColorRed;
    [SerializeField] bool recoverySTA, exhaustState, deadState, vulnerable;
    [Header("UI")]
    [SerializeField] Image STABColor;
    [SerializeField] Slider HPB, STAB, DEB;

    [Header("Danno per prove")]
    public bool DANNO;

    [Header("Posizioni per Spell")]
    static Vector3 mousePos;
    GameObject target;

    PlayerMovement playerM;
    Warrior warrior;
    Mage mage;
    Barbarian barbarian;
    [HideInInspector]
    public GameObject w, m, b;
    [Header("Altro")]
    [SerializeField]
    TriggerArea triggerArea;
    GameManager.SlotGame SG; //Era pubblico in caso di prob
    ScoreManager SM;

    public AudioClip hitDmg, shieldClip;

    bool tutorial, lvl1, lvl2, lvl3, lvl4, lvl5;

    public bool Tutorial { get { return tutorial; } }
    public bool Lvl1 { get { return lvl1; } }
    public bool Lvl2 { get { return lvl2; } }
    public bool Lvl3 { get { return lvl3; } }
    public bool Lvl4 { get { return lvl4; } }
    public bool Lvl5 { get { return lvl5; } }

    public float HealtsPoints
    {
        get
        {
            return _healtsPoints;
        }
        private set
        {
            if (value > maxHP)
            {
                _healtsPoints = maxHP;
            }
            else if (value < minHP)
            {
                _healtsPoints = minHP;
            }
            else
            {
                _healtsPoints = value;
            }
        }
    }

    public float StaminaPoints
    {
        get
        {
            return _staminaPoints;
        }
        private set
        {
            if(value > maxSTA)
            { 
                _staminaPoints = maxSTA;
                exhaustState = false;
            }
            else if(value < minSTA)
            {
                _staminaPoints = value;
                exhaustState = true;
                if(value < overSTA)
                {
                    _staminaPoints = overSTA;
                }
            }
            else
            {
                _staminaPoints = value;
            }
        }
    }

    public bool ExhaustState
    {
        get
        {
            return exhaustState;
        }
    }

    public float DarkEnergyPoints
    {
        get 
        { 
            return _darkEnergyPoints;
        }
        private set
        { 
            if(value > maxDE)
            {
                _darkEnergyPoints = maxDE;
            }
            else if(value < minDE)
            {
                _darkEnergyPoints = minDE;
            }
            else
            {
                _darkEnergyPoints = value;
            }
        }
    }

    [SerializeField]
    private Material _matW, _matM, _matB;

    public enum Character
    {
        Warrior,
        Mage,
        Barbarian
    }

    private void Awake()
    {
        switch(SG)
        {
            case GameManager.SlotGame.Slot1:
                if (File.Exists(Application.persistentDataPath + "/Slot1Data")) LoadLvl1();
                break;
            case GameManager.SlotGame.Slot2:
                if (File.Exists(Application.persistentDataPath + "/Slot2Data")) LoadLvl2();
                break;
            case GameManager.SlotGame.Slot3:
                if (File.Exists(Application.persistentDataPath + "/Slot3Data")) LoadLvl3();
                break;
        }

        _matW = GameObject.Find("Toon Paladin").GetComponent<Renderer>().material;
        _matM = GameObject.Find("Toon Wizard").GetComponent<Renderer>().material;
        _matB = GameObject.Find("Toon Barbarian").GetComponent<Renderer>().material;
        warrior = GetComponentInChildren<Warrior>();
        mage = GetComponentInChildren<Mage>();
        barbarian = GetComponentInChildren<Barbarian>();
        w = GameObject.Find("Warrior");
        m = GameObject.Find("Mage");
        b = GameObject.Find("Barbarian");

        STABColor = GameObject.Find("BarraSTAB").GetComponent<Image>();
        HPB = GameObject.Find("HPBar").GetComponent<Slider>();
        DEB = GameObject.Find("DEBar").GetComponent<Slider>();
        STAB = GameObject.Find("STABar").GetComponent<Slider>();
    }

    private void Start()
    {
        recoverySTA = true;
        HealtsPoints = maxHP;
        StaminaPoints = maxSTA;
        DarkEnergyPoints = 50f;
        vulnerable = true;
        playerM = GetComponent<PlayerMovement>();
        target = GameObject.FindGameObjectWithTag("Target");
        if (GameObject.FindGameObjectWithTag("TriggerScene")) triggerArea = GameObject.Find("TriggerArea").GetComponent<TriggerArea>();
        if (SM == null) SM = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        m.SetActive(false);
        b.SetActive(false);
        fullColor255 = new Color32(255, 255, 255, 255);
        transparent195 = new Color32(255, 255, 255, 195);
        transparent135 = new Color32(255, 255, 255, 135);
        transparent75 = new Color32(255, 255, 255, 75);
        chColorDmg190 = new Color32(255, 190, 190, 255);
        chColorDmg125 = new Color32(255, 125, 125, 255);
        chColorDmg60 = new Color32(255, 60, 60, 255);
        chColorRed = new Color32(255, 0, 0, 255);
    }

    private void Update()
    {
        UpdateSTA(0f);
        IsDead();
        Timer();
        UIPlayer();
        FeedbackDamage();
        ChangeClass();
        TargetPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("TriggerScene"))
        {
            switch(triggerArea.currentLevel)
            {
                case TriggerArea.LevelComplete._tutorial:
                    tutorial = true;
                    break;
                case TriggerArea.LevelComplete._level1:
                    lvl1 = true;
                    break;
                case TriggerArea.LevelComplete._level2:
                    lvl2 = true;
                    break;
                case TriggerArea.LevelComplete._level3:
                    lvl3 = true;
                    break;
                case TriggerArea.LevelComplete._level4:
                    lvl4 = true;
                    break;
                case TriggerArea.LevelComplete._level5:
                    lvl5 = true;
                    break;
            }
        }
    }

    public void UpdateSTA(float riduzioneStamina) //Funzione per creare una meccanica di stamina
    {
        if (recoverySTA && !exhaustState) StaminaPoints += Time.deltaTime * 25f;
        else if (recoverySTA && exhaustState) StaminaPoints += Time.deltaTime * 17.5f;

        if (playerM.consumeRun)
        {
            recoverySTA = false;
            StaminaPoints -= Time.deltaTime * 15f;
        }

        if(playerM.consumeDash)
        {
            playerM.consumeDash = false;
            StaminaPoints -= 22.5f;
        }

        if (warrior.isAttacking) recoverySTA = false;

        if (warrior.isShielding) recoverySTA = false;

        if (!warrior.isAttacking && !playerM.consumeRun && !warrior.isShielding) recoverySTA = true;

        StaminaPoints -= riduzioneStamina;
    }

    public float UpdateHP(float value, bool damage) //Funzione per gestire la vita del player
    {
        if (vulnerable && !warrior.isShielding)
        {
            if (damage)
            {
                playerM.animW.SetTrigger("TakeDamage");
                playerM.animM.SetTrigger("TakeDamage");
                playerM.animB.SetTrigger("TakeDamage");
                playerM.audioSourceSFX.PlayOneShot(hitDmg);
                timerInvulnerable = 1.5f;
                InvulnerableStatus(true);
                return HealtsPoints -= value;
            }
        }
        if(damage && warrior.isShielding)
        {
            UpdateSTA(15);
            playerM.audioSourceSFX.PlayOneShot(shieldClip);
            return 0;
        }
        if (damage) return 0;
        return HealtsPoints += value;
    }

    public float UpdateDarkEnergy(float value, bool consume)
    {
        if (consume) return DarkEnergyPoints -= value;
        return DarkEnergyPoints += value;
    }

    void FeedbackDamage()
    {
        timerPG += Time.deltaTime;
        if(!vulnerable)
        {
            if(timerPG > 0.05f)
            {
                changePG++;
                switch(changePG)
                {
                    case 0:
                        _matW.color = fullColor255;
                        _matM.color = fullColor255;
                        _matB.color = fullColor255;
                        timerPG = 0;
                        break;
                    case 1:
                        _matW.color = chColorDmg190;
                        _matM.color= chColorDmg190;
                        _matB.color= chColorDmg190;
                        timerPG = 0;
                        break;
                    case 2:
                        _matW.color = chColorDmg125;
                        _matM.color = chColorDmg125;
                        _matB.color = chColorDmg125;
                        timerPG = 0;
                        break;
                    case 3:
                        _matW.color = chColorDmg60;
                        _matM.color = chColorDmg60;
                        _matB.color = chColorDmg60;
                        timerPG = 0;
                        break;
                    case 4:
                        _matW.color = chColorRed;
                        _matM.color = chColorRed;
                        _matB.color = chColorRed;
                        timerPG = 0;
                        break;
                    case 5:
                        changePG = 0;
                        goto case 0;
                }
            }
        }
        else
        {
            _matW.color = fullColor255;
            _matM.color = fullColor255;
            _matB.color = fullColor255;
        }
    }

    bool IsDead() //Funzione per gestire lo stato morto del player
    {
        playerM.animW.SetBool("isDead", deadState);
        playerM.animM.SetBool("isDead", deadState);
        playerM.animB.SetBool("isDead", deadState);
        if (HealtsPoints <= 0)
        {
            SM.playerDeath = true;
            return deadState = true;
        }           
        return deadState = false;
    }

    bool InvulnerableStatus(bool invulnerable) //Funzione per invulnerabilità
    {
        if (invulnerable) return vulnerable = false;
        return vulnerable = true;
    }

    void ChangeClass()
    {
        switch(playerM.presentCh)
        {
            case (Character)0:
                w.SetActive(true);
                m.SetActive(false);
                b.SetActive(false);
                break;
            case (Character)1:
                w.SetActive(false);
                m.SetActive(true);
                b.SetActive(false);
                break;
            case (Character)2:
                w.SetActive(false);
                m.SetActive(false);
                b.SetActive(true);
                break;
        }
    }

    void Timer() //Timer per invulenrabilità
    {
        timerInvulnerable -= Time.deltaTime;
        if (timerInvulnerable < 0) InvulnerableStatus(false);
    }

    void UIPlayer()
    {
        HPB.value = HealtsPoints;
        STAB.value = StaminaPoints;
        DEB.value = DarkEnergyPoints;
        timerUI += Time.deltaTime;
        if (exhaustState) //Rende la barra stamina lampeggiante quando il personaggio è stanco
        {
            if (timerUI > 0.08f)
            {
                changeUI++;
                switch (changeUI)
                {
                    case 0:
                        STABColor.color = fullColor255;
                        timerUI = 0f;
                        break;
                    case 1:
                        STABColor.color = transparent195;
                        timerUI = 0f;
                        break;
                    case 2:
                        STABColor.color = transparent135;
                        timerUI = 0f;
                        break;
                    case 3:
                        STABColor.color = transparent75;
                        timerUI = 0f;
                        break;
                    case 4:
                        changeUI = 0;
                        goto case 1;
                }
            }
        }
        else
        {
            changeUI = 0;
            STABColor.color = fullColor255;
        }
    }

    public static Vector3 TargetPosition()
    {
        mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    void LoadLvl1()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Slot1Data");

        Game game = JsonUtility.FromJson<Game>(json);

        lvl1 = game.level1;
        lvl2 = game.level2;
        lvl3 = game.level3;
        lvl4 = game.level4;
        lvl5 = game.level5;
    }

    void LoadLvl2()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Slot2Data");

        Game game = JsonUtility.FromJson<Game>(json);

        lvl1 = game.level1;
        lvl2 = game.level2;
        lvl3 = game.level3;
        lvl4 = game.level4;
        lvl5 = game.level5;
    }

    void LoadLvl3()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/Slot3Data");

        Game game = JsonUtility.FromJson<Game>(json);

        lvl1 = game.level1;
        lvl2 = game.level2;
        lvl3 = game.level3;
        lvl4 = game.level4;
        lvl5 = game.level5;
    }
}
