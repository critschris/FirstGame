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

    public bool selfclick;

    public int[] indexesthatneedstopping;
    public int counter;
    public int stoppingathisint;

    public int[] indexforopengate;
    public int currentgate;

    public bool Outoflines = false;

    public Animator Commander;

    public float textspeed;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        text.text = string.Empty;
        //counter = 0;

    }

    public void StartDialogue(int a)
    {
        text.text = string.Empty;
        index = a;
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

    public void NextLine()
    {
        index++;
        if (index > lines.Length - 1)
        {
            Outoflines = true;
            parent.SetActive(false);
            return;
        }
        //Stopping on that line of text
        if (counter< indexesthatneedstopping.Length && index == indexesthatneedstopping[counter])
        {
            stoppingathisint = counter;
            counter++;
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
