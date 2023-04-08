using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AktivateOnButtonPress : MonoBehaviour
{
    [SerializeField]
    private GameObject aktivate; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            aktivate.SetActive(!aktivate.activeInHierarchy);
        }
    }
}
