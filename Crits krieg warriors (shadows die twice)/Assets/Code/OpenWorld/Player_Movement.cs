using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Exception;

public class Player_Movement : MonoBehaviour
{
    public float AttackRange;
    public float movementSpeed;
    public float dashspeed;
    private Vector2 direction;

    //Dashing ability
    public float dashtime = 2;
    float dashtimeCounter;
    public bool dashing;
    public bool IsdashCoolDown;
    public float dashCoolDownTime;
    float dashCoolDownTimeCounter;
    public GameObject CoolDown1BlurrObject;
    Image CoolDown1Blurr;

    //Ability2CoolDown
    public bool IsAbility2CoolDown;
    public float Ability2CoolDownTime;
    float Ability2CoolDownTimeCounter;
    public GameObject CoolDown2BlurrObject;
    Image CoolDown2Blurr;

    public Rigidbody2D rb;


    private Vector2 input;
    private Vector2 pointerInput;

    public Animator animator;
    Unit playerUnit;
    public Swords playersword;
    public float swordscaling;
    public Animator effectsAnimator;

    public LayerMask solidObjectLayer;
    public LayerMask Player;
    public LayerMask EnemyLayer;

    private WeaponParent weaponParent;

    private void Awake()
    {
        weaponParent = gameObject.GetComponentInChildren<WeaponParent>();
        CoolDown1Blurr = CoolDown1BlurrObject.GetComponent<Image>();
        playerUnit = gameObject.GetComponent<Unit>();

    }

    private void Start()
    {
        IsdashCoolDown = true;
        IsAbility2CoolDown = false;
        dashCoolDownTimeCounter = dashCoolDownTime;
        playersword = playerUnit.sword;
        //swordscaling = playersword.getScaling();
        dashtimeCounter = dashtime;
    }

    private void Update()
    {
        // Movement();

        //Input for movement
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input != Vector2.zero)
        {
            animator.SetFloat("X", input.x);
            animator.SetFloat("Y", input.y);
        }

        animator.SetFloat("Speed", input.sqrMagnitude);


        //Mouse finding
        pointerInput = GetPointerInput();
        weaponParent.Pointerposition = pointerInput;
        direction = (pointerInput - (Vector2)transform.position).normalized;
       

        if (!dashing&&!IsdashCoolDown&&Input.GetKeyDown(KeyCode.Space))
        {
            CoolDown1Blurr.fillAmount = 1;
            Vector2 targetPOS = (direction);
            StartCoroutine(Dash(targetPOS));
        }
        if (!dashing && Input.GetMouseButtonDown(0))
        {
            PlayerAttack();
        }
        if (!dashing &&!IsAbility2CoolDown&& Input.GetKeyDown(KeyCode.Q))
        {
            CoolDown2Blurr.fillAmount = 1;
            PlayerAbility2();
        }
        if (playerUnit.Stunned==true)
        {
            effectsAnimator.SetBool("Stunned",true);
        }
        else
        {
            effectsAnimator.SetBool("Stunned", false);
        }

        if (playerUnit.LevelUp==true)
        {
            effectsAnimator.SetTrigger("LevelUP");
            playerUnit.LevelUp = false;
        }

