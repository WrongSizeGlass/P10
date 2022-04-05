using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestController : MonoBehaviour
{
    // Start is called before the first frame update
    private Quest1Controller q1c;
    private Quest2Controller q2c;
    private bool lvl1Complete, lvl2Complete, lvl3Complete;
    [Header("Grouped elements that are specific for the different levels")]
    [SerializeField] private GameObject lvl1, lvl2, lvl3;
    void Start()
    {
        q1c = GetComponent<Quest1Controller>();
        q2c = GetComponent<Quest2Controller>();
    }

    // Update is called once per frame
    void Update(){
        lvl1Complete = getLvl1Complete();
        if (lvl1Complete && !lvl3Complete) {
            lvl2.SetActive(true);
        }
        
    }
    bool getLvl1Complete() {
         return q1c.getLevelOneComplete();
    }
}
