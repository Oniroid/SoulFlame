using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableRock : MonoBehaviour
{
    public void Move(Vector3 direction)
    {
        StartCoroutine(CrMove(direction));

        if (AlphaController.Alpha < 80)
        {
        }
    }

    IEnumerator CrMove(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition +direction;
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, i/0.5f);
            yield return null;
        }
        transform.position = endPosition;
    }
}
