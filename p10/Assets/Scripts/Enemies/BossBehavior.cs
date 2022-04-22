using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // for introduction
    [Header("Introduction variables")]
    [SerializeField] private Transform leftTargetGroup;
    [SerializeField] private Transform rightTargetGroup;
    private List<Transform> leftTargets;
    private List<Transform> rightTargets;
    [SerializeField] private int sunkedCounter = 0;
    private int stage = 0;
    private bool indexCanIncrement = false;
    private bool stopIntroMovement = false;
    [SerializeField] private float introMovement = 1.5f;
    [SerializeField] private List<Transform> introWaypointList;
    private bool bossIntroEnd = false;

    // General vars
    [Header("General variables")]
    [SerializeField] private Transform lw;
    [SerializeField] private Transform rw;
    [SerializeField] private GameObject leftFireOff;
    [SerializeField] private GameObject rightFireOff;
    [SerializeField] private MeshRenderer leftVisualMarker;
    [SerializeField] private MeshRenderer rightVisualMarker;
    [SerializeField] private int movementIndex = 0;
    private string leftWeapon = "LeftWeapon";
    private string rightWeapon = "RightWeapon";
    private Vector3 targetDirection;
    private Vector3 newDirection;
    private Vector3 temp;
    private VisualAOERotateAround leftVaoe;
    private VisualAOERotateAround rightVaoe;
    private BossAOEHit boosHitL;
    private BossAOEHit boosHitR;
    private int counter = 0;
    private int counter2 = 0;
    private bool leftAttack = true;
    private bool rightAttack = true;
    bool hit = false;
    bool once = false;
    private int hp = 3;
   
    private float distToActivate = 99;
    bool dead = false;

    //Attack Settings vars
    [Header("Attack settings ")]
    [SerializeField] private float ParticalDuriation = 5f;
    [SerializeField] private float AttactDuriation = 2.5f;
    [SerializeField] private float switchWeaponTime = 1.5f;
    [SerializeField] private float weaponSpeed = 1.75f;
    private List<BossAOEHit> selectedWeapon;
    private bool resetAttact = false;
    private bool canAttack = false;
    private bool hasAttacked = false;
    private int currentWeapon = 0;
    private int weaponIndex;
    private int hpChange = 3;

    [Header("Boss Combat variables")]
    [SerializeField] private Transform movementPatternGroup;
    private List<Transform> movementPatternList;
    private bool startCombat = false;
    [SerializeField] private Transform player;
    [SerializeField] private Transform deadPos;
    // comming back to it
    [SerializeField] private ArenaTrigger bossArena;
    [SerializeField] private float movementSpeed = 3.5f;
    // only changes if  index < 0 || index > count
    [SerializeField] private bool changeDirection = false;
    // should change if ice is destroid
    [SerializeField] private bool hasChangeMovementIndex = false;
    private bool hasTargetPos = false;
    private bool endGame = false;

    private bool changeMovementIndex = false;

    // Start is called before the first frame update
    void Start() {

        movementPatternList = new List<Transform>();
        leftTargets = new List<Transform>();
        rightTargets = new List<Transform>();
        boosHitL = lw.GetComponent<BossAOEHit>();
        boosHitR = rw.GetComponent<BossAOEHit>();
        boosHitL.setBossBehavior(this.transform);
        boosHitR.setBossBehavior(this.transform);
        for (int i = 0; i < leftTargetGroup.childCount; i++) {
            leftTargets.Add(leftTargetGroup.GetChild(i).GetComponent<Transform>());
            rightTargets.Add(rightTargetGroup.GetChild(i).GetComponent<Transform>());
        }
        for (int i = 0; i < movementPatternGroup.childCount; i++) {
            movementPatternList.Add(movementPatternGroup.GetChild(i).GetComponent<Transform>());
        }
        selectedWeapon = new List<BossAOEHit>();
        selectedWeapon.Add(boosHitL);
        selectedWeapon.Add(boosHitR);
        boosHitL.enabled = true;
        boosHitR.enabled = true;
        setNewWeapon("");
        leftVaoe = leftVisualMarker.GetComponent<VisualAOERotateAround>();
        rightVaoe = rightVisualMarker.GetComponent<VisualAOERotateAround>();
        leftVisualMarker.enabled = false;
        rightVisualMarker.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        counter++;
        counter2++;
        currentWeapon = currentWeapon > 1 ? 0 : currentWeapon;

        // bossIntro
        if (!bossIntroEnd) {
            bossIntro();

            // bossBattle
        } else if (bossIntroEnd && !dead) {
            bossMovement();
            if (bossArena.getBossCanAttack()) {
                bossAttackPlayer();
            }
        }
        if (bossArena.getBossCanAttack() && !bossIntroEnd) {
            bossIntroEnd = true;
        }
        if (getHp() <= 0) {
            dead = true;
            setTransform(deadPos.position, 1);
            if (Vector3.Distance(transform.position, deadPos.position) < 1.75)
            {
                endGame = true;
            }
            if (Vector3.Distance(transform.position, deadPos.position)< 1.55) {
               
                transform.position = deadPos.position;
                Destroy(this.gameObject);
            }
        }
    }

    void bossAttackPlayer() {
        // if player is inside areana

        // select l or r with the shortes distance to the player between each shoot

        // shoot
        


        if (!hasTargetPos) {
            
        }
        if (!hasAttacked) {
         
            if (distToActivate < 0.1f){
                // start explosion animation
                hit = true;
               
            } 

            if (hit){
                canDamageAnimationStart(selectWeapon());
                once = true;
            }else {

                temp = player.position;
                moveToTarget(selectWeapon(), false, player);
            }
            
        }

        // canDamageAnimationStart(selectWeapon());
        if (hasAttacked) {
            cannotDamageAnimationEnd(selectWeapon(), false);
        }

    }
    // selects the weapon with the shortest dist to the player
    BossAOEHit selectWeapon() {
        return Vector3.Distance(lw.position, player.position) < Vector3.Distance(rw.position, player.position) ? boosHitL : boosHitR;
    }

    void bossMovement() {
        // set start index
        if (!startCombat) {
            movementIndex = 0;
            startCombat = true;
        }
        if (Vector3.Distance(transform.position, movementPatternList[movementIndex].position) < 0.1f) {
            setMovementIndexDirection();
        }
        if (getHp() != hpChange) {
            hpChange--;
            hasChangeMovementIndex = true;
            
        }
        if (hasChangeMovementIndex) {          
            setChangeDir(!getChangeDir());
            setMovementIndexDirection();
        }
        setTransform(movementPatternList[movementIndex].position, movementSpeed);
    }

    // set this.transform's move-towards and rot-towards
    void setTransform(Vector3 target, float speed) {

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        targetDirection = target - transform.position;
        newDirection = Vector3.RotateTowards(transform.forward, targetDirection, (speed / 2) * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    // in(de)crement movement index for movement pattern doing combat
    void setMovementIndexDirection() {
        if (getChangeDir())
        {
            movementIndex--;
        } else {
            movementIndex++;
        }

        if (movementIndex > movementPatternList.Count - 1) {
            movementIndex = 0;
        } else if (movementIndex < 0) {
            movementIndex = movementPatternList.Count - 1;
        }
        hasChangeMovementIndex = false;
    }

    void bossIntro() {
        // move boss
        if (!stopIntroMovement) {
            bossIntroMovement();
        }
        // only fire weapons when ship has reached the second waypoint, set limits for targets in the harbor
        if (sunkedCounter < 3 && movementIndex > 1) {
            setWeaponsForBossIntro();
        }

        if (stopIntroMovement && sunkedCounter>2) {

            bossIntroEnd = true;
        }
        
    }
    void bossIntroMovement() {

        if (Vector3.Distance(transform.position, introWaypointList[movementIndex].position) < 0.2f) {
            movementIndex++;
            introMovement = 2f;
        }
        if (movementIndex > introWaypointList.Count - 1) {
            stopIntroMovement = true;
        }
        setTransform(introWaypointList[movementIndex].position, introMovement);
    }

    void setWeaponsForBossIntro() {
        if (stage % 2 == 0) {
            // used left weapons
            BossIntroductionAttack(boosHitL);
        } else if (stage % 2 == 1) {
            // use right weapons
            BossIntroductionAttack(boosHitR);
            // only increment target index after both left and right weapons has fired, and only hit 3 targets pr side
            if (sunkedCounter < leftTargets.Count) {
                indexCanIncrement = true;
            }
        }
        // set new target, reset counter
        if (stage % 2 == 0 && indexCanIncrement) {
            sunkedCounter++;
            counter = 0;
            indexCanIncrement = false;
        }
    }
    void BossIntroductionAttack(BossAOEHit LorR) {
        if (!hasAttacked) {
            // get+move to target
            introMoveToTarget(LorR);
            if (distToActivate < 0.1f) {
                // start explosion animation
                canDamageAnimationStart(LorR);
            }
        }
        if (hasAttacked) {
            // start damage/end part of explosion and reset
            cannotDamageAnimationEnd(LorR);
        }
    }
    // move towards target depended on which side it is
    void moveToTarget(BossAOEHit LorR, bool intro, Transform target)
    {
        hasTargetPos = true;
        
        if (LorR.getName() == leftWeapon && !intro) {
            lw.SetParent(null);
            lw.position = Vector3.MoveTowards(lw.position, temp, weaponSpeed * Time.deltaTime);
            distToActivate = Vector3.Distance(temp, lw.position);
            boosHitR.resetPos();
            setGUI(true, leftFireOff, leftVisualMarker, leftVaoe, target);

        } else if (LorR.getName() == rightWeapon && !intro) {
            rw.SetParent(null);
            rw.position = Vector3.MoveTowards(rw.position, temp, weaponSpeed * Time.deltaTime);
            distToActivate = Vector3.Distance(temp, rw.position);
            boosHitL.resetPos();
            setGUI(true, rightFireOff, rightVisualMarker, rightVaoe, target);
        }
    }
       void introMoveToTarget(BossAOEHit LorR) {

           if (stage % 2 == 0){
               lw.SetParent(null);
               lw.position = Vector3.MoveTowards(lw.position, leftTargets[sunkedCounter].position, weaponSpeed * Time.deltaTime);
               temp = leftTargets[sunkedCounter].position;
               distToActivate = Vector3.Distance(temp, lw.position);
               boosHitR.resetPos();
               setGUI(true, leftFireOff, leftVisualMarker, leftVaoe, leftTargets[sunkedCounter]);

           }else if (stage % 2 == 1){
               rw.SetParent(null);
               rw.position = Vector3.MoveTowards(rw.position, rightTargets[sunkedCounter].position, weaponSpeed * Time.deltaTime);
               temp = rightTargets[sunkedCounter].position;
               distToActivate = Vector3.Distance(temp, rw.position);
               boosHitL.resetPos();
               setGUI(true, rightFireOff, rightVisualMarker, rightVaoe, rightTargets[sunkedCounter]);
           }
       }
    // start animation turn off GUI
    void canDamageAnimationStart(BossAOEHit LorR) {       
       // if (LorR.getCanAttack() && counter % Mathf.Round(ParticalDuriation / Time.fixedDeltaTime) == 0){
            LorR.setCanAttack(false);
           
            hasAttacked = true;
            if (LorR.getName()==leftWeapon)
            {
                Debug.LogError("turn off left");
                setGUI(false, leftFireOff, leftVisualMarker) ;
            }else if(LorR.getName() == rightWeapon)
            {
                setGUI(false, rightFireOff, rightVisualMarker);
                Debug.LogError("turn off right");
            }
        counter2 = 0;
      // }
    }
    //sink parts of the harbor, reset weapon position
    void cannotDamageAnimationEnd(BossAOEHit LorR, bool intro= true){
        LorR.setBeamActive(true);
        if (counter % Mathf.Round((AttactDuriation-1.1f) / Time.fixedDeltaTime) == 0 && intro){

            LorR.setSinkTheHarbor(true);
            Debug.LogError("sink harbour");
        }
        
        if ( counter2 % Mathf.Round((AttactDuriation) / Time.fixedDeltaTime) == 0 && counter2!=0){
           // LorR.transform.SetParent(LorR.getMyParent());
            
            LorR.Resetposition();
            //setNewWeapon(LorR.transform.name);
            if (intro) {
                stage++;
            }
            
            //stage++;
            counter = 0;
            distToActivate = 99;
            LorR.setCanAttack(true);
            hasTargetPos = false;
            hasAttacked = false;
            hit = false;
            once = false;
            Debug.LogError("RESET!! " + LorR.name);
        }
    }
    // set GUI of the weapon pos
    void setGUI(bool on, GameObject fireOff = null, MeshRenderer mr = null, VisualAOERotateAround rotateGUI = null, Transform rotateTarget = null){
        fireOff.SetActive(on);
        mr.enabled = on;
        if (rotateGUI != null){
            rotateGUI.setTarget(rotateTarget.position);
        }
    }
    // select weapon left or right
    private void setNewWeapon(string weapon) {
        if (weapon == "LeftWeapon"){
            weaponIndex = 1;
        }else{
            weaponIndex =0;
        }
    }
    public bool getChangeDir() {
        return changeDirection;
    }
    public void setChangeDir( bool set) {
        changeDirection = set;
    }
    private bool getChangeMovementIndex() {
        return changeMovementIndex;
    }
    private void setChangeMovementIndex(bool set) {
        changeMovementIndex = set;
    }
    public void degrementHP() {
        hp--;
    }
    public int getHp() {
        return hp;
    }
    public bool getDead() {
        return endGame;
    }
}
