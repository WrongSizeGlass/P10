using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAOEHit : AOEHIt
{
    [SerializeField]private Transform myParent;
    private BossBehavior bb;
    private DestroyHarborTest dht;
    private Vector3 myResetPos;
    private bool canAttack = true;
    private bool resetAttact = true;
    public GameObject beam;
    bool isBeamActive = false;
    bool sinkTheHarbor = false;
    SphereCollider sc;
    bool intro = true;
    [SerializeField] Transform player;
    float dist = 1;

    //public AudioSource audioSource;
    //public AudioClip Anticipation;
  //  public AudioClip Explosion;
   // public float volume = 0.5f;
    // setters
    public void setBossBehavior(Transform set) {
      //  myParent = transform.parent.GetComponent<Transform>();
        bb = set.GetComponent<BossBehavior>();
        dht = GetComponent<DestroyHarborTest>();
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;
        // beam = transform.GetChild(0).GetComponent<GameObject>();
    }

    public void setIntro(bool set) {
        intro = set;
    }
    public bool getIntro() {
        return intro;
    }
    public void setCanAttack(bool set) {
        canAttack = set;
    }
    public void setResetAttact(bool set) {
        resetAttact = set;
    }
    public void setMyResetPos() {
        myResetPos = myParent.position;
        dist = 1;
    }
    public void setSinkTheHarbor(bool set) {
        sinkTheHarbor = set;
    }
    public void setBeamActive(bool set) {
        isBeamActive = set;
        beam.SetActive(set);
    }
    public string getName() {
        return this.name;
    }
    // getters
    public bool getCanAttack() {
        return canAttack;
    }
    public bool getResetAttack() {
        return resetAttact;
    }
    public bool getBeamActive()
    {
        return isBeamActive;
    }
    public bool getSinkTheHarbor() {
        return sinkTheHarbor;
    }
    public Transform getMyParent() {
        return myParent;
    }
    public Transform getMyTransform() {
        return this.transform;
    }
    public void resetPos() {
        transform.position = myResetPos;
    }
    public void Resetposition(){
        if (!getCanAttack() && transform.position != myResetPos){
            setSinkTheHarbor(false);
            transform.position = myResetPos;
            setBeamActive(false);
            setResetAttact(true);
            setCanAttack(true);

            
            //bb.incrementWeapon();
        }
    }


    // Start is called before the first frame update
    void Start(){
        audioSource = GetComponent<AudioSource>();
        //Resetposition();
    }

    // Update is called once per frame
    void Update()
    {
        //setMyResetPos();
        if (getIntro()) {
            if (getSinkTheHarbor())
            {

                dht.enabled = true;
                sc.enabled = true;
            }
            else if (!getSinkTheHarbor())
            {
                sc.enabled = false;
                dht.enabled = false;
            }
        }
        if (!getIntro()) {
            if (Vector3.Distance(transform.position, player.position) < dist)
            {
                sc.enabled = true;
            }
            else
            {
                sc.enabled = false;
                hasHit = false;
            }
        }
        
        setMyResetPos();
    }
    bool hasHit = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") {
            Debug.LogError("## player is here");
            hasHit = true;
            dist = 1.5f;
        }
        if (other.transform != null)
        {
            //play sound explosion
            
        }
        volume = startvolume;
        audioSource.PlayOneShot(Explosion, volume);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.LogError("## player left");
            hasHit = false;
            volume = 0;
        }
    }
    public bool getHasHit() {
        return hasHit;
    }
}
