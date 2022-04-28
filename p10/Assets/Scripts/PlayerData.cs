using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private int seconds = 0;
    private List<Vector3> ppQ1;
    private List<Vector3> ppQ2;
    private List<Vector3> ppQ3;
    private int counter = 0;
    private const int oneSecond = 1;
    [SerializeField] Transform player;
    MainQuestController mqc;
    bool q1, q2, q3;
    bool q1Once, q2Once, q3Once;
    int quest = 1;
    [SerializeField] private PlayerPositionMap ppm;
    // Start is called before the first frame update
    void Start()
    {
        mqc = GetComponent<MainQuestController>();
        ppQ1 = new List<Vector3>();
        ppQ2 = new List<Vector3>();
        ppQ3 = new List<Vector3>();
        q1 = true;
        q1Once = false;
        q2Once = false;
        q3Once = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (mqc.getLvl1Complete() && !q1Once) {
            quest = 1;
            ppm.writeJason1 = true;
            q1 = false;
            q2 = true;
            q1Once = true;
            seconds = 0;
            
        }
        if (mqc.getLvl2Complete() && !q2Once) {
            quest = 2;
            ppm.writeJason2 = true;
            q2 = false;
            q3 = true;
            q2Once = true;
            seconds = 0;
           
        }
        if (mqc.getLvl3Complete() && !q3Once) {
            quest = 3;
            ppm.writeJason3 = true;
            q3Once = true;
            q3 = false;
        }
        incrementData();
    }
    void incrementData() {
        if (counter % Mathf.Round(oneSecond / Time.fixedDeltaTime) == 0 && counter!=0){
            

            if (q1) 
                ppQ1.Add(player.transform.position);
            
            if(q2)
                ppQ2.Add(player.transform.position);

            if(q3)
                ppQ3.Add(player.transform.position);

            seconds++;
        }
    }
    public int getQuest() {
        return quest;  
    }
    public List<Vector3> getPlayerPosQ1List() {
        return ppQ1;
    }
    public List<Vector3> getPlayerPosQ2List(){
        return ppQ2;
    }
    public List<Vector3> getPlayerPosQ3List(){
        return ppQ3;
    }
}
