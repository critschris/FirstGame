using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonaProjectile : MonoBehaviour
{
    public float rotationspeed = 480F;
    public float flyingspeed = 20F;
    public float Destroytime = 5F;
    float timer = 0;
    public float damage;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right*flyingspeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward*rotationspeed*Time.fixedDeltaTime);
        if (timer>Destroytime)
        {
            Destroy(gameObject);
        }
        else
        {
            timer +=Time.fixedDeltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unithit = collision.gameObject.GetComponent<Unit>();
        
        if (unithit!=null)
        {
            unithit.takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
