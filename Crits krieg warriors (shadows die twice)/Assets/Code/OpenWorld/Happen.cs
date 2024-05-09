using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Happen : MonoBehaviour
{
    public CheckPoint checkPointManager;

    public bool firsttime = true;

    //
    public GameObject[] enemies;
    private float[] enemycHP;

    public GameObject[] ItemSlots;
    public Swords [] swords_player_has;

    public Swords[] swords;

    //Player Objects and UI
    public StatSaver PlayerBaseStats;
    public StatSaver StatsForDungeon;
    public StatSaver PlayerStats;
    public GameObject Player;
    Unit PlayerUnit;
    float currenthealth;
    float currentdamage;
    public Slider healthbar;
    public Slider easehealthbar;

    public GameObject LevelInstructions;
    bool activateLevelInstructionOnce = false;

    //
    public GameObject PlayerUpgradeButtons;
    public Text PlayerName;
    public Text PlayerLevel;
    public Text PlayerHP;
    public Text Playeratk;
    public Text PlayeratkBuffs;
    public Text PlayerLevelPoints;
    public GameObject PlayerStatUI;

    //
    public GameObject Buttons;

    //
    public GameObject Equipped_Inventory_Sword;
    private Image Image_of_Sword;
    private bool change_Weapon = false;

    //
    public GameObject SwordChanger;
    private Animator Sword_Stand_1_E_Animator;
    private SwordChange Sword_Stand_Holder;
    private GameObject PillarInteract;

    //
    public GameObject Enemy_Spawner;
    private Animator EnemySpawner_E_Animator;

    //
    private bool Ishealingovertime = false;

    //
    public GameObject deathscreen;
    public GameObject endscreen;


    //
    WeaponParent weaponParent;
    public GameObject ParticlesForBigBertha;
    public GameObject Ability2;

    //
    public GameObject damagetext_holder;
    Text damagetext;

    //Description Canvas
    public GameObject Description_Template_holder;
    Description_Template description_Template;
    public Sprite Fire_Spirit;
    public Sprite BigBurtha;

    //Ending button
    public GameObject MeteorPrefab;

    private void Awake()
    {
        weaponParent = Player.GetComponentInChildren<WeaponParent>();
        damagetext = damagetext_holder.GetComponent<Text>();
        PillarInteract = SwordChanger.GetComponentInChildren<Pillar_Interaction>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (firsttime) {
            Debug.Log("Reset stuff");
            FindObjectOfType<AudioManager>().Play("BGM");
            swords_player_has = new Swords[1];
            swords_player_has[0] = swords[0];
            //PlayerStats.Reset();
            PlayerUnit = Player.GetComponent<Unit>();
            PlayerUnit.EquipSword(swords_player_has[0]);
            firsttime = false;
        }

        Image_of_Sword = Equipped_Inventory_Sword.GetComponent<Image>();
        Image_of_Sword.sprite = swords_player_has[0].getSprite();

        Sword_Stand_1_E_Animator = SwordChanger.GetComponentInChildren<Animator>();

        Sword_Stand_Holder = SwordChanger.GetComponent<SwordChange>();

        currenthealth = PlayerUnit.cHP;
        currentdamage = PlayerUnit.atk + PlayerUnit.atk * PlayerUnit.sword.getScaling();

        EnemySpawner_E_Animator = Enemy_Spawner.GetComponentInChildren<Animator>();
        description_Template = Description_Template_holder.GetComponent<Description_Template>();

        DeActivePillar();

        enemycHP = new float[enemies.Length];

        for (int i = 0; i<enemies.Length;i++)
        {
            Unit temp = enemies[i].GetComponent<Unit>();
            enemycHP[i] = temp.cHP;
        }

        Debug.Log("Load Saved stats");
        LoadBaseStats();
        PlayerStats.Reset();
        if (checkPointManager.TrialComplete==true)
        {
            EquipSwordToUnit(Sword_Stand_Holder.sword_In_Sword_Stand);

            Image_of_Sword.sprite = PlayerUnit.sword.getSprite();
            if (PlayerUnit.sword.getName().Equals("Big Burtha"))
            {
                ParticlesForBigBertha.SetActive(true);
                Ability2.SetActive(true);
                weaponParent.animator.SetBool("IsBigBertha", true);
            }
        }
        SwordSetUp();

        

        

        StartCoroutine(IntroToEnemy());
        


    }

    public void SavePlayerStats()
    {
        PlayerStats.setStats(PlayerUnit.name, PlayerUnit.level, PlayerUnit.maxHP, PlayerUnit.cHP, PlayerUnit.maxStamina, PlayerUnit.cStamina, PlayerUnit.atk, PlayerUnit.levelpoints, PlayerUnit.sword);
    }

    public void LoadBaseStats()
    {
        PlayerBaseStats.ApplyStats(PlayerUnit);
    }

    public void LoadStatsForDungeon()
    {
        Swords temp =PlayerUnit.sword;
        StatsForDungeon.ApplyStats(PlayerUnit);
        PlayerUnit.sword = temp;
    }

    public void LoadPlayerStatsOnPlayer()
    {
        PlayerStats.ApplyStats(PlayerUnit);
    }

    public void SetTimeScaleToOne()
    {
        FindObjectOfType<AudioManager>().UnPauseAll();
        Time.timeScale = 1;
    }

    public void SetTimeScaletoZero()
    {
        FindObjectOfType<AudioManager>().PauseAll();
        Time.timeScale = 0;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetRun()
    {
        checkPointManager.TrialComplete = false;
    }
    IEnumerator ThreeSecondsAndThenMeteorStrike()
    {
        yield return new WaitForSeconds(3f);
        Instantiate(MeteorPrefab, Player.transform.position, Quaternion.identity);
    }

    IEnumerator IntroToEnemy()
    {
        FindObjectOfType<AudioManager>().PauseAll();
        yield return new WaitForSeconds(1f);
        Description_Template_holder.SetActive(true);
        description_Template.Start();
        description_Template.ChangeUIInfo(Fire_Spirit, "Fire Spirit", "This is a creature made purely of fire. It's only method of attacking is an explosion that takes 1 second to charge. After exploding it becomes tired for 1.5 seconds. This is the recommended window for you to attack.");
        Time.timeScale = 0;
    }

    public void EquipSwordToUnit(Swords a)
    {
        PlayerUnit.EquipSword(a);
    }



    public void AddSwordToInventory(Swords newSword)
    {
        Swords []temparr = new Swords[swords_player_has.Length];
        for (int i=0; i< swords_player_has.Length; i++)
        {
            temparr[i] = swords_player_has[i];
        }
        temparr[swords_player_has.Length] = newSword;
        swords_player_has = temparr;
    }

    public void UpgradeButtonsAppear()
    {
        if (PlayerUnit.checkPlayerUpgradePoints())
        {
            PlayerUpgradeButtons.SetActive(true);
        }
    }

    public void checkifnomorelevelpoints()
    {
        if (PlayerUnit.levelpoints<1)
        {
            PlayerUpgradeButtons.SetActive(false);
        }
    }

    public void IncreaseLevel_Stats()
    {
        /*PlayerUnit.level += 1;
        float temp = PlayerUnit.maxHP * 0.5F;
        PlayerUnit.maxHP += temp;
        PlayerUnit.cHP += temp;
        PlayerUnit.atk += 5;*/
        PlayerUnit.level += 1;
        PlayerUnit.levelpoints += 1;
        PlayerUnit.LevelUp = true;

    }

    public void UpdatePlayerStatUI()
    {
        PlayerName.text = PlayerUnit.name;
        PlayerLevel.text = PlayerUnit.level+"";
        PlayerHP.text = PlayerUnit.maxHP+"";
        Playeratk.text = PlayerUnit.atk+"";
        PlayeratkBuffs.text = "(+" + PlayerUnit.atk * PlayerUnit.sword.getScaling() + ")";
        PlayerLevelPoints.text = PlayerUnit.levelpoints + "";
        Debug.Log("JustUpdated/JustClickedButton To open upgrade menu");
    }

    public void OpenPlayerStatUI()
    {
        UpdatePlayerStatUI();
        UpgradeButtonsAppear();
        PlayerStatUI.SetActive(true);
        Buttons.SetActive(false);
    }

    IEnumerator HealOverTime()
    {
        Ishealingovertime = true;

        float temp1 = PlayerUnit.cHP;
        yield return new WaitForSeconds(5f);
        
        if (temp1>PlayerUnit.cHP)
        {
            Ishealingovertime = false;
            yield break;
        }

        while (PlayerUnit.cHP<PlayerUnit.maxHP)
        {
            PlayerUnit.cHP += 0.05F * PlayerUnit.maxHP;
            float temp2 = PlayerUnit.cHP;
            yield return new WaitForSeconds(1f);
            if (PlayerUnit.cHP > PlayerUnit.maxHP) {
                PlayerUnit.cHP = PlayerUnit.maxHP;
            }
            if (PlayerUnit.cHP<temp2)
            {
                break;
            }
        }
        Ishealingovertime = false;
    }

    // Update is called once per frame
    public void EnemyChecker()
    {
        //updating enemy health
        for (int i = 0; i < enemies.Length; i++)
        {
            Unit Enemy = enemies[i].GetComponent<Unit>();
            if ((Enemy.isActiveAndEnabled)&&(enemycHP[i] != Enemy.cHP)) {
                if ((enemies[i].GetComponentInChildren<Slider>() != null)) {
                    Slider temp = enemies[i].GetComponentInChildren<Slider>();
                    float a = Enemy.cHP;
                    enemycHP[i] = a;
                    float b = Enemy.maxHP;
                    temp.value = (a / b);
                }


                if (Enemy.checkDead())
                {
                    IncreaseLevel_Stats();
                    enemies[i].SetActive(false);
                }
            }
        }
        //End of Checking stuff
    }

    public void healthupdate()
    {
            healthbar.value = (PlayerUnit.cHP) /(PlayerUnit.maxHP);
            currenthealth = PlayerUnit.cHP;

        if (healthbar.value != easehealthbar.value)
        {
            easehealthbar.value = Mathf.Lerp(easehealthbar.value, healthbar.value, 0.01f);
        }

    }

    public void SwordSetUp()
    {
        Image_of_Sword.sprite = PlayerUnit.sword.getSprite();
        if (PlayerUnit.sword.getName().Equals("Big Burtha"))
        {
            ParticlesForBigBertha.SetActive(true);
            Ability2.SetActive(true);
            weaponParent.animator.SetBool("IsBigBertha", true);
            Player.GetComponent<Player_Movement>().Awake();
            Player.GetComponent<Player_Movement>().Start();
            Player.GetComponent<Player_Movement>().swordscaling = Player.GetComponent<Player_Movement>().playersword.getScaling();
            Player.GetComponent<Player_Movement>().setAbility2up();
        }
        Player.GetComponent<Player_Movement>().Awake();
        Player.GetComponent<Player_Movement>().Start();
        Player.GetComponent<Player_Movement>().swordscaling = Player.GetComponent<Player_Movement>().playersword.getScaling();

    }

    public void LoadTester()
    {
        SavePlayerStats();
        SceneManager.LoadScene(3);
    }
    public void replayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DeActivePillar()
    {
        PillarInteract.SetActive(false);
    }

    public void ActivePillar()
    {
        PillarInteract.SetActive(true);
    }

    void Update()
    {
        checkifnomorelevelpoints();
        if (PlayerUnit.level==1&&!activateLevelInstructionOnce)
        {
            LevelInstructions.SetActive(true);
            SetTimeScaletoZero();
            activateLevelInstructionOnce = true;
        }

        if (currentdamage!= PlayerUnit.atk + PlayerUnit.atk * PlayerUnit.sword.getScaling())
        {
            currentdamage = PlayerUnit.atk + PlayerUnit.atk * PlayerUnit.sword.getScaling();
            damagetext.text = "" + (PlayerUnit.atk + PlayerUnit.atk * PlayerUnit.sword.getScaling());
        }

        
        if (Input.GetKeyDown(KeyCode.E)&&(Sword_Stand_1_E_Animator.GetBool("Appear") ==true))
        {
            EquipSwordToUnit(Sword_Stand_Holder.sword_In_Sword_Stand);
            change_Weapon = true;
            FindObjectOfType<AudioManager>().PauseAll();
            Time.timeScale = 0;
            Description_Template_holder.SetActive(true);
            description_Template.Start();
            description_Template.ChangeUIInfo(BigBurtha,swords[3].getName(),swords[3].getDescription());
        }
        if (change_Weapon)
        {
            Image_of_Sword.sprite = PlayerUnit.sword.getSprite();
            if (PlayerUnit.sword.getName().Equals("Big Burtha"))
            {
                ParticlesForBigBertha.SetActive(true);
                Ability2.SetActive(true);
                weaponParent.animator.SetBool("IsBigBertha", true);
            }

            change_Weapon = false;
        }
        if (Input.GetKeyDown(KeyCode.E) && (EnemySpawner_E_Animator.GetBool("Appear") == true))
        {
            for (int i =1; i<6;i++)
            {
                enemies[i].SetActive(true);
            }
            Enemy_Spawner.SetActive(false);
        }

        healthupdate();

      

        EnemyChecker();

        if (PlayerUnit.cHP<PlayerUnit.maxHP&& !Ishealingovertime)
        {
            StartCoroutine(HealOverTime());
        }

        if (PlayerUnit.checkDead())

        {
            Player.SetActive(false);
            PlayerStats.Reset();
            FindObjectOfType<AudioManager>().StopAll();
            deathscreen.SetActive(true);
            Time.timeScale = 0;
        }

       /* if (PlayerUnit.level>=7)
        {
            Player.SetActive(false);
            endscreen.SetActive(true);
            Time.timeScale = 0;
        }*/
    }
}
