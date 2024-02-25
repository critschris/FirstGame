using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GokuProjectile : MonoBehaviour
{

    public float flyingspeed = 20F;
    public float Destroytime = 5F;
    float timer = 0;
    public float damage;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * flyingspeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > Destroytime)
        {
            Destroy(gameObject);
        }
        else
        {
            timer += Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unithit = collision.GetComponent<Unit>();
        if (unithit != null)
        {
            unithit.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
