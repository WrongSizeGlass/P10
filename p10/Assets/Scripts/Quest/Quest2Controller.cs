using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2Controller : MonoBehaviour
{

    public GameObject frozenChurch;
    public GameObject normalChurch;
    public GameObject iceSpirite;
    public GameObject fireSpirite;
    private FireSpiretWalkRoute fswr;
    public RotateObjects rb;
    bool fireQuest = false;
    bool iceQuest = false;
    bool quest2Complete=false;
    public RotateObjects ro;
    // Start is called before the first frame update
    void Start()
    {
        fswr = fireSpirite.GetComponent<FireSpiretWalkRoute>();
    }

    // Update is called once per frame
    void Update()
    {

        // church is complete if
        
        iceQuest = ro.getFireQuestComplete();
        // spawn normal church
        if (iceQuest) {
            frozenChurch.SetActive(false);
            normalChurch.SetActive(true);
            iceSpirite.SetActive(false);
            fireSpirite.SetActive(true);
        }
        // walkaround
        quest2Complete= fswr.getFinishedRoute();
        if (iceQuest) {
            Debug.LogError("ice quest"+ iceQuest);
        }
        if (quest2Complete) {
            Debug.LogError("Quest 2 is complete" + quest2Complete);
        }

    }
    public bool getChurchComplete() {
        return iceQuest;
    }
    public bool getQuest2Complete() {
        return quest2Complete;
    }
   
}
