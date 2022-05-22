using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class DublicantFree {
    public List<Vector3> positions;
    public List<int> dublicants;
    public int dublicantCounter = 1;
    public int count { get; set; }
    public DublicantFree() {
        positions = new List<Vector3>();
        dublicants = new List<int>();
    }
    public void setLists( List<Vector3> orgList) {
        for (int i = 0; i < orgList.Count; i++){
            if (i!=0) {
                if (orgList[i - 1] == orgList[i]) {
                    dublicantCounter++;
                }
            }
            positions.Add(orgList[i]);
            dublicants.Add(dublicantCounter);
            dublicantCounter = 1;
        }      
         removeDublicants();
    }
    private void removeDublicants() {
        for (int i =1; i < positions.Count; i++) {
            if (positions[i - 1] == positions[i]){
                positions.RemoveAt(i);
                dublicants.RemoveAt(i);
            }
        }
        count = positions.Count;
    }
}

public class ReadJson : MonoBehaviour
{
    DublicantFree q1List;
    DublicantFree q2List;
    DublicantFree q3List;
    private string jsonString;
    private string path;
    private string pathA = "/Json/MergedGroupA.json";
    private string pathB = "/Json/MergedGroupB.json";
    private JsonData itemData;
    private List<Vector3> Q1positions;
    private List<Vector3> p1;
    private List<Vector3> p2;
    private List<Vector3> p3;
    
    private List<Vector3> Q2positions;
    private List<Vector3> Q3positions;
    int insertNumber = 0;
    public List<Material> mat;
    MeshRenderer meshrender;
    SphereCollider col;
    int counter = 0;
    [SerializeField] private Transform Q1Parent;
    [SerializeField] private Transform Q2Parent;
    [SerializeField] private Transform Q3Parent;

    // Start is called before the first frame update
    void Start(){
        q1List = new DublicantFree();
        q2List = new DublicantFree();
        q3List = new DublicantFree();
        path = SceneManager.GetActiveScene().name == "Sector_Model" ? pathB : pathA;
        Q1positions = new List<Vector3>();
        Q2positions = new List<Vector3>();
        Q3positions = new List<Vector3>();
        jsonString = File.ReadAllText(Application.dataPath + path);
        itemData = JsonMapper.ToObject(jsonString);
        for (int k = 0; k < itemData[0].Count; k++){
            for (int i = 0; i < itemData[0][0][0].Count; i++){    
                try {GetPlayerPositionsFromJson(k, i);} catch (Exception e) { }
            }
        }
        Q1positions = Q1positions.OrderBy(v => v.z).ToList();
        Q2positions = Q2positions.OrderBy(v => v.z).ToList();
        Q3positions = Q3positions.OrderBy(v => v.z).ToList();
        q1List.setLists(Q1positions);
        q2List.setLists(Q2positions);
        q3List.setLists(Q3positions);
    }
    private float seconds = 0.25f;
    private void FixedUpdate(){   
        counter++;
        if (counter % Mathf.Round(seconds / Time.fixedDeltaTime) == 0 && seconds<4){
            if (seconds ==1) {
                for (int i = 0; i < q1List.count; i++){
                    CreateHeatMap(i, q1List, 1); 
                }
            }
            if (seconds ==0.5f) {
                for (int j = 0; j < q2List.count; j++){
                    CreateHeatMap(j, q2List, 2);
                }
            }
            if (seconds ==0.75f) {
                for (int k = 0; k < q3List.count; k++){
                    CreateHeatMap(k, q3List, 3);
                }
            }
            seconds +=0.25f;
        }
    }

    void GetPlayerPositionsFromJson(int participatNumber,int QuestNumber){
        float x = 0;
        float y = 0;
        float z = 0;
        
        for (int i = 0; i < itemData[0][participatNumber][0][QuestNumber]["Player_Positions"].Count; i++){
            
            string xString = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Split(',')[0].Trim('(');
            string yString = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Split(',')[1];
            
            int startZ = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().IndexOf(itemData[0][participatNumber][0]
                [QuestNumber]["Player_Positions"][i].ToString().Split(',')[2]);
            int endZ = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().IndexOf(')');

            string zString = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Substring(startZ, endZ - startZ);

            x = float.Parse(xString);
            y = float.Parse(yString);
            z = float.Parse(zString);
            
            if (QuestNumber == 0) {
                Q1positions.Add( new Vector3(x, y, z));
            }
            if (QuestNumber == 1)
            {
                Q2positions.Add(new Vector3(x, y, z));
            }
            if (QuestNumber == 2)
            {
                Q3positions.Add(new Vector3(x, y, z));
            }
            //insertNumber++;
        }
    }
    void CreateHeatMap(int posIndex,DublicantFree df, int quest){
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        meshrender = sphere.GetComponent<MeshRenderer>();
        col = sphere.GetComponent<SphereCollider>();
        ChangeColor cc = sphere.AddComponent<ChangeColor>();
        cc.questNR = quest;
        cc.setDub(df.dublicants[posIndex]);
        sphere.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().isKinematic = true;
        sphere.tag = "Positions";
        sphere.name ="Q"+quest+ "pos" + posIndex;
        col.isTrigger = true;
        sphere.transform.position = df.positions[posIndex];
        sphere.transform.localScale = new Vector3(0.75f, (0.5f), 0.75f);
        meshrender.material = mat[0];
        if (quest == 1) {
            sphere.transform.SetParent(Q1Parent);
        } else if (quest == 2) {
            sphere.transform.SetParent(Q2Parent);
        } else if (quest == 3) {
            sphere.transform.SetParent(Q3Parent);
        }
    }
}
