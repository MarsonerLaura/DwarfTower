using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cameras;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AktivateOnly(0);
        }else if(Input.GetKeyDown(KeyCode.Alpha2))
        {

            AktivateOnly(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            AktivateOnly(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AktivateOnly(3);

        }
    }

    private void AktivateOnly(int number)
    {
        for(int i=0; i<cameras.Count; i++)
        {
            if (i == number)
            {
                cameras[i].SetActive(true);
            }
            else
            {
                cameras[i].SetActive(false);
            }
        }
    }


}
