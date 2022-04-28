using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureTrigger : MonoBehaviour
{
    public string me;
    public int myNr;
    [SerializeField] private FireSpiretWalkRoute spirit;
    [SerializeField] private NatureTrigger nature;
    [SerializeField] private GameObject natureEffect;
    [SerializeField] private AudioSource sound;
    bool hasPressed = false;
    BoxCollider col;
    // Start is called before the first frame update
    void Start()
    {
   
        col = GetComponent<BoxCollider>();
        col.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPressed && !sound.isPlaying) {
            if (myNr == 2 && !sound.isPlaying)
            {
                spirit.setActivatedInn(true);
                gameObject.SetActive(false);
            }
            if (myNr == 5 && !sound.isPlaying)
            {
                spirit.setCorrectStreet(true);
                gameObject.SetActive(false);
            }
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag =="Player" && Input.GetKeyDown(KeyCode.E)) {
            if (nature !=null) {
                nature.enabled = true;
            }
            hasPressed = true;
            
            natureEffect.SetActive(true);
        }
    }

}
