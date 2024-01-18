using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCam : MonoBehaviour
{

    public Transform DungeonArea;

    public GameObject FollowModule;


    public void SetUp()
    {
        FollowModule.SetActive(false);
    }

    public void ExitDungeon()
    {
        FollowModule.SetActive(true);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0;
        while (elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y+originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
    

}
