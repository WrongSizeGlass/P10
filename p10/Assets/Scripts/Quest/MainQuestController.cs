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
    [SerializeField] private GameObject player;
    string attacker = "";
    public bool lvl1Complete, lvl2Complete, lvl3Complete;
    
    [SerializeField] private GameObject lvl1Group, lvl2Group, lvl3Group;
    [SerializeField] private bool playerIsDead = false;

    [SerializeField] private Transform waterRespawnGroup;
    private List<Transform> waterRespawnList;
    Vector3 closesPos;
    Vector3 startClosesPos;
    bool playerHitWater = false;
    [SerializeField] GameObject bc;
    //[SerializeField] CameraTest ct;
    void Start(){
        startClosesPos = new Vector3(999,999,999);
        closesPos = startClosesPos;
        lvl1Group.SetActive(false);
        level2Controller(false);
        level3Controller(false, false);
       // level3Controller(true, true);
        waterRespawnList = new List<Transform>();
        for (int i=0; i<waterRespawnGroup.childCount; i++) {
            waterRespawnList.Add(waterRespawnGroup.GetChild(i).GetComponent<Transform>());
        
        }
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
    bool bossOnce = false;
    void level3Controller( bool set, bool test) {
        if (!bossOnce && test) {
            bc.SetActive(true);
            bossOnce = true;
        }
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
            Debug.LogError("¤¤ test?" );
            level3Controller(true, true);
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


        if (playerHitWater) {
            Debug.LogError("PLAYER NEW POS ### ");
            playerHitsWater();
        }
        if (!once) {
            
            once = true;
        }
        /*if (bossOnce && !bc.enabled) {
            ct.enabled = true;
        }*/
    }
    bool once = false;
    public void playerHitsWater() {
        for (int i =0; i<waterRespawnList.Count; i++) {
            if (Vector3.Distance(waterRespawnList[i].position, player.transform.position)< Vector3.Distance(player.transform.position, closesPos)) {
                closesPos = waterRespawnList[i].position;
                Debug.LogError("### " + waterRespawnList[i].name);
            }
        
        }
        player.transform.position = closesPos;
        closesPos = startClosesPos;
        playerHitWater = false;
    }

    bool getWalkRouteComplete(FireSpiretWalkRoute lvl) {
         return lvl.getLastLightIsOn();
    }

    bool getIsBossDead() {
       return BB.getDead();
    }

    public void setPlayerIsDead(bool set) {
        playerIsDead = set;
    }
    public bool getPlayerIsDead() {
        return playerIsDead;
    }
    public void setAttacker(string set) {
        attacker = set;
    }
    public void setPlayerHitsWater(bool set) {
        playerHitWater = set;
    }
}
