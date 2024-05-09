using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour
{
    public CheckPoint checkPoint;

    Player_Movement PlayerMovementComponent;
    GameObject Player;
    Unit PlayerUnit;
    public ListOfSwords swordlist;
    bool dashtutorialstart = false;
    bool attacktutorial = false;
    bool dashenable = false;
    Dialogue dialogue;
    public GameObject DialogueBox;

    public GameObject deathscreen;

    public Slider healthbar;
    public Slider easehealthbar;

    [SerializeField]
    GameObject GokuPrefab;
    Unit GokuUnit;
    public GameObject GokuHealthbarCanvas;
    [SerializeField]
    Slider GokuHealth;

    [SerializeField]
    Slider GokuEaseHealth;

    [SerializeField]
    GameObject BlackFade;

    [SerializeField]
    GameObject TutEnding;

    DeathOFDummyDetector DeathOFDummyDetector;

    DeathOfGokuDetector DeathOfGokuDetector;

    public bool oncegate1;
    public bool oncegate2;
    public bool oncegate3;
    public bool oncegate4;

    public bool oncefordashtut;

    public bool oncefordashinput;

    public bool oncefordashthroughbox;

    public bool onceforattacktutorial;

    public bool onceforGokufight;

    public bool onceforTUTFIN;

    public GameObject UI_ofPlayer;

    public GateControl[] gates;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementComponent = FindObjectOfType<Player_Movement>();
        Player = PlayerMovementComponent.gameObject;
        dialogue = FindObjectOfType<Dialogue>();
        DeathOFDummyDetector = FindObjectOfType<DeathOFDummyDetector>();
        GokuUnit = GokuPrefab.GetComponent<Unit>();
        GokuHealthbarCanvas.SetActive(false);
        oncegate1 = false;
        oncegate2 = false;
        oncefordashtut = false;
        oncefordashinput = false;
        oncefordashthroughbox = false;
        onceforattacktutorial = false;
        PlayerUnit = PlayerMovementComponent.gameObject.GetComponent<Unit>();
        DeathOfGokuDetector = FindObjectOfType<DeathOfGokuDetector>();
        DeathOfGokuDetector.gameObject.SetActive(false);
        PlayerUnit.EquipSword(swordlist.translateindexintosword("Toy Light Saber"));
        StartCoroutine(BlackFadeWait());
        if (!checkPoint.GotToBoss) {
            StartCoroutine(TutorialStart());
        }
        else
        {
            Destroy(DeathOFDummyDetector.gameObject);
            dashenable = true;
            UI_ofPlayer.SetActive(true);
            dialogue.index = 19;
            dialogue.counter = 6;
            dialogue.stoppingathisint = 5;
            dialogue.currentgate = 2;
            dialogue.text = dialogue.gameObject.GetComponent<Text>();
            dialogue.text.text = string.Empty;
            Player.transform.position = new Vector3(0, -24.5F, 0);
            DialogueBox.SetActive(false);
        }
    }

    IEnumerator BlackFadeWait()
    {
        Debug.Log("Started to talk");
        yield return new WaitForSeconds(0.75F);
        FindObjectOfType<AudioManager>().Play("Doki Doki");
        BlackFade.GetComponent<Animator>().SetBool("Fade",true);
    }

    IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(1F);
        BlackFade.SetActive(false);
        dialogue.StartDialogue(0);
    }

    void Update()
    {
        if (dialogue.currentgate<gates.Length) {
            if (dialogue.index == dialogue.indexforopengate[dialogue.currentgate])
            {
                //Make new gate everytime you have new module cause I bad at code
                if (!oncegate1&&dialogue.currentgate==0)
                {
                    gates[0].OpenGate();
                    oncegate1 = true;
                }else if (!oncegate2&&dialogue.currentgate==1)
                {
                    gates[1].OpenGate();
                    oncegate2 = true;
                }else if (!oncegate3 && dialogue.currentgate == 2)
                {
                    gates[2].OpenGate();
                    oncegate3 = true;
                }
                else if (!oncegate4 && dialogue.currentgate == 3)
                {
                    gates[3].OpenGate();
                    oncegate4 = true;
                }

                /*
                 * if(!oncegate[dialogue.currentgate]){
                 *      gates[dialogue.currentgate].OpenGate();
                 *      oncegate[dialogue.currentgate] = true;
                 * }
                 */
            }
            if (gates[dialogue.currentgate].playerDetector.playerspotted == true)
            {
                if (dialogue.currentgate==0) {
                    dashtutorialstart = true;
                }else if (dialogue.currentgate == 1)
                {
                    attacktutorial = true;
                }
            }
        }
        if (!oncefordashtut&&dashtutorialstart)
        {
            
            UI_ofPlayer.SetActive(true);
            ActivateDialogue();
            oncefordashtut = true;
        }
        if (!oncefordashinput && dashtutorialstart && UI_ofPlayer.activeInHierarchy && PlayerMovementComponent.dashing&& !DialogueBox.activeInHierarchy)
        {
            ActivateDialogue();
            oncefordashinput = true;
            dialogue.currentgate++;
        }
        if (!onceforattacktutorial&&attacktutorial)
        {
            ActivateDialogue();
            onceforattacktutorial = true;
        }
        if (!checkPoint.GotToBoss && dialogue.index > 18)
        {
            checkPoint.GotToBoss = true;
        }
        if (!onceforGokufight&&gates[2].playerDetector.playerspotted)
        {
            FindObjectOfType<AudioManager>().StopAll();
            FindObjectOfType<AudioManager>().Play("DB Super OP2");
            Goku_Attackpattern_OutSideDungeon goku_pattern = GokuPrefab.GetComponent<Goku_Attackpattern_OutSideDungeon>();
            goku_pattern.GokuHealth = GokuHealth;
            goku_pattern.GokuEaseHealth = GokuEaseHealth;
            goku_pattern.shakingIntensity = 0.5F;
            GameObject tempGoku = Instantiate(GokuPrefab,new Vector3(0,-32.5F,0),Quaternion.identity);
            onceforGokufight = true;
            DeathOfGokuDetector.Goku = tempGoku;
            DeathOfGokuDetector.gameObject.SetActive(true);
            GokuHealthbarCanvas.SetActive(true);
        }
        if (!onceforTUTFIN&&gates[3].playerDetector.playerspotted)
        {
            checkPoint.FinishedTutorial = true;
        }

    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void ActivateFinishedTutorial()
    {
        Player.SetActive(false);
        TutEnding.SetActive(true);
    }

    public void ActivateDialogue()
    {
        DialogueBox.SetActive(true);
        dialogue.NextLine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dashenable&&!dashtutorialstart) {
            PlayerMovementComponent.IsdashCoolDown = true;
        }
        if (UI_ofPlayer.activeInHierarchy)
        {
            healthupdate();
        }
        if (PlayerUnit.checkDead())
        {
            //reset boss battle
            Player.SetActive(false);
            //PlayerStats.Reset();
            deathscreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void settimescaletoone()
    {
        Time.timeScale = 1;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void incrementcurrentgate()
    {
        dialogue.currentgate++;
    }

    public void healthupdate()
    {
        healthbar.value = (PlayerUnit.cHP) / (PlayerUnit.maxHP);
        //currenthealth = PlayerUnit.cHP;

        if (healthbar.value != easehealthbar.value)
        {
            easehealthbar.value = Mathf.Lerp(easehealthbar.value, healthbar.value, 0.01f);
        }

    }

}
