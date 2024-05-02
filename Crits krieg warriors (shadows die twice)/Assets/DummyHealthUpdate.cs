using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyHealthUpdate : MonoBehaviour
{
    public Slider healthbar;
    Unit dummyunit;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GetComponentInChildren<Slider>();
        dummyunit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.value = dummyunit.cHP/dummyunit.maxHP;
        if (dummyunit.cHP<=0)
        {
            Destroy(gameObject);
        }
    }
}
