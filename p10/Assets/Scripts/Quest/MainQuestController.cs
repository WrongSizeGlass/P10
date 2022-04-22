using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private FireSpiretWalkRoute Q1WalkRoute;
    [SerializeField] private FireSpiretWalkRoute Q2WalkRoute;
    [SerializeField] private BossBehavior BB;
    [SerializeField] private Quest1Controller q1;
    
    public bool lvl1Complete, lvl2Complete, lvl3Complete;
    
    [SerializeField] private GameObject lvl1Group, lvl2Group, lvl3Group;
    void Start(){
        lvl1Group.SetActive(false);
        level2Controller(false);
        level3Controller(false);
    }

    void level1Controller() {
        if (q1.getBoatsSinked()>1) {
            // activate trapped fire spirit
            lvl1Group.SetActive(true);
        }
    }
    void level2Controller(bool set) {
        lvl2Group.SetActive(set);
    }

    void level3Controller( bool set) {
        lvl3Group.SetActive(set);
    }

    // Update is called once per frame
    void Update(){
        lvl1Complete = getWalkRouteComplete(Q1WalkRoute);
        lvl2Complete = getWalkRouteComplete(Q2WalkRoute);
        lvl3Complete = getIsBossDead();
        if (!lvl1Complete) {
            Debug.LogError("## " + q1.getBoatsSinked());
            level1Controller();
        }
        if (lvl1Complete && !lvl2Complete) {
            level2Controller(true);
        }
        if (lvl2Complete) {
            level3Controller(true);
        }
        if (lvl3Complete) {
            Debug.LogError("Congrats you have completed the game");
        }
        // Lvl 1
            // destroid 2 ships
            // free the fire spiret
            // walk complete

        // Lvl 2 
            // turn on ice chuch, ice fire, ice spirit.
            // if both bonfires is on 
                // turn on trapped fire spirite
            // walk complete

        // turn on boss // control camera?
        // if boss dead game is complete




       
        
    }
    bool getWalkRouteComplete(FireSpiretWalkRoute lvl) {
         return lvl.getLastLightIsOn();
    }

    bool getIsBossDead() {
       return BB.getDead();
    }
}
