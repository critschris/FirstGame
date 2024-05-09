using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public float followspeed;
    public Transform follow;
    public Transform Camera;



    // Update is called once per frame
    void Update()
    {

        Vector3 newPOS = new Vector3(follow.position.x, follow.position.y, Camera.position.z);
        Camera.position = Vector3.Slerp(transform.position,newPOS,followspeed*Time.deltaTime);

    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        

        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            Vector3 originalPos = transform.position;
            transform.position = new Vector3(x + originalPos.x, y + originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        //transform.position = originalPos;
    }
}
