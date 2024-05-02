using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfSwords : MonoBehaviour
{
    public Swords[] swords;

    public Swords translateindexintosword(string a)
    {
        for (int i = 0; i <swords.Length ;i++)
        {
            if (a == swords[i].getName())
            {
                return swords[i];
            }
        }

        return swords[1];
    }
}
