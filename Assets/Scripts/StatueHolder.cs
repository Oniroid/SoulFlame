using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHolder : MonoBehaviour
{
    [SerializeField] private GameObject _statue;
    bool _open;
    [SerializeField] private GameObject _closedDoors;
    [SerializeField] private GameObject _openDoors;

    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(_statue.transform.position, transform.position) < 0.3f)
        {
            if(GetComponent<SpriteRenderer>().color.a > 0.9f && _statue.GetComponent<SpriteRenderer>().color.a > 0.9f)
            {
                if (!_open)
                {
                    _open = true;
                    _closedDoors.GetComponent<Animator>().SetTrigger("OpenDoor");
                    
                    //Dialogo Dragon y fin de juego
                }
            }

        }
    }
}
