using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private bool _vertical;
    float _shootCd;
    void Start()
    {
        _shootCd = 0f;
    }

    void Update()
    {
        if (_shootCd > -1)
        {
            _shootCd -= Time.deltaTime;
        }
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + transform.right * 20, _mask);
        if (_vertical)
        {
            hit = Physics2D.Linecast(transform.position, transform.position + Vector3.down * 20, _mask);
        }
        

        
        if (hit)
        {
            
            if (_shootCd <= 0)
            {
                
                GameObject g = Instantiate(_arrow, transform.position, Quaternion.identity);
                if (g.GetComponent<HorizontalArrow>() != null)
                {
                    g.GetComponent<HorizontalArrow>()._target = hit.transform.position;
                }
                else
                {
                    g.GetComponent<VerticalArrow>()._target = hit.transform.position;

                }
                if (hit.collider.gameObject.GetComponent<CharacterMovement>() != null)
                {
                    CharacterMovement charac = FindObjectOfType<CharacterMovement>();
                    charac.DieArrowed();
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                if (hit.collider.gameObject.GetComponent<StandardSlimeBehaviour>()!=null)
                {
                    if (_vertical)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<StandardSlimeBehaviour>().Die();
                    }
                }
                _shootCd = 2f;
            }
        }
    }
}
