using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description_Template : MonoBehaviour
{

    public GameObject itemimage_holder;
    public GameObject description_holder;
    public GameObject Name_of_item_holder;
    Image itemimage;
    Text description;
    Text Name_of_item;

    // Start is called before the first frame update

    public void Start()
    {
        itemimage = itemimage_holder.GetComponent<Image>();
        description = description_holder.GetComponent<Text>();
        Name_of_item = Name_of_item_holder.GetComponent<Text>();

    }

    public void ChangeUIInfo(Sprite a,string b,string c)
    {

        itemimage.sprite = a;
        Name_of_item.text = b;
        description.text = c;

    }
  
}
