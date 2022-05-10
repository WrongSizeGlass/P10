using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

public class ReadJson : MonoBehaviour
{
    private string jsonString;
    private string path;
    private string pathA = "/Json/28min.json";
    //private string pathA = "/Json/MergedGroupA.json";
    private string pathB = "/Json/MergedGroupB.json";
    private JsonData itemData;
    private List<Vector3> Q1positions;
    private List<Vector3> Q2positions;
    private List<Vector3> Q3positions;
    int insertNumber = 0;
    public List<Material> mat;
    MeshRenderer meshrender;
    SphereCollider col;
    public GameObject prefab;
  //  public TextAsset test;
    bool firsthalf = false;
    bool firstquater = false;
    bool thridquearter = false;
    bool rest = false;
    int counter = 0;
    [SerializeField] private Transform Q1Parent;
    [SerializeField] private Transform Q2Parent;
    [SerializeField] private Transform Q3Parent;
    // Start is called before the first frame update
    void Start()
    {
        // the following is only needed if you have 2 scenes
        if (SceneManager.GetActiveScene().name == "Sector_Model")
        {
            path = pathB;
            Debug.LogWarning("path= pathB");
        }else {
            path = pathA;
            Debug.LogWarning("path = pathA");
        }
        // the following if statments are only relevant if you want a list of positions based on which quest the participant is on
        // you only need 1 list
        Q1positions = new List<Vector3>();
        Q2positions = new List<Vector3>();
        Q3positions = new List<Vector3>();
        jsonString = File.ReadAllText(Application.dataPath + path);
        itemData = JsonMapper.ToObject(jsonString);
        //Debug.LogError("itemData[0].Count: " + itemData[0][0][0].Count);
        for (int k = 0; k < itemData[0].Count; k++)
        {
            for (int i = 0; i < itemData[0][0][0].Count; i++)
            {
                GetPlayerPositionsFromJson(0, i);
            }
        }
        // the amount of if statments are only relevant if you want a list of positions based on which quest the participant is on
        // you can just do one for loop with your position list, and remove the last argument in the createHeatMap function
        // depended on the amount of data you have collected spawning the objects in start might crash or slow your pc
        /*
            a solution I have used is using a fixed update and splits of the the amount game objects created with 1 sec interval
            code fx
            private void FixedUpdate(){
                    Fixedcounter++;
                    if (!JustOnce && Fixedcounter % Mathf.Round(1f / Time.fixedDeltaTime) ==0) {
                         for (int l = 0; l < Q1positions.Count /2 (or 4 or how many intervals is needed); l++) {
                                CreateHeatMap(l, Q1positions,1);
                         }
                        JustOnce = true;
                    }
         }
         then just copy past and change the values depended on the amount of intervals you need
         */
        for (int l = 0; l < Q1positions.Count; l++)
        {
            CreateHeatMap(l, Q1positions,1);
        }
        for (int m = 0; m < Q2positions.Count; m++)
        {
            CreateHeatMap(m, Q2positions,2);
        }
        for (int n = 0; n < Q3positions.Count; n++)
        {
            CreateHeatMap(n, Q3positions,3);
        }

    }
    bool q1One = false;
    bool q2One = false;
    bool q3One = false;
   
    private void FixedUpdate()
    {
        counter++;
        if (!q1One && counter % Mathf.Round(1f / Time.fixedDeltaTime) == 0)
        {
            for (int l = 0; l < Q1positions.Count; l++)
            {
                CreateHeatMap(l, Q1positions, 1);
            }
            q1One = true;
        }
        if (!q2One && counter % Mathf.Round(2f / Time.fixedDeltaTime) == 0)
        {
            for (int l = 0; l < Q1positions.Count; l++)
            {
                CreateHeatMap(l, Q1positions, 1);
            }
            q2One = true;
        }
        if (!q1One && counter % Mathf.Round(3f / Time.fixedDeltaTime) == 0)
        {
            for (int l = 0; l < Q1positions.Count; l++)
            {
                CreateHeatMap(l, Q1positions, 1);
            }
            q3One = true;
        }
    }

    void GetPlayerPositionsFromJson(int participatNumber,int QuestNumber){
        float x = 0;
        float y = 0;
        float z = 0;

        // here we get json index[0][participantNR][0][which quest][player pos]
        // with the purpose to convert the vector3 string to a vector 3
        // the index the string "Player_Positions" should be named the same as the list string in your QuestData Class
        for (int i = 0; i < itemData[0][participatNumber][0][QuestNumber]["Player_Positions"].Count; i++){
           // Debug.Log(itemData[0][0][0][PuzzleNumberInOrder]["Player_Positions"][i].ToString());
            
                int startZ = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().IndexOf(itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Split(',')[2]);
                int endZ = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().IndexOf(')');

                string xString = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Split(',')[0].Trim('(');
                string yString = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Split(',')[1];
                string zString = itemData[0][participatNumber][0][QuestNumber]["Player_Positions"][i].ToString().Substring(startZ, endZ - startZ);

                x = float.Parse(xString);
                y = float.Parse(yString);
                z = float.Parse(zString);

            // the following if statments are only relevant if you want a list of positions based on which quest the participant is on
            // you can just do positions.Add( new Vector3(x, y, z));
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
    /*
        the following function creates and spawns a Sphere prefab on the positions from the list
        you will need a seperate script to make the gameobject to change color I have called mine for ChangeColor
        you will also need a default mat, I have used UI/Unlit/Detail, because then I also can changes the transparentsy
     */
    // int quest is only used here to generate differnt colors based on which quest the participant is on
    void CreateHeatMap(int posIndex, List<Vector3> positions, int quest)
    {
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        meshrender = sphere.GetComponent<MeshRenderer>();
        col = sphere.GetComponent<SphereCollider>();
        sphere.AddComponent<ChangeColor>();
        sphere.GetComponent<ChangeColor>().questNR = quest;
        sphere.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().isKinematic = true;
        sphere.tag = "Positions";
        sphere.name ="Q"+quest+ "pos" + posIndex;
        col.isTrigger = true;
        //Instantiate(prefab, positions[posIndex], Quaternion.identity);
        sphere.transform.position = positions[posIndex];
        sphere.transform.localScale = new Vector3(1, 0.5f, 1);
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
