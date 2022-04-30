using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    [SerializeField] private GameObject visualAid;
    [SerializeField] private AOEHIt aoeBool;
    public bool rotate = true;
    bool start = false;
    int counter = 0;
    float y = 2.5f;
    public float timer=1;
    public float speed = 10f;
    public float iceSpeed = 2.5f;
    public float ParticalDuriation = 5;
    public float AttactColdDownDuriation = 5;
    [SerializeField] private Transform target;
    [SerializeField] private Health h;
    private Transform player;
    private List<Transform> waypointList;
    private Transform me;
    public Transform waypointGroup;
    bool getTarget = false;
    private AreaEffectHItArea AEHA;
    public  bool setHit=false;
    private Vector3 myTarget;
    public bool haveReset = true;
    public MeshRenderer mr;
    Vector3 targetStartPos;

    public Logs L1;
    public Logs L2;
    public GameObject aoe;
    // Start is called before the first frame update
    void Start(){
        
        targetStartPos = target.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        AEHA = target.GetChild(0).GetComponent<AreaEffectHItArea>();
      
    }

   
   
    int hitCounter = 0;
    bool AttactPlayer;
    bool once = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        TurnOnOffBoonFires();
        Vector3 temp =  player.position;
        temp.y = temp.y + 0.5f;
        
        if (!getFireQuestComplete() && start) {
            if (l1Off && !once|| !once && l2Off) {
                AttactColdDownDuriation = AttactColdDownDuriation / 2;
               // speed = 2*speed;
                once = true;
            }

            AttactPlayer = AEHA.getHit();
            counter++;
            getTarget = Vector3.Distance(temp, transform.position) > 5;
            // rotate

            if (!resetAttact) {

                if (getTarget && !AttactPlayer && !setHit) {
                    if (!mr.enabled) {
                        mr.enabled = true;
                    }
                    target.position = Vector3.MoveTowards(target.position, temp, speed * Time.deltaTime);
                }


            }
            if (AttactPlayer && !resetAttact) {
                /**/
                attack();
            }
            if (resetAttact) {
                target.position = transform.position;
                setHit = false;
                resetAttact = false;
                aoe.SetActive(false);
                AEHA.setHit(false);
                onceDmg = false;
                h.setDamageEffect(false);
                aoeBool.setIsHitting(false);
            }
        }
        //Debug.LogError("%% bonfire on " +getFireQuestComplete());
       
    }
    bool l1Off = false;
    bool l2Off = false;
    void TurnOnOffBoonFires() {
        // set target position
        if (L1.getFireActive() ){
            
            l1Off = true;
        }
        if (L2.getFireActive() ){
           
            l2Off = true;
        }

    }
   public bool getFireQuestComplete() {
        return l1Off && l2Off;
    }
    void attack() {
        Debug.Log("AEHA.getHit() " + AEHA.getHit());
        // get temp position
        if (AEHA.getHit()&& !setHit)
        {
           
            myTarget = player.position;
           
            if (mr.enabled)
            {
                mr.enabled = false;
            }
            //box.enabled = false;
            target.position =  targetStartPos;
            setHit = true;
            //
        }
        // Send objects towards players
        if (setHit) {
           
            canDamageAnimationStart();
            aoe.SetActive(true);
            aoe.transform.position = myTarget;
            cannotDamageAnimationEnd();
        }
    }
    bool canDamage = false;
    bool resetAttact = false;

    void canDamageAnimationStart(){
        if (!getCanDamage() && counter % Mathf.Round(ParticalDuriation / Time.fixedDeltaTime) == 0){
            setCanDamage(true);
            counter = 0;
        }
    }
    void cannotDamageAnimationEnd()
    {
        if (getCanDamage() && counter % Mathf.Round((AttactColdDownDuriation) / Time.fixedDeltaTime) == 0){
            setCanDamage(false);
            
            counter = 0;
            if (aoeBool.getIsHitting()) {
                //setDealDmg(true);
                //h.setHp(1);
                h.playerTakeDamage();

                h.setQuest(this.tag);
                onceDmg = true;
                Debug.Log("¤¤ Hitting player dmgEffect:" + h.getDamageEffect());
                
            }
            resetAttact = true;
        }
    }
    bool onceDmg = false;
    bool dealDmg = false;
    public bool getDealDmg() {
        return dealDmg;
    }
    public void setDealDmg(bool set) {
        dealDmg = set;
    }

    private int getHitCounter() {
        return hitCounter;
    }
    private void setHitCounter(int set) {
        hitCounter = set;
    }

    public bool getCanDamage(){
        return canDamage;
    }
    public void setCanDamage(bool set){
        canDamage = set;
    }
    public void setStart(bool set) {
        start = set;
    }

}
