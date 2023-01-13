using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> _sprites;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private GameObject _statue;
    void Start()
    {
        
    }
    public void ReFlash()
    {
        StartCoroutine(CrFlash());
    }

    IEnumerator CrFlash()
    {
        Color green = _sprites[0].color;

        for (float i = 0; i < 0.25f; i += Time.deltaTime)
        {
            yield return null;
            for (int j = 0; j < _sprites.Count; j++)
            {
                Color c = green;
                c.a = _curve.Evaluate(i/0.25f);
                _sprites[j].color = c;
            }
        }

        if (Vector3.Distance(transform.position, _statue.transform.position) < 1.5f)
        {
            _statue.GetComponent<Statue>().Flashazo();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReFlash();

        }
    }
}
