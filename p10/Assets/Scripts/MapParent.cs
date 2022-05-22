using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapParent : MonoBehaviour
{
    // Start is called before the first frame update
    List<ChangeColor> cc;
    List<Transform> child;
    List<int> neighbors;
    List<int> dublicants;
    public bool DESC;
    public bool ASC;
    void Start()
    {
        child = new List<Transform>();
        cc = new List<ChangeColor>();
        neighbors = new List<int>();
        dublicants = new List<int>();

        for (int i =0; i< transform.childCount; i++) {
            child.Add(transform.GetChild(i).GetComponent<Transform>());
            cc.Add(child[i].GetComponent<ChangeColor>());
        }
        for (int i=0; i<cc.Count; i++) {
            neighbors.Add(cc[i].neighboor);
            dublicants.Add(cc[i].dublicants);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (ASC) {
            neighbors = neighbors.OrderBy(v => v).ToList();
        }
    }
}
