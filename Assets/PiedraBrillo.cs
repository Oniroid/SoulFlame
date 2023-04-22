using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiedraBrillo : MonoBehaviour
{
    [SerializeField] private  int piedraBrilloIndex;
    [SerializeField] private Sprite[] altSpriteBajo0;

    private void Update()
    {

        if (Vector3.Distance(transform.position, new Vector3(-1f, 1f, 0)) < 0.1f)
        {
            transform.position = new Vector3(-1, 0.8f);
        }

        if (Vector3.Distance(transform.position, new Vector3(-1f, 0f, 0)) < 0.1f)
        {
            transform.position = new Vector3(-1,-0.2f);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            //GetComponent<SpriteRenderer>().sprite = altSpriteBajo0[piedraBrilloIndex];
        }

        if (Vector3.Distance(transform.position, new Vector3(-2f, 1f, 0)) < 0.1f)
        {
            transform.position = new Vector3(-2, 0.8f);
        }

        if (Vector3.Distance(transform.position, new Vector3(-2f, 0f, 0)) < 0.1f)
        {
            transform.position = new Vector3(-2, -0.2f);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
            //GetComponent<SpriteRenderer>().sprite = altSpriteBajo0[piedraBrilloIndex];

        }
    }

}