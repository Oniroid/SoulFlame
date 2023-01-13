using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour0 : MonoBehaviour
{
    
    IEnumerator Start()
    {
        for(int j =0; j< 2; j++)
        {
            yield return new WaitForSeconds(1f);
            Vector3 startPosition = transform.position;
            Vector3 endPosition = transform.position + Vector3.down;
            for (float i = 0; i < 1f; i += Time.deltaTime)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, i);
                yield return null;
            }
        }
    }

    void Update()
    {
            
    }
}
