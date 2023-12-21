using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmiteEffect : MonoBehaviour
{
    public SpriteRenderer effect;
    public Sprite effect1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(effectanddestroy());
    }

    IEnumerator effectanddestroy()
    {

        yield return new WaitForSeconds(0.1F);
        effect.sprite = effect1;
        yield return new WaitForSeconds(0.2F);
        Destroy(gameObject);

    }
}
