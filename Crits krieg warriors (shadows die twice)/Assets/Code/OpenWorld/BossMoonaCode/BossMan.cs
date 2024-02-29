using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMan : MonoBehaviour
{

    public Transform locationzero;
    public Transform locationone;
    public Transform locationtwo;
    public Transform locationthree;
    public Transform locationfour;
    public bool moving = false;
    public Transform destination;
    public float speed;

    bool nomoresetting=false;
    public GameObject BossPillar;
    public GameObject PillarGrid;
    private Animator floatingE_Animator;
    private bool pillarchecker=true;

    public GameObject Player;
    Unit playerUnit;
    private Vector2 playerposition;
    private Player_Movement player_Movement;

    public GameObject Boss;
    MoonaMoveMent moonaMoveMent;
    public GameObject bottomSpriteHolder;
    public GameObject BossHealth;
    Unit BossHP;
    float currenthealth;
    public Slider healthbar;
    public Slider easehealthbar;
    SpriteRenderer Boss_Sprite_Renderer;
    SpriteRenderer BottomSprite;
    public Animator BossAnimator;
    public Boss_Moona_WeaponParent boss_Moona_WeaponParent;
    public Animator attackOutlineAnimator;
    public Animator Darken;

    public Transform pivotForpullingAttackArea;
    public Animator attacOutLinePullingYouInAnimator;
    public PullingInAttack pullingInAttack;
    public bool IsPullingInHori = false;
    public Transform slammingArea;

    public GameObject MeteorPrefab;

    public Transform teleport_position_for_player;

    public Camera mainCamera;

    public GameObject smiteeffectprefab;

    public MoonaFireWeaponParent MoonaFireWeapon;
    bool once = false;

    public GameObject endscreen;
    // Start is called before the first frame update
    void Awake()
    {
        player_Movement = Player.GetComponent<Player_Movement>();
        BossHP = Boss.GetComponent<Unit>();
        currenthealth = BossHP.cHP;
        Boss_Sprite_Renderer = Boss.GetComponent<SpriteRenderer>();
        playerUnit = Player.GetComponent<Unit>();
        floatingE_Animator = BossPillar.GetComponentInChildren<Animator>();
        BottomSprite = bottomSpriteHolder.GetComponent<SpriteRenderer>();
        moonaMoveMent = Boss.GetComponent<MoonaMoveMent>();
        BossPillar.SetActive(false);
        PillarGrid.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (playerUnit.level>=6&& !nomoresetting)
        {
            PillarGrid.SetActive(true);
            BossPillar.SetActive(true);
            nomoresetting = true;
        }
        else if(playerUnit.level<6)
        {
            return;
        }

        if ((pillarchecker) && Input.GetKeyDown(KeyCode.E) && (floatingE_Animator.GetBool("Appear") == true))
        {
            Interact_with_Moona_Boss_Pillar();
        }

        healthupdate();

        if (Player.transform.position.x >= Boss.transform.position.x)
        {
            if (Boss_Sprite_Renderer.flipX == true)
            {
                flip();
            }
            
        }
        else
        {
            if (Boss_Sprite_Renderer.flipX == false)
            {
                flip();
            }
            
        }

        

    }

    private void FixedUpdate()
    {
        /*if (moving)
        {
            Debug.Log("Moving");
            transform.position = Vector3.MoveTowards(Boss.transform.position, destination.position, Time.fixedDeltaTime * speed);
            CloseEnough();
        }*/
    }


    IEnumerator BossAttackPattern()
    {

        yield return new WaitForSeconds(1.5F);
        playerUnit.setStunned(true);

        float temp = Time.time;
        FindObjectOfType<AudioManager>().Play("BossMusic");
        BossHealth.SetActive(true);
        mainCamera.orthographicSize = 10;
        yield return new WaitForSeconds(1F);
        playerUnit.setStunned(false);
        yield return new WaitForSeconds(5F);
        Boss.SetActive(true);
        yield return new WaitForSeconds(1F);
        //Debug.Log("Move function active to location 2");
        
        yield return new WaitForSeconds(1F);
        moveTo(locationthree);
        while (Boss.activeInHierarchy) {

            for (int i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(3F);
                StartCoroutine(AttackPlayer_Thrust());
            }
            moveTo(locationtwo);
            for (int i = 0; i < 2; i++)
            {
                yield return new WaitForSeconds(2F);
                StartCoroutine(AttackPlayer_Thrust());
            }
            //moveTo(locationone);

            Darken.SetBool("Darken", true);
            StartCoroutine(AttackPlayer_Thrust());//24 after finishing this coroutine
            yield return new WaitForSeconds(5F);
            moveTo(locationfour);
            yield return new WaitForSeconds(5F);
            moveTo(locationzero);
            yield return new WaitForSeconds(1F);
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
            moveTo(locationone);
            StartCoroutine(AttackPlayer_Thrust());//57
            yield return new WaitForSeconds(3F);
            StartCoroutine(AttackPlayer_Thrust());//1:00
            randomlocation();
            for (int i = 0; i < 5; i++)
            {
                if (i==2||i==5)
                {
                    randomlocation();
                }
                yield return new WaitForSeconds(2F);
                StartCoroutine(AttackPlayer_Thrust());

            }
            
            //1:10
            yield return new WaitForSeconds(2F);
            StartCoroutine(AttackPlayer_Thrust());
            StartCoroutine(MeteorInPlace());//1:12
            yield return new WaitForSeconds(2F);
            moveTo(locationzero);
            yield return new WaitForSeconds(3F);
            StartCoroutine(AttackPlayerPullingIn());//1:17
            yield return new WaitForSeconds(3F);
            StartCoroutine(FireProjectTile());
            once = true;
            //1:20 projectile
            //1:22 explode
            randomlocation();
            Debug.Log(Time.time - temp);


            Debug.Log("Done For now");
        }
    }

    public void CloseEnough()
    {
        float Bossxcoordinate = Boss.transform.position.x;
        float Bossycoordiante = Boss.transform.position.y;
        if (destination.position.x -1 <Bossxcoordinate &&Bossxcoordinate< destination.position.x +1&& destination.position.y - 1< Bossycoordiante&& Bossycoordiante< destination.position.y + 1)
        {
            moving = false;
        }
        
    }

    public void randomlocation()
    {
        int random = Random.Range(0,5);
        moveTo(randomIntToLocation(random));
    }

    public Transform randomIntToLocation(int a)
    {
        if (a == 0)
        {
            return locationzero;
        }
        else if (a==1)
        {
            return locationone;
        }
        else if (a == 2)
        {
            return locationtwo;
        }
        else if (a == 3)
        {
            return locationthree;
        }
        else if (a == 4)
        {
            return locationfour;
        }
        else
        {
            return locationzero;
        }
    }

    public void moveTo(Transform a)
    {
        Debug.Log("Move set to true");
        moonaMoveMent.moving = true;
        moonaMoveMent.destination = a;
        StartCoroutine(continuosFire());
    }

    IEnumerator continuosFire()
    {
        for (int i =0;i<10 ;i++)
        {
            MoonaFireWeapon.Aim();
            MoonaFireWeapon.Fire();
            yield return new WaitForSeconds(0.2F);
        }
    }

    IEnumerator FireProjectTile()
    {
        if (!once) {
            while (Boss.activeInHierarchy)
            {
                yield return new WaitForSeconds(5F);
                MoonaFireWeapon.Aim();
                MoonaFireWeapon.Fire();

            }
        }
    }

    IEnumerator AttackPlayerPullingIn()
    {
        if (IsPullingInHori==true)
        {
            IsPullingInHori = false;
            pivotForpullingAttackArea.transform.Rotate(Vector3.forward*(-45F));
        }
        else
        {
            IsPullingInHori = true;
            pivotForpullingAttackArea.transform.Rotate(Vector3.forward*45F);
        }
        attacOutLinePullingYouInAnimator.SetBool("Appear",true);
        yield return new WaitForSeconds(0.5F);
        pullingInAttack.Catcher(playerUnit);
        if (pullingInAttack.hitPlayer==true)
        {
            Player.transform.position = slammingArea.transform.position;
        }
        yield return new WaitForSeconds(1.5F);
        attacOutLinePullingYouInAnimator.SetBool("Appear", false);
        Instantiate(smiteeffectprefab,Player.transform);
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
        boss_Moona_WeaponParent.AttackMethod(15);
        yield return new WaitForSeconds(0.2F);
        

    }

    IEnumerator MeteorFall()
    {
        yield return new WaitForSeconds(0.5F);
        Vector2 displacement = player_Movement.Getinput();
        Vector2 aimMeteor = new Vector2(playerposition.x+ displacement.x*7, playerposition.y+ displacement.y*7);
        Instantiate(MeteorPrefab,aimMeteor, Quaternion.identity);
        
    }

    IEnumerator MeteorInPlace()
    {
        yield return new WaitForSeconds(0.5F);
        Vector2 aimMeteor = new Vector2(playerposition.x , playerposition.y );
        Instantiate(MeteorPrefab, aimMeteor, Quaternion.identity);
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
        BottomSprite.flipX = !BottomSprite.flipX;
    }

    public void healthupdate()
    {
        
            healthbar.value = (BossHP.cHP) / (BossHP.maxHP);
            currenthealth = BossHP.cHP;
       

        if (healthbar.value != easehealthbar.value)
        {
            easehealthbar.value = Mathf.Lerp(easehealthbar.value, healthbar.value, 0.01f);
        }

        if (BossHP.Isdead)
        {
            Boss.SetActive(false);
            Player.SetActive(false);
            endscreen.SetActive(true);
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().PauseAll();
        }

    }
}