        if (playersword!=playerUnit.sword)
        {
            playersword = playerUnit.sword;
            swordscaling = playersword.getScaling();
            setAbility2up();
        }
       

    }

    private void FixedUpdate()
    {
        if (IsdashCoolDown == true)
        {

            dashCoolDownTimeCounter -= Time.fixedDeltaTime;
            CoolDown1Blurr.fillAmount -= 1 / dashCoolDownTime * Time.fixedDeltaTime;
            if (dashCoolDownTimeCounter <= 0)
            {
                dashCoolDownTimeCounter = dashCoolDownTime;
                CoolDown1Blurr.fillAmount = 0;
                IsdashCoolDown = false;
            }
        }
        if (IsAbility2CoolDown == true)
        {
            Ability2CoolDownTimeCounter -= Time.fixedDeltaTime;
            CoolDown2Blurr.fillAmount -= 1 / Ability2CoolDownTime * Time.fixedDeltaTime;
            if (Ability2CoolDownTimeCounter <= 0)
            {
                Ability2CoolDownTimeCounter = Ability2CoolDownTime;
                CoolDown2Blurr.fillAmount = 0;
                IsAbility2CoolDown = false;
            }
        }
        if (playerUnit.Stunned == false && !dashing)
        {
            Move();
        }
    }

    public void PlayerAbility2()
    {
        IsAbility2CoolDown = true;
        Ability2CoolDownTimeCounter = Ability2CoolDownTime;
        Debug.Log("CoolDown Active");
        weaponParent.Ability2Active(playerUnit.atk + playerUnit.atk * swordscaling,playersword);
        
    }

    public Vector2 Getinput()
    {
        return new Vector2(animator.GetFloat("X"), animator.GetFloat("Y"));
    }

    private void PlayerAttack()
    {
        weaponParent.AttackMethod(playerUnit.atk + playerUnit.atk * swordscaling);
    }

    


    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }


    //New Movement
    private void Move()
    {
        input = input.normalized;
        rb.MovePosition(rb.position + input * movementSpeed * Time.fixedDeltaTime);
    }

    IEnumerator Dash(Vector2 targetPOS)
    {
        dashing = true;
        playerUnit.Invulnarable = true;
        while (dashtimeCounter>0)
        {

            if (playerUnit.Stunned == true)
            {
                break;
            }

            rb.MovePosition(rb.position+new Vector2(targetPOS.x*0.15F, targetPOS.y * 0.15F));
            yield return new WaitForSeconds(0.01F);
            dashtimeCounter -= 0.01F;
         
        }
        playerUnit.Invulnarable = false;
        dashtimeCounter = dashtime;
        IsdashCoolDown = true;
        dashing = false;
        
        yield return null;
        
        
    }

    public void setAbility2up()
    {
        IsAbility2CoolDown = true;
        Ability2CoolDownTime = playersword.getAbility2CoolDown();
        Ability2CoolDownTimeCounter = Ability2CoolDownTime;
        CoolDown2Blurr = CoolDown2BlurrObject.GetComponent<Image>();
    }

    //Old movement
    //Very clunky, slow and was made for different game
    /*void Movement()
    {
        //Input Checking
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!moving)
            {
                var targetPOSD = transform.position;
                targetPOSD.x += animator.GetFloat("X") * 2;
                targetPOSD.y += animator.GetFloat("Y") * 2;

                if (IsWalkable(targetPOSD))
                {
                    StartCoroutine(Dash(targetPOSD));
                }
            }
            else
            {
                return;
            }
        }
        else if ((!moving) && (!dashing))
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {

                animator.SetFloat("X", input.x);
                animator.SetFloat("Y", input.y);

                var targetPOS = transform.position;
                targetPOS.x += input.x;
                targetPOS.y += input.y;

                if (IsWalkable(targetPOS))
                {
                    StartCoroutine(Move(targetPOS));
                }

            }
        }


        animator.SetBool("IsMoving", moving);
    }*/
    /*void Attack()
    {
        animator.SetTrigger("Attack");


        Collider2D[] hit_enemies = new Collider2D [1];
        if (animator.GetFloat("X") == 0 && animator.GetFloat("Y") == -1)
        {
            Collider2D[] hit_enemies_front = Physics2D.OverlapCircleAll(attack_front.position, AttackRange, EnemyLayer);
            hit_enemies = hit_enemies_front;
        }

        if (animator.GetFloat("X") == 0 && animator.GetFloat("Y") == 1)
        {
            Collider2D[] hit_enemies_up = Physics2D.OverlapCircleAll(attack_up.position, AttackRange, EnemyLayer);
            hit_enemies = hit_enemies_up;
        }

        if (animator.GetFloat("X") == -1 && animator.GetFloat("Y") == 0)
        {
            Collider2D[] hit_enemies_left = Physics2D.OverlapCircleAll(attack_left.position, AttackRange, EnemyLayer);
            hit_enemies = hit_enemies_left;
        }

        if (animator.GetFloat("X") == 1 && animator.GetFloat("Y") == 0)
        {
            Collider2D[] hit_enemies_right = Physics2D.OverlapCircleAll(attack_right.position, AttackRange, EnemyLayer);
            hit_enemies = hit_enemies_right;
        }

        //foreach (Collider2D enemy in hit_enemies)
        try
        {
            Collider2D enemy = hit_enemies[0];

            //{
            if (enemy.GetComponentInChildren<Attacked>() != null)
            {
                Attacked slash_animation = enemy.GetComponentInChildren<Attacked>();

                slash_animation.GetCut();
            }
            if (enemy.GetComponentInChildren<Floating_Text>() != null)
            {
                Floating_Text damagenumber = enemy.GetComponentInChildren<Floating_Text>();

            }
            if (enemy.GetComponent<Unit>() != null)
            {
                Unit hit = enemy.GetComponent<Unit>();

                hit.takeDamage(player.atk+player.atk*player.sword.getScaling());
            }
            // }
        }
        catch (System.Exception noenemy)
        {

            return;
        }
    }
    */
   

   

   

    public bool IsWalkable(Vector3 TargetPOS)
    {
        if(Physics2D.OverlapCircle(TargetPOS, 0.3f, solidObjectLayer) != null)
        {
            return false;
        }
        else if (Physics2D.OverlapCircle(TargetPOS,0.3f,Player)!=null)
        {
            return false;
        }
        return true;
    }

    

}
