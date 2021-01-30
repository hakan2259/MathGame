using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingLifeManager : MonoBehaviour
{


    [SerializeField]
    private GameObject health1, health2, health3;

   
   

    
    

    public void remainingLifeControl(int remaingHealth)
    {

        switch (remaingHealth)
        {
            case 3:
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(true);
                break;
            case 2:
                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(false);
                break;
            case 1:
                health1.SetActive(true);
                health2.SetActive(false);
                health3.SetActive(false);
                break;
            case 0:
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(false);
                break;
        }


    }
}
