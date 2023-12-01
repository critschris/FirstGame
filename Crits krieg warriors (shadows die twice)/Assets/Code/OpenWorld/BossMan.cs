using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMan : MonoBehaviour
{

    

    public GameObject BossPillar;
    private Animator floatingE_Animator;
    private bool pillarchecker=true;

    public GameObject Player;
    Unit playerUnit;
    private Vector2 playerposition;
    private Player_Movement player_Movement;

    public GameObject Boss;
    public GameObject BossHealth;
    Unit BossHP;
    float currenthealth;
    public Slider healthbar;
    public Slider easehealthbar;
    SpriteRenderer Boss_Sprite_Renderer;
    public Animator BossAnimator;
    public Boss_Moona_WeaponParent boss_Moona_WeaponParent;
    public Animator attackOutlineAnimator;
    public Animator Darken;

    public Transform pivotForpullingAttackArea;
    public Animator attacOutLinePullingYouInAnimator;
    public PullingInAttack pullingInAttack;
    public bool IsPullingInHori = true;
    public Transform slammingArea;

    public GameObject MeteorPrefab;

    public Transform teleport_position_for_player;

    public Camera mainCamera;
    // Start is called before the first frame update
    void Awake()
    {
        player_Movement = Player.GetComponent<Player_Movement>();
        BossHP = Boss.GetComponent<Unit>();
        currenthealth = BossHP.cHP;
        Boss_Sprite_Renderer = Boss.GetComponent<SpriteRenderer>();
        playerUnit = Player.GetComponent<Unit>();
        floatingE_Animator = BossPillar.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((pillarchecker) && Input.GetKeyDown(KeyCode.E) && (floatingE_Animator.GetBool("Appear") == true))
        {
            Boss.SetActive(true);
            Interact_with_Moona_Boss_Pillar();
        }

        healthupdate();

        if (Player.transform.position.x >= Boss.transform.position.x)
        {
            if (Boss_Sprite_Renderer.flipX == true)
            {
                flip();
            }
            else
            {
                return;
            }
        }
        else
        {
            if (Boss_Sprite_Renderer.flipX == false)
            {
                flip();
            }
            else
            {
                return;
            }
        }

        if (BossHP.cHP<0)
        {
            Boss.SetActive(false);
        }
        
    }


    IEnumerator BossAttackPattern()
    {

        yield return new WaitForSeconds(1.5F);
        playerUnit.setStunned(true);

        float temp = Time.time;
        FindObjectOfType<AudioManager>().Play("BossMusic");
        BossHealth.SetActive(true);
        mainCamera.orthographicSize = 10;

       // while (true) {
            yield return new WaitForSeconds(8F);//Second 8 of the song
            playerUnit.setStunned(false);

            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(3F);
                StartCoroutine(AttackPlayer_Thrust());
            }
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(2F);
                StartCoroutine(AttackPlayer_Thrust());
            }

            Darken.SetBool("Darken", true);
            StartCoroutine(AttackPlayer_Thrust());//24 after finishing this coroutine
            yield return new WaitForSeconds(11F);
            BossHP.Shielded = true;
            Debug.Log("Shielded");
            yield return new WaitForSeconds(11F);
            BossHP.Shielded = false;
            //yield return new WaitForSeconds(1F);
            StartCoroutine(AttackPlayerPullingIn());//46
            yield return new WaitForSeconds(5F);//51
            StartCoroutine(AttackPlayerPullingIn());//52
            yield return new WaitForSeconds(1F);
            yield return new WaitForSeconds(5F);
            StartCoroutine(AttackPlayer_Thrust());//57
            yield return new WaitForSeconds(3F);
            StartCoroutine(AttackPlayer_Thrust());//1:00
            
            for (int i=0; i<5;i++)
            {
                yield return new WaitForSeconds(2F);
                StartCoroutine(AttackPlayer_Thrust());
            }
        

        StartCoroutine(AttackPlayer_Thrust());
        Debug.Log(Time.time-temp);
        Debug.Log("Done For now");
      //  }
        /*
        yield return new WaitForSeconds(3F);//Second 10 of the song
        StartCoroutine(AttackPlayer_Thrust());//Second 11 after finishing this coroutine 
        yield return new WaitForSeconds(3F);//Second 13 of the song
        StartCoroutine(AttackPlayer_Thrust());//Second 14 after finishing this coroutine
        yield return new WaitForSeconds(3F);//Second 16 of the song
        StartCoroutine(AttackPlayer_Thrust());//Second 17 after finishing this coroutine
        yield return new WaitForSeconds(3F);//Second 19 of the song
        StartCoroutine(AttackPlayer_Thrust());//Second 20 after finishing this coroutine
        yield return new WaitForSeconds(2F);//
        StartCoroutine(AttackPlayer_Thrust());//Second 22 after finishing this coroutine
        yield return new WaitForSeconds(2F);//23
        Darken.SetBool("Darken", true);
        StartCoroutine(AttackPlayer_Thrust());//24 after finishing this coroutine
        yield return new WaitForSeconds(11F);//34 at the end
        
        //Shielded phase
        BossHP.Shielded = true;
        Debug.Log("Shielded");
        yield return new WaitForSeconds(11F);//45
        BossHP.Shielded = false;
        //yield return new WaitForSeconds(1F);//46
        //
        StartCoroutine(AttackPlayerPullingIn());//46
        yield return new WaitForSeconds(5F);//51
        StartCoroutine(AttackPlayerPullingIn());//52
        yield return new WaitForSeconds(1F);//52 

        yield return new WaitForSeconds(5F);
        StartCoroutine(AttackPlayer_Thrust());//57
        yield return new WaitForSeconds(3F);
        StartCoroutine(AttackPlayer_Thrust());//1:00
        yield return new WaitForSeconds(3F);
        StartCoroutine(AttackPlayer_Thrust());//1:03*/


        
        


    }

    IEnumerator AttackPlayerPullingIn()
    {
       /* if (IsPullingInHori==true)
        {
            IsPullingInHori = false;
            pivotForpullingAttackArea.transform.Rotate(Vector3.forward,-45);
        }
        else
        {
            IsPullingInHori = true;
            pivotForpullingAttackArea.transform.Rotate(Vector3.forward, 45);
        }*/
        attacOutLinePullingYouInAnimator.SetBool("Appear",true);
        pullingInAttack.Catcher(playerUnit);
        
        if (pullingInAttack.hitPlayer==true)
        {
            Player.transform.position = slammingArea.transform.position;
        }
        yield return new WaitForSeconds(1F);
        attacOutLinePullingYouInAnimator.SetBool("Appear", false);
        pullingInAttack.SlamKwan(playerUnit,playerUnit.maxHP*0.2F);
        playerUnit.Stunned = false;
    }

    IEnumerator AttackPlayer_Thrust()
    {
        playerposition = Player.transform.position;
        boss_Moona_WeaponParent.Playerposition = playerposition;

        boss_Moona_WeaponParent.Aim();
        attackOutlineAnimator.SetBool("Appear",true);
        yield return new WaitForSeconds(0.2F);
        StartCoroutine(MeteorFall());
        yield return new WaitForSeconds(0.6F);
        attackOutlineAnimator.SetBool("Appear", false);
        BossAnimator.SetTrigger("Thrust");
        boss_Moona_WeaponParent.HitPlayer();
        boss_Moona_WeaponParent.AttackMethod(playerUnit.maxHP*0.2F);
        yield return new WaitForSeconds(0.2F);
        

    }

    IEnumerator MeteorFall()
    {
        yield return new WaitForSeconds(0.5F);
        Vector2 displacement = player_Movement.Getinput();
        Vector2 aimMeteor = new Vector2(playerposition.x+ displacement.x*7, playerposition.y+ displacement.y*7);
        Instantiate(MeteorPrefab,aimMeteor, Quaternion.identity);
        
    }

    public void Interact_with_Moona_Boss_Pillar()
    {
        
            FindObjectOfType<AudioManager>().Stop("BGM");
            Vector3 temp = teleport_position_for_player.position;
            Player.transform.position = temp;
            Destroy(BossPillar);
            pillarchecker = false;
            StartCoroutine(BossAttackPattern());
        
    }

    void flip()
    {
        Boss_Sprite_Renderer.flipX = !Boss_Sprite_Renderer.flipX;
    }

    public void healthupdate()
    {
        
            healthbar.value = (BossHP.cHP) / (BossHP.maxHP);
            currenthealth = BossHP.cHP;
       

        if (healthbar.value != easehealthbar.value)
        {
            easehealthbar.value = Mathf.Lerp(easehealthbar.value, healthbar.value, 0.01f);
        }
        
    }
}
