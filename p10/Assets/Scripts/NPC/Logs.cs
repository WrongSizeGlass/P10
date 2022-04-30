using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logs : MonoBehaviour
{
    public GameObject fire;
    public GameObject ice;
    [SerializeField] private Transform player;
    public bool fireActive = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance( player.transform.position, transform.position)<1f && Input.GetKeyDown(KeyCode.E)) {
            fire.SetActive(true);
            ice.GetComponent<AudioSource>().volume = 0;
            ice.SetActive(false);
            fireActive = true;
           // Debug.LogError("__%%%%__");
        }
    }
    public bool getFireActive() {
        return fireActive;
    }
}
