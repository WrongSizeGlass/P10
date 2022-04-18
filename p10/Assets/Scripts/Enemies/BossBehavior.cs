using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // for introduction
    [Header("Introduction variables")]
    [SerializeField] private Transform leftTargetGroup;
    [SerializeField] private Transform rightTargetGroup;
    [SerializeField] private GameObject leftFireOff;
    [SerializeField] private GameObject rightFireOff;
    private List<Transform> leftTargets;
    private List<Transform> rightTargets;
    [SerializeField]private int sunkedCounter = 0;
    private int stage = 0;
    private bool indexCanIncrement = false;
    private bool stopIntroMovement = false;
    [SerializeField] private int movementIndex = 0;
    [SerializeField] private float introMovement = 1.5f;
    [SerializeField] private List<Transform> introWaypointList;

    // General vars
    [Header("General variables")]
    [SerializeField] private Transform lw;
    [SerializeField] private Transform rw;
    [SerializeField] private MeshRenderer leftVisualMarker;
    [SerializeField] private MeshRenderer rightVisualMarker;
    private VisualAOERotateAround leftVaoe;
    private VisualAOERotateAround rightVaoe;
    private BossAOEHit boosHitL;
    private BossAOEHit boosHitR;
    private int counter = 0;
    private bool leftAttack = true;
    private bool rightAttack = true;
    private Vector3 temp;
    private float distToActivate=1;

    //Attack Settings vars
    [Header("Attack settings ")]
    [SerializeField] private float ParticalDuriation = 5f;
    [SerializeField] private float AttactColdDownDuriation = 2.5f;
    [SerializeField] private float switchWeaponTime = 1.5f;
    [SerializeField] private float weaponSpeed = 1.75f;
    private List<BossAOEHit> selectedWeapon;
    private bool resetAttact =false;
    private bool canAttack = false;
    private bool hasAttacked = false;
    private int currentWeapon = 0;
    private int weaponIndex;


    // Start is called before the first frame update
    void Start(){

        leftTargets = new List<Transform>();
        rightTargets = new List<Transform>();
        boosHitL = lw.GetComponent<BossAOEHit>();
        boosHitR = rw.GetComponent<BossAOEHit>();
        boosHitL.setBossBehavior(this.transform);
        boosHitR.setBossBehavior(this.transform);
        for (int i=0; i<leftTargetGroup.childCount; i++) {
            leftTargets.Add(leftTargetGroup.GetChild(i).GetComponent<Transform>());
            rightTargets.Add(rightTargetGroup.GetChild(i).GetComponent<Transform>());
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
    void FixedUpdate(){
        counter++;
        currentWeapon = currentWeapon>1 ? 0 : currentWeapon;

        bossIntro();

    }
    void bossIntro() {
    // move boss
        if (!stopIntroMovement) {
            bossIntroMovement();
        }
        // only fire weapons when ship has reached the second waypoint, set limits for targets in the harbor
        if (sunkedCounter < 3 && movementIndex > 1){
            setWeaponsForBossIntro();
        }
    }
    void bossIntroMovement() {
        
        if (Vector3.Distance(transform.position, introWaypointList[movementIndex].position)<0.2f) {
            movementIndex++;
            introMovement = 2f;
        }
        if (movementIndex > introWaypointList.Count -1){
            stopIntroMovement = true;
        }
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, introWaypointList[movementIndex].position, introMovement * Time.deltaTime);
        
    }

    void setWeaponsForBossIntro() {
        if (stage % 2 == 0) {
            // used left weapons
            BossIntroductionAttack(boosHitL);
        } else if (stage % 2 == 1) {
            // use right weapons
            BossIntroductionAttack(boosHitR);
            // only increment target index after both left and right weapons has fired, and only hit 3 targets pr side
            if (sunkedCounter<leftTargets.Count) {
                indexCanIncrement = true;

            }
        }
        // set new target, reset counter
        if (stage%2==0 && indexCanIncrement) {
            sunkedCounter++;
            counter = 0;
            indexCanIncrement = false;
        }
    }
    void BossIntroductionAttack(BossAOEHit LorR){
        if (!hasAttacked) {
            // get+move to target
            moveToTarget(LorR);
            if (distToActivate < 0.1f ){
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
    void moveToTarget(BossAOEHit LorR) {

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
        if (LorR.getCanAttack() && counter % Mathf.Round(ParticalDuriation / Time.fixedDeltaTime) == 0){
            LorR.setCanAttack(false);
            LorR.setBeamActive(true);
            hasAttacked = true;
            if (stage % 2 == 0){
                setGUI(false, leftFireOff, leftVisualMarker) ;
            }else{
                setGUI(false, rightFireOff, rightVisualMarker);
            }

        }
    }
    //sink parts of the harbor, reset weapon position
    void cannotDamageAnimationEnd(BossAOEHit LorR){
        if (counter % Mathf.Round((AttactColdDownDuriation-1.1f) / Time.fixedDeltaTime) == 0){

            LorR.setSinkTheHarbor(true);
            Debug.LogError("sink harbour");
        }
        if ( counter % Mathf.Round((AttactColdDownDuriation) / Time.fixedDeltaTime) == 0){
           // LorR.transform.SetParent(LorR.getMyParent());
            hasAttacked = false;
            LorR.Resetposition();
            setNewWeapon(LorR.transform.name);
            stage++;
            counter = 0;
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
    

}
