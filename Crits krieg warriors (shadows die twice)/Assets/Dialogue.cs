using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject parent;

    public string [] lines;
    public int index;
    public Text text;

    public bool Outoflines = false;

    public Animator Commander;

    public float textspeed;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = string.Empty;

    }

    public void StartDialogue()
    {
        text.text = string.Empty;
        index = 0;
        StartCoroutine(Talk());
    }

    IEnumerator Talk()
    {
        Commander.SetBool("Talking",true);
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textspeed);
        }
        Commander.SetBool("Talking", false);
    }

    void NextLine()
    {
        index++;
        if (index > lines.Length - 1)
        {
            Outoflines = true;
            return;
        }
        if (index == 7)
        {
            parent.SetActive(false);
            return;
        }
        text.text = string.Empty;
        StartCoroutine(Talk());

    }
    // Update is called once per frame
    void Update()
    {
        if (!Outoflines&&Input.GetMouseButtonDown(0))
        {
            if (text.text == lines[index])
            {
                
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                Commander.SetBool("Talking", false);
                text.text = lines[index];
            }
        }
    }
}
