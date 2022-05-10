using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;

/*
 Quest data is what each index in the json will look like fx
 "SavedDataList": [
    {
      "UniqueID": 11,
      "QuestNr": 1,
      "Time": 273,
      "mapName": "Multiple_Nuclei",
      "Player_Positions": [
        "(287.53, 6.91, 45.69)",
        "(287.67, 6.97, 44.85)",.........]
        },
 */
public class QuestData
{

    public double UniqueID;
    public int QuestNr;
    public int Time;
    public string mapName;
    public List<string> Player_Positions;

    public QuestData(double UniqueID_, int QuestNr_, int Time_, string mapName_)
    {
        this.UniqueID = UniqueID_;
        this.QuestNr = QuestNr_;
        this.Time = Time_;
        this.mapName = mapName_;
        this.Player_Positions = new List<string>();
       // this.EventCapsuleCounter = EventCapsuleCounter;
    }
}
// this will store all QuestData classes in a list
public class SavedData
{
    public List<QuestData> SavedDataList;
    public SavedData()
    {

        this.SavedDataList = new List<QuestData>();
    }

}
public class PlayerPositionMap : MonoBehaviour
{
//  where you will store the json file, this was the pos where it was stored when you tested my project
    private static string path = "SaveFile.json";


    private string mapName;

    // a seperat script from where we gets the data, you can store the data in this script it is up to you:)
    [SerializeField] private PlayerData pd;

    // assign class data onces
    private bool p1once = false;
    private bool p2once = false;
    private bool p3once = false;

    // quest data list
    static SavedData sd;
    // I only needed 3 indexs for my json you can add more 
    public QuestData q1 = null;
    public QuestData q2 = null;
    public QuestData q3 = null;

    private List<string> MyList;

    static bool staticOnce = false;
    static bool staticWriteOnce = false;


    // just aditional conditions so I can see in the inspector how many classes that has been assigned
    public bool writeJason1 = false;
    public bool writeJason2 = false;
    public bool writeJason3 = false;
    static int indexCounter = 0;
    private double MyUniqueID;
    private int questNumber = 1;
    int seconds = 0;
    void Start()
    {
        MyUniqueID = Random.Range(0,200);
        MyList = new List<string>();
        p1once = false;
        p2once = false;
        p3once = false;
    }
    bool setOnce = false;
    private void Update(){
        // for some reason it works better in update then in start xD
        if (!setOnce){
          
            sd = new SavedData();
            staticOnce = true;
            mapName = SceneManager.GetActiveScene().name;
            setOnce = true;
        }

        // When player has completed quest 1 create a new QuestData class
        if (!p1once && writeJason1){
            if (q1 == null && !p1once){
                // pd. is the script where we gets the data
                q1 = new QuestData(MyUniqueID, 1, pd.getPlayerPosQ1List().Count+1, mapName);
                for (int i = 0; i < pd.getPlayerPosQ1List().Count; i++){
                    q1.Player_Positions.Insert(0, pd.getPlayerPosQ1List()[i].ToString());
                }
                sd.SavedDataList.Insert(indexCounter, q1);
                indexCounter++;
                p1once = true;
               // Debug.Log("event capsule score: " + (p1timer.ECC_1 + p1timer.ECC_2));
            }
            
        }
        // When player has completed quest 2 create a new QuestData class
        if (!p2once && writeJason2){
            if (q2 == null && !p2once){
                questNumber = 2;
                // pd. is the script where we gets the data
                q2 = new QuestData(MyUniqueID, 2, pd.getPlayerPosQ2List().Count + 1, mapName);
                for (int i = 0; i < pd.getPlayerPosQ2List().Count; i++){
                    q2.Player_Positions.Insert(0, pd.getPlayerPosQ2List()[i].ToString());
                }
                sd.SavedDataList.Insert(indexCounter, q2);
                indexCounter++;
                p2once = true;
               
            }
           
        }
        // When player has completed quest 3 create a new QuestData class
        if (!p3once && writeJason3){
            if (q3 == null && !p3once){
                questNumber = 3;
                // pd. is the script where we gets the data
                q3 = new QuestData(MyUniqueID, 3, pd.getPlayerPosQ3List().Count + 1, mapName);
                for (int i = 0; i < pd.getPlayerPosQ3List().Count; i++){
                    q3.Player_Positions.Insert(0, pd.getPlayerPosQ3List()[i].ToString());
                }
                sd.SavedDataList.Insert(indexCounter, q3);
                indexCounter++;
                p3once = true;
                
            }
            
        }
        // when the 3th QuestData has been created and we haven't written json yet
        if (p3once && !staticWriteOnce)
        {
            writeJson(sd);
            staticWriteOnce = true;
        }

        // when we have written json we can change scene
        if (Exit()) {
            SceneManager.LoadSceneAsync(2);
        }
    }
    static bool oneTime = false;
    public bool Exit()
    {
        return canExit;
    }
    public static bool canExit = false;
    // write json
    static void writeJson(SavedData ClassData)
    {
        Debug.LogError("Writing Jason");
        JsonData newData = new JsonData();

        newData = JsonMapper.ToJson(ClassData);

        string data = newData.ToString();

        File.WriteAllText(Application.dataPath + path, data);

        Debug.LogError(" Json is done ");
       
        canExit = true;

    }
}
