using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] private AudioClip takeDmgSound;
    [SerializeField] private AudioClip isDeadSound;
    public int hp = 2;
    public int currentHealth;
    public HealthBar healthBar;
    Rigidbody rb;
    public float vol = 0.4f;
    // List<string> tags = new List<string> { "House", "Boat", "Player" };
    private string myTag;
    AudioSource sound;
    private bool IamDead = false;
    private Transform player;
    bool once = false;
    private bool damageEffect = false;
    public int hpLost = 1;
    float x, y, z;
    public bool bossIce = false;
    [SerializeField] private BossBehavior bb;
    [SerializeField] private MainQuestController mqc;
    [SerializeField] private Transform waterRespawnGroup;
    [SerializeField] private Transform ChurchRespawnGroup;
    [SerializeField] private Transform BossRespawnGroup;
    [SerializeField] private RotateObjects ro;
    private List<Transform> waterRespawnList;
    private List<Transform> ChurchRespawnList;
    private List<Transform> BossRespawnList;
    Vector3 closesPos;
    Vector3 startClosesPos;
    bool playerHitWater = false;
    private CharacterController cc;
    string dmgByQuest="";
    int startHp;
    int hitCounter = 0;
    // Vector3 IwasHitHere;
    // Start is called before the first frame update
    void Awake() {
        sound = GetComponent<AudioSource>();
        startHp = hp;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(hp);
        }
        
        cc = GetComponent<CharacterController>();
        startClosesPos = new Vector3(999, 999, 999);
        closesPos = startClosesPos;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myTag = this.gameObject.tag;
        rb = GetComponent<Rigidbody>();
        x = transform.localScale.x;
        y = transform.localScale.y;
        z = transform.localScale.z;
        if (waterRespawnGroup!=null) {
            waterRespawnList = new List<Transform>();
            for (int i = 0; i < waterRespawnGroup.childCount; i++) {
                waterRespawnList.Add(waterRespawnGroup.GetChild(i).GetComponent<Transform>());

            }
        }
        if (ChurchRespawnGroup != null)
        {
            ChurchRespawnList = new List<Transform>();
            for (int i = 0; i < ChurchRespawnGroup.childCount; i++)
            {
                ChurchRespawnList.Add(ChurchRespawnGroup.GetChild(i).GetComponent<Transform>());

            }
        }
        if (BossRespawnGroup != null)
        {
            BossRespawnList = new List<Transform>();
            for (int i = 0; i < BossRespawnGroup.childCount; i++)
            {
                BossRespawnList.Add(BossRespawnGroup.GetChild(i).GetComponent<Transform>());

            }
        }
    }
    // Update is called once per frame
    void Update() {
        updateMyState();
        /*if (ro != null) {
            if (ro.getDealDmg()) {
                damageEffect = true;
                updateMyState();
            }
        }*/
        /*if (myTag=="Player") {
            playerIsDead();
        }*/
        if (myTag =="Player") {
            if (!sound.isPlaying && playDmgSound){
                playDmgSound = false;

            }
            if (!sound.isPlaying && pcisDead)
            {   
                pcisDead = false;
                hitCounter = 0;
            }
        }
    }
    public bool getIamDead(){
        return IamDead;
    }

    private void updateMyState() {
        if (getDamageEffect()) {
            
            effectWhenDamaged();
        //    Debug.LogError("¤¤ player is damaged " );
            
        }
      //  Debug.LogError("¤¤ player is damaged " + getDamageEffect());
        if (getHp()<=0 ) {
            
            IamDead = true;
            deathState();
        }   
    }

    private void effectWhenDamaged(){
       
        switch (myTag){
            case "House":
                
                damageEffect = false;
                break;
            case "Player":
                playerTakeDamage();
                
                Debug.LogError("damage player");
                damageEffect = false;
                break;
            case "Ice":
                setHp(hpLost);
                shrinkingEffect();
                damageEffect = false;
                break;
            case "Boat":
                setHp(hpLost);
                damageEffect = false;
                break;
            default:
              
               // damageEffect = false;
                break;
        }
    }

    private void deathState() {
        if (IamDead) {
            switch (myTag) {
                case "House":
                    Debug.LogError("I am dead I don't have a function:" + gameObject.name);
                    break;
                case "Player":
                    playerIsDead();
                    Debug.LogError("I am dead I don't have a function:" + gameObject.name);
                    break;
                case "Ice":
                    deadIce();
                    break;
                case "Boat":
                    deadBoat();
                    break;
                default:
                    Debug.LogError("I am dead I don't have a tag name:" + gameObject.name);
                    break;
            }
        }  
    }
    bool playDmgSound = false;
    void shrinkingEffect() {
       // Debug.Log(" ## I am damaged play sound");
        if (!playDmgSound && getHp()>=1) {
            playSound(takeDmgSound, hitCounter);
            playDmgSound = true;
        }
        if (!playDmgSound && getHp() < 1)
        {
            playSound(isDeadSound, hitCounter);
            playDmgSound = true;
        }
      //  Debug.LogError("## play " + sound.isPlaying + " hit counter: " + hitCounter);
            x = x - 10;
            y = y - 10;
            z = z - 10;
        
        
        transform.localScale = new Vector3(x,y,z);
        if (!sound.isPlaying) {
            playDmgSound = false;
            
        }
        
    }
    public void playerTakeDamage() {
        setHp(hpLost);
        hitCounter++;
        if (!playDmgSound)
        {
            playSound(takeDmgSound, hitCounter);
            playDmgSound = true;
           // Debug.LogError("¤¤ player is taking damage playing: " + sound.isPlaying);
        }
        
        healthBar.SetHealth(getHp());
    }

    bool onceDmg = false;
    bool deadOnce = false;
    private void deadIce() {
        if (!deadOnce) {
            playSound(isDeadSound,0);
            deadOnce = true;
        }
        if (bossIce && !onceDmg) {
            bb.degrementHP();
            onceDmg = true;
        }
        if (!sound.isPlaying) {
            Destroy(gameObject);
        }
    }
    bool playing = false;
    public void playSound(AudioClip audio, int index)
    {
        int i = index;
       // Debug.LogError("## i: " + i + " index: " + index + " isPlaying " + sound.isPlaying);
        if (!sound.isPlaying)
        {
            playing = false;
            sound.Stop();
        }
        if (!playing && !sound.isPlaying && i == index)
        {
            playing = true;
            sound.volume = 1;
            sound.pitch = 1;
            sound.PlayOneShot(audio);
            sound.Play();
            i++;
          //  Debug.LogError("## PLAY is playing : " + audio.name);
        }

    }
    bool pcisDead = false;
    void playerIsDead() {
        if (!pcisDead) {
            playSound(isDeadSound, 0);
            pcisDead = true;
        }
        if (waterRespawnGroup != null){
            if (playerHitWater){
               // Debug.LogError("respown player");
                respondPlayerFromWater();
            }
        }
        if (ChurchRespawnGroup != null && dmgByQuest =="Quest2") {
            if (getHp() <= 0) {
                respondPlayerFromChurch();
            }
        }
        if (BossRespawnGroup != null && dmgByQuest == "Quest3")
        {
            if (getHp() <= 0)
            {
                respondPlayerFromBoss();
            }
        }
        healthBar.SetMaxHealth(hp);
        //mqc.setPlayerIsDead(true);
        
    }
    public void setQuest(string tag) {
        dmgByQuest = tag;
    }
    bool deadboat = false;
    private void deadBoat() {
        if (!once) {
            player.GetComponent<Quest1Controller>().incrementBoatsSinked();
            once = true;
        }
        if (!deadboat) {
            playSound(isDeadSound, 0);
            deadboat = true;
        }
        GetComponent<MeshCollider>().convex = true;
        GetComponent<MeshCollider>().isTrigger = true;
        int child = transform.childCount-1;
        transform.position = Vector3.MoveTowards(transform.position,transform.GetChild(child).transform.position, 1 * Time.deltaTime);
        if (transform.position== transform.GetChild(child).transform.position) {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Hammer" && myTag != "Player" || collision.collider.tag == "shockWave") {
            damageEffect = true;
            //IwasHitHere = collision.transform.position;
        }
        if (collision.collider.tag=="Water" && myTag =="Player") {
           // Debug.LogError("### Water hit ");
        }
        if (collision.collider.tag == "shockWave" && myTag == "Player")
        {
            //damageEffect = true;
           // Debug.LogError("### shockWave hit ");
        }

    }
    public void setDamageEffect(bool set) {
        damageEffect = set;
    }
    public bool getDamageEffect() {
        return damageEffect;
    }
    int q = 99;
    public void respondPlayerFromWater()
    {
        cc.enabled = false;
        for (int i = 0; i < waterRespawnList.Count; i++)
        {
            if (Vector3.Distance(waterRespawnList[i].position, transform.position) < Vector3.Distance(transform.position, closesPos))
            {
                closesPos = waterRespawnList[i].position;
                q = i;
               // Debug.LogError("### " + waterRespawnList[i].name);
            }

        }
        if (q!=99) {
            this.gameObject.transform.position = waterRespawnList[q].position;
            closesPos = startClosesPos;
            playerHitWater = false;
            q = 99;
          //  Debug.LogError("### RESSPPPOOOONN");
            cc.enabled = true;
        }
    }
    public void respondPlayerFromBoss()
    {
        cc.enabled = false;
        for (int i = 0; i < BossRespawnList.Count; i++)
        {
            if (Vector3.Distance(BossRespawnList[i].position, transform.position) < Vector3.Distance(transform.position, closesPos))
            {
                closesPos = BossRespawnList[i].position;
                q = i;
               // Debug.LogError("### " + BossRespawnList[i].name);
            }

        }
        if (q != 99)
        {
            this.gameObject.transform.position = BossRespawnList[q].position;
            closesPos = startClosesPos;
            hp = startHp;
            q = 99;
          //  Debug.LogError("### RESSPPPOOOONN");
            cc.enabled = true;
        }
    }

    public void respondPlayerFromChurch()
    {
        cc.enabled = false;
        for (int i = 0; i < ChurchRespawnList.Count; i++)
        {
            if (Vector3.Distance(ChurchRespawnList[i].position, transform.position) < Vector3.Distance(transform.position, closesPos))
            {
                closesPos = ChurchRespawnList[i].position;
                q = i;
             //   Debug.LogError("### " + ChurchRespawnList[i].name);
            }
            
        }
        if (q != 99)
        {
            ro.setStart(false);
            this.gameObject.transform.position = ChurchRespawnList[q].position;
            closesPos = startClosesPos;
            hp = startHp;
            q = 99;
           // Debug.LogError("### RESSPPPOOOONN");
            cc.enabled = true;
        }
    }



    public void setHpLost(int dmg) {
        hpLost = dmg;
    }
    public void setHp(int dmg) {
        hp = hp - dmg;
    }
    public int getHp() {
        return hp;
    }
   public void setPlayerHitWater(bool set) {
        playerHitWater = set;
    }

    
    


}
