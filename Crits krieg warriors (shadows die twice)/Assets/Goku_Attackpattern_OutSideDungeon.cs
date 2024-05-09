using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goku_Attackpattern_OutSideDungeon : MonoBehaviour
{
    public Transform playerposition;
    public bool Charge;
    public bool Fire;
    public bool BeamBefore;
    public bool Kame;
    public Animator GokuAnimator;
    public SpriteRenderer GokuSprite;
    //public DungeonCam dungeonCam;
    public Follow CameraFollow;

    public Animator KameAnimator;

    public Transform ParticlePivot;
    public GameObject ParticleForKame;

    public Unit Goku;
    public Slider GokuHealth;
    public Slider GokuEaseHealth;

    public Transform KameFirePoint;
    public GameObject Projectileprefab;

    public float movementSpeed;
    public Vector2 input;
    Rigidbody2D rb;
    public bool moving;
    public float movingCounter = 0;
    float movingTime = 1f;

    public float shakingIntensity;

    public Transform EnergyBallTransform;
    public Transform EnergyBallFirepoint1;
    public Transform EnergyBallFirepoint2;

    public GameObject EnergyBallprefab;


    // Start is called before the first frame update
    void Awake()
    {
        Goku = gameObject.GetComponent<Unit>();
        playerposition = FindObjectOfType<Player_Movement>().gameObject.transform;
        CameraFollow = FindObjectOfType<Camera>().gameObject.GetComponent<Follow>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AttackPattern());
    }

    IEnumerator AttackPattern()
    {
        while (true)
        {
            float random = Random.Range(3, 5);
            randomMovement();
            StartCoroutine(FireEnergyBalls());
            yield return new WaitForSeconds(random);

            Charge = true;
            FindObjectOfType<AudioManager>().Play("Kamehame");
            ParticleForKame.SetActive(true);
            //
            yield return new WaitForSeconds(2.5F);
            ParticleForKame.SetActive(false);
            GokuAnimator.SetBool("End", false);
            KameAnimator.SetBool("End", false);
            Fire = true;
            KamePointFixer();
            BeamBefore = true;
            yield return new WaitForSeconds(0.5F);
            Kame = true;

            FireKameHameHa();
            StartCoroutine(CameraFollow.Shake(1*shakingIntensity, 0.25F*shakingIntensity));
            yield return new WaitForSeconds(1F);
            ResetRotation();
            Charge = false;
            GokuAnimator.SetBool("Charge", Charge);
            GokuAnimator.SetBool("End", true);
            Fire = false;
            BeamBefore = false;
            KameAnimator.SetBool("BeamBefore", BeamBefore);
            KameAnimator.SetBool("End", true);

            Kame = false;
            yield return new WaitForSeconds(1F);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Charge && !Fire)
        {
            GokuSprite.flipX = false;
            Aim();

            if (GokuSprite.flipY == false && ParticlePivot.localPosition.y != -0.1395F)
            {
                ParticlePivot.localPosition = new Vector3(ParticlePivot.localPosition.x, -0.1395F, 0);
            }
            else if (GokuSprite.flipY == true && ParticlePivot.localPosition.y != 0.1395F)
            {
                ParticlePivot.localPosition = new Vector3(ParticlePivot.localPosition.x, 0.1395F, 0);
            }
        }
        else if (Fire)
        {
            GokuSprite.flipX = false;
        }
        else
        {
            if (playerposition.position.x > transform.position.x)
            {
                GokuSprite.flipX = false;
            }
            else if (playerposition.position.x < transform.position.x)
            {
                GokuSprite.flipX = true;
            }
        }
        GokuAnimator.SetBool("Charge", Charge);
        GokuAnimator.SetBool("Fire", Fire);

        KameAnimator.SetBool("BeamBefore", BeamBefore);
        KameAnimator.SetBool("Kame", Kame);

        if (GokuHealth.IsActive()) { 
            GokuHealth.value = (Goku.cHP / Goku.maxHP);
            if (GokuHealth.value != GokuEaseHealth.value)
            {
                GokuEaseHealth.value = Mathf.Lerp(GokuEaseHealth.value, GokuHealth.value, 0.01f);
            }
        }
        if (Goku.checkDead())
        {
            GokuHealth.gameObject.SetActive(false);
            GokuEaseHealth.gameObject.SetActive(false);
            Destroy(gameObject);
        }


    }

    public void FixedUpdate()
    {
        if (movingCounter > movingTime)
        {
            moving = false;
        }
        else
        {
            movingCounter += Time.fixedDeltaTime;
            MoveGoku();
        }

    }

    public void ResetRotation()
    {
        Vector2 direction = new Vector2(1, 0);
        GokuSprite.gameObject.transform.right = direction;

        GokuSprite.flipY = false;
    }

    public void KamePointFixer()
    {

        if (GokuSprite.flipY == false && KameFirePoint.localPosition.y != -0.22F)
        {
            KameFirePoint.localPosition = new Vector3(KameFirePoint.localPosition.x, -0.22F, 0);
        }
        else if (GokuSprite.flipY == true && KameFirePoint.localPosition.y != 0.22F)
        {
            KameFirePoint.localPosition = new Vector3(KameFirePoint.localPosition.x, 0.22F, 0);
        }

    }

    public void Aim()
    {
        Vector2 direction = ((Vector2)playerposition.position - (Vector2)transform.position).normalized;
        GokuSprite.gameObject.transform.right = direction;

        if (playerposition.position.x > transform.position.x)
        {
            GokuSprite.flipY = false;
        }
        else if (playerposition.position.x < transform.position.x)
        {
            GokuSprite.flipY = true;
        }
    }

    public void AimEnergyBalls()
    {
        Vector2 direction = ((Vector2)playerposition.position - (Vector2)transform.position).normalized;
        EnergyBallTransform.right = direction;
    }

    IEnumerator FireEnergyBalls()
    {
        for (int i = 0;i<5 ;i++) {
            AimEnergyBalls();
            Instantiate(EnergyBallprefab, EnergyBallFirepoint1.position, EnergyBallTransform.rotation);
            yield return new WaitForSeconds(0.5F);
            Instantiate(EnergyBallprefab, EnergyBallFirepoint2.position, EnergyBallTransform.rotation);
            yield return new WaitForSeconds(0.5F);
        }
    }

    

    public void FireKameHameHa()
    {
        Instantiate(Projectileprefab, KameFirePoint.position, KameFirePoint.rotation);
    }

    public void randomMovement()
    {


        Vector2 direction = new Vector2(-1, 0);
        Vector3 playerposition = FindAnyObjectByType<Player_Movement>().gameObject.transform.position;

        Vector3 Gokuposition = gameObject.transform.position;

        Vector3 aiming = playerposition - Gokuposition;

        int way = 0;
        if (aiming.x >= 0 && aiming.y >= 0)
        {
            way = 0;
        }
        else if (aiming.x >= 0 && aiming.y < 0)
        {
            way = 1;
        }
        else if (aiming.x < 0 && aiming.y < 0)
        {
            way = 2;
        }
        else if (aiming.x < 0 && aiming.y >= 0)
        {
            way = 3;
        }

        int random = Mathf.FloorToInt(Random.Range(0, 3));
        if (way == 0)
        {
            if (random == 0)
            {
                direction = new Vector2(0, 1);
            }
            else if (random == 1)
            {
                direction = new Vector2(1, 1);
            }
            else if (random == 2)
            {
                direction = new Vector2(1, 0);
            }

        }
        else if (way == 1)
        {
            if (random == 0)
            {
                direction = new Vector2(1, 0);
            }
            else if (random == 1)
            {
                direction = new Vector2(1, -1);
            }
            else if (random == 2)
            {
                direction = new Vector2(0, -1);
            }

        }
        else if (way == 2)
        {
            if (random == 0)
            {
                direction = new Vector2(0, -1);
            }
            else if (random == 1)
            {
                direction = new Vector2(-1, -1);
            }
            else if (random == 2)
            {
                direction = new Vector2(-1, 0);
            }

        }
        else if (way == 3)
        {
            if (random == 0)
            {
                direction = new Vector2(-1, 0);
            }
            else if (random == 1)
            {
                direction = new Vector2(-1, 1);
            }
            else if (random == 2)
            {
                direction = new Vector2(0, 1);
            }

        }
        //More directions
        //direction.Normalize();
        input = direction.normalized;


        Vector2 temp = new Vector2(rb.position.x + input.x, rb.position.y + input.y);

        //destination = temp;
        moving = true;
        movingCounter = 0;
    }

    public void MoveGoku()
    {
        if (moving)
        {
            input = input.normalized;
            rb.MovePosition(rb.position + input * movementSpeed * Time.fixedDeltaTime);
        }
    }
}
