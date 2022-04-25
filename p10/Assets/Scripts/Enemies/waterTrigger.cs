using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterTrigger : MonoBehaviour
{
    [SerializeField] MainQuestController mqc;
    [SerializeField] Health h;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Player") {
            h.setPlayerHitWater(true);
            Debug.LogError("PLAYER ### ");

        }
    }
}
