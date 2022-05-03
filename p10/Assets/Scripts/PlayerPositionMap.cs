using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.SceneManagement;
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
    private static string path = "SaveFile.json";

  //  public GameObject GUIMenu;
   // private MenuGUI MGUI;
 //   public bool Emergent = false;
    //private bool MyEmergent = false;
    private string mapName;
   // public GameObject p1TimerObject;
  //  public GameObject p2TimerObject;
  //  public GameObject p3TimerObject;
  //  private GameObject GUI;
  //  private static MenuGUI mGUI;
    [SerializeField] private PlayerData pd;


    private bool p1once = false;
    private bool p2once = false;
    private bool p3once = false;

    static SavedData sd;
    public QuestData q1 = null;
    public QuestData q2 = null;
    public QuestData q3 = null;

    List<string> MyList;
    List<string> p2pos;
    List<string> p3pos;


    /*public static PuzzleData P1Data_2 = null;
    public static PuzzleData P2Data_2 = null;
    public static PuzzleData P3Data_2 = null;*/

    static bool staticOnce = false;
    static bool staticWriteOnce = false;
    private bool JustOnce = false;
    public bool writeJason1 = false;
    public bool writeJason2 = false;
    public bool writeJason3 = false;
    static int indexCounter = 0;
    private double MyUniqueID;
    private int questNumber = 1;
    int seconds = 0;
    void Start()
    {

        //  GUI = GameObject.FindGameObjectWithTag("GUI");
        ///  mGUI = GUI.GetComponent<MenuGUI>();
        MyUniqueID = Random.Range(0,200);
        MyList = new List<string>();
        // p2pos = new List<string>();
        //  p3pos = new List<string>();
       /* q1 = p1TimerObject.GetComponent<PuzzleTimer>();
        q1 = p2TimerObject.GetComponent<PuzzleTimer>();
        p3timer = p3TimerObject.GetComponent<PuzzleTimer>();*/
        p1once = false;
        p2once = false;
        p3once = false;


    }

    /* static bool getGameType()
     {
         return mGUI.getGameType();
     }
     static double getUniqueID()
     {
         return mGUI.getUniqeID();
     }

     public bool CanSkipCutScenes()
     {
         return csd.CollectionList.Count == 3;

     }*/

    void setQuestData(QuestData q) { 
    
    }
    bool setOnce = false;
    private void Update(){
        if (!setOnce){
           // MyUniqueID = getUniqueID();
            sd = new SavedData();
            staticOnce = true;
            mapName = SceneManager.GetActiveScene().name;
            setOnce = true;
        }
       // Debug.LogError("j1:" + writeJason1 + " j2: " + writeJason2 + " j3 " + writeJason3);

        if (!p1once && writeJason1){
            if (q1 == null && !p1once){
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
        if (!p2once && writeJason2){
            if (q2 == null && !p2once){
                questNumber = 2;
                q2 = new QuestData(MyUniqueID, 2, pd.getPlayerPosQ2List().Count + 1, mapName);
                for (int i = 0; i < pd.getPlayerPosQ2List().Count; i++){
                    q2.Player_Positions.Insert(0, pd.getPlayerPosQ2List()[i].ToString());
                }
                sd.SavedDataList.Insert(indexCounter, q2);
                indexCounter++;
                p2once = true;
                // Debug.Log("event capsule score: " + (p1timer.ECC_1 + p1timer.ECC_2));
            }
           
        }
        if (!p3once && writeJason3){
            if (q3 == null && !p3once){
                questNumber = 3;
                q3 = new QuestData(MyUniqueID, 3, pd.getPlayerPosQ3List().Count + 1, mapName);
                for (int i = 0; i < pd.getPlayerPosQ3List().Count; i++){
                    q3.Player_Positions.Insert(0, pd.getPlayerPosQ3List()[i].ToString());
                }
                sd.SavedDataList.Insert(indexCounter, q3);
                indexCounter++;
                p3once = true;
                // Debug.Log("event capsule score: " + (p1timer.ECC_1 + p1timer.ECC_2));
            }
            
        }
      
        if (p3once && !staticWriteOnce)
        {
            writeJson(sd);
            staticWriteOnce = true;
        }
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
    static void writeJson(SavedData ClassData)
    {
        Debug.LogError("Writing Jason");
        JsonData newData = new JsonData();

        newData = JsonMapper.ToJson(ClassData);

        string data = newData.ToString();

        File.WriteAllText(Application.dataPath + path, data);

        Debug.LogError(" Json is done ");
        //SceneManager.LoadSceneAsync(0);
        canExit = true;

    }
}
