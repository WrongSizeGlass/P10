using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    public GameObject interactButton;

    private void Awake()
    {
        interactButton.SetActive(false);
    }
    void Update()
    {
        void OnTriggerEnter(Collider other)
        {
            if(other.tag == "PressE")
            {
                interactButton.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E) == true)
                {
                    interactButton.SetActive(false);
                    //GetComponent<Collider>().enabled = false;
                }
            }
        }

        void onTriggerExit(Collider collider)
        {
            interactButton.SetActive(false);
        }
    }
}
