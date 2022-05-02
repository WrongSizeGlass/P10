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
    [SerializeField] private HammerDialogScript hds;
    bool hasPressed = false;
    SphereCollider col;
    bool canTalk;
    // Start is called before the first frame update
    void Start()
    {
   
        col = GetComponent<SphereCollider>();
        col.enabled = true;
        canTalk = myNr == 1 || myNr == 3 || myNr == 4;
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
            if (canTalk) {
             //   Debug.LogError("myNumber = " + myNr);
                hds.setNatureExternalList(myNr, true);
            }
        }
    }

}
