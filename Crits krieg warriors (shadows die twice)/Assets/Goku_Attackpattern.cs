using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goku_Attackpattern : MonoBehaviour
{

    public Transform playerposition;
    public bool Charge;
    public bool Fire;
    public bool BeamBefore;
    public bool Kame;
    public Animator GokuAnimator;
    public SpriteRenderer GokuSprite;
    public DungeonCam dungeonCam;

    public Animator KameAnimator;

    public Transform ParticlePivot;
    public GameObject ParticleForKame;

    public Unit Goku;
    public Slider GokuHealth;

    public Transform KameFirePoint;
    public GameObject Projectileprefab;

    // Start is called before the first frame update
    void Awake()
    {
        Goku = gameObject.GetComponent<Unit>();
        playerposition = FindObjectOfType<Player_Movement>().gameObject.transform;
    }

    private void Start()
    {
        StartCoroutine(AttackPattern());
    }

    IEnumerator AttackPattern()
    {
       // while(true){
            yield return new WaitForSeconds(3F);
            Charge = true;
            FindObjectOfType<AudioManager>().Play("Kamehame");
            ParticleForKame.SetActive(true);
        //
            yield return new WaitForSeconds(3.5F);
        ParticleForKame.SetActive(false);
        GokuAnimator.SetBool("End", false);
        KameAnimator.SetBool("End", false);
        Fire = true;
        KamePointFixer();
        BeamBefore = true;
        yield return new WaitForSeconds(0.5F);
        Kame = true;
       
        FireKameHameHa();
        //StartCoroutine(dungeonCam.Shake(1,0.25F));
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

        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Charge&&!Fire)
        {
            GokuSprite.flipX = false;
            Aim();

            if (GokuSprite.flipY==false&& ParticlePivot.localPosition.y!= -0.1395F)
            {
                ParticlePivot.localPosition = new Vector3(ParticlePivot.localPosition.x, -0.1395F,0);
            }else if (GokuSprite.flipY == true && ParticlePivot.localPosition.y != 0.1395F)
            {
                ParticlePivot.localPosition = new Vector3(ParticlePivot.localPosition.x, 0.1395F, 0);
            }
        }else if (Fire)
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
        GokuAnimator.SetBool("Charge",Charge);
        GokuAnimator.SetBool("Fire", Fire);

        KameAnimator.SetBool("BeamBefore", BeamBefore);
        KameAnimator.SetBool("Kame",Kame);

        GokuHealth.value = (Goku.cHP/Goku.maxHP);

        if (Goku.cHP<0)
        {
            Destroy(gameObject);
        }


    }

    public void ResetRotation()
    {
        Vector2 direction = new Vector2(1,0);
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

    public void FireKameHameHa()
    {
        Instantiate(Projectileprefab, KameFirePoint.position, KameFirePoint.rotation);
    }
}
