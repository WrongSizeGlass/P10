using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HammerDialogScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("External variables")]
    [SerializeField] private ThrowableHammer th;
    [SerializeField] private MainQuestController mqc;
    [SerializeField] private Quest2Controller q2;
    [SerializeField] private FireSpiretWalkRoute walk1;
    [SerializeField] private FireSpiretWalkRoute walk2;
    [SerializeField] private BossCutScne bcs;
    public TextMeshProUGUI textComponent;
    public GameObject dialogueBox;
    public float textSpeed;

    private int i = 0;
    private int ni = 0;
    private int j = 0;
    private int nj = 0;
    private int counter = 0;
    private AudioSource sound;
    private List<bool> mainDialogLocalBoolList;
    private List<bool> natureDialogLocalBoolList;
    private List<bool> mainDialogExternalBoolList;
    private List<bool> natureDialogExternalBoolList;

    [Header("Dialog variables")]
    [SerializeField] private List<AudioClip> audioTrack;
    [SerializeField] private List<AudioClip> natureTrack;
    [TextArea(4, 10)]
    [SerializeField] private string[] sentences;
    [TextArea(4, 10)]
    [SerializeField] private string[] natureSentences;
    string lastDialog;
    [SerializeField] private GameObject Qbutton;
    void Start()
    {
        Qbutton.SetActive(false);
        mainDialogLocalBoolList = new List<bool>();
        natureDialogLocalBoolList = new List<bool>();
        mainDialogExternalBoolList = new List<bool>();
        natureDialogExternalBoolList = new List<bool>();
       
        for (int i=0; i<audioTrack.Count; i++) {
            mainDialogLocalBoolList.Add(false);
            mainDialogExternalBoolList.Add(false);
        }
        for (int i =0; i<natureTrack.Count; i++) {
            natureDialogLocalBoolList.Add(false);
            natureDialogExternalBoolList.Add(false);
        }
        textComponent.text = string.Empty;
        sound = GetComponent<AudioSource>();
       
    }
    bool qPressed = false;
    // Update is called once per frame
    void FixedUpdate(){
        counter++;
        if (counter % Mathf.Round((1.5f) / Time.fixedDeltaTime) == 0){
            if (!sound.isPlaying && i < audioTrack.Count){
                SetCondition(audioTrack, sentences, mainDialogExternalBoolList, mainDialogLocalBoolList, true, i, j);
            }
        }
        if (counter % Mathf.Round((1f) / Time.fixedDeltaTime) == 0){
            if (!sound.isPlaying && ni < natureTrack.Count){
                SetCondition(natureTrack, natureSentences, natureDialogExternalBoolList, natureDialogLocalBoolList, false, ni, nj);
            }
        }
        
        showLastDialog();
        if (!sound.isPlaying && !showDialog){
            dialogueBox.SetActive(false);
        }
        if (i<= mainDialogLocalBoolList.Count && ni <= natureDialogLocalBoolList.Count){
            checkExternalBools();
        }
        if (mainDialogLocalBoolList[1] && !mainDialogLocalBoolList[2]&& !qPressed && !sound.isPlaying)
        {
            Qbutton.SetActive(true);
        }
    }

    void SetCondition(List<AudioClip> track, string[] txtDialog,List<bool> externalBool, List<bool> myLocalBools, bool main, int myi, int myj) {
        if (externalBool[myj] && !myLocalBools[myj] && !sound.isPlaying){
            textComponent.text = "";
            playSound(track, txtDialog,myi);
            myLocalBools[myj] = true;
            setIncrements(main);
        }
    }
    bool isPlaying = false;
    void playSound(List<AudioClip> track, string[] txtDialog, int i_)
    {
        if (!sound.isPlaying){
            isPlaying = false;
            StopAllCoroutines();
            sound.Stop();
        }
        lastDialog = txtDialog[i_];
        if (!isPlaying){
            isPlaying = true;
            dialogueBox.SetActive(true);
            StartDialogue(txtDialog,i_);
            sound.PlayOneShot(track[i_]);
            sound.Play();
        }
    }
    void StartDialogue(string[] txtDialog, int i_)
    {
        StartCoroutine(TypeLine(txtDialog, i_));
    }
    IEnumerator TypeLine(string [] txtDialog,int i_){
        foreach (char c in txtDialog[i_].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void showLastDialog() {
        if (Input.GetKey(KeyCode.Q)){
            if (!sound.isPlaying) {
                Qbutton.SetActive(false);
                dialogueBox.SetActive(true);
                textComponent.text = lastDialog;
                showDialog = true;
                qPressed = true;
            }
        }else {
            showDialog = false;
        }
    
    }
    bool showDialog = false;
    private void checkExternalBools(){
        mainDialogExternalBoolList[0] = th.getPlayHammerIntroVoiceLine();
        mainDialogExternalBoolList[1] = mainDialogLocalBoolList[0];
        mainDialogExternalBoolList[2] = mqc.getLvl1Active();
        mainDialogExternalBoolList[3] = walk1.getBanter1();
        mainDialogExternalBoolList[4] = mqc.getLvl1Complete();
        mainDialogExternalBoolList[5] = mainDialogLocalBoolList[4];
        mainDialogExternalBoolList[6] = q2.getChurchComplete();
        mainDialogExternalBoolList[7] = walk2.getBanter2();
        mainDialogExternalBoolList[8] = bcs.getCutSceneDone();

        natureDialogExternalBoolList[0] = walk1.getWalkHasStarted();
        natureDialogExternalBoolList[2] = walk2.getWalkHasStarted();
    }
    public void setNatureExternalList(int index, bool set) {
        natureDialogExternalBoolList[index] = set;
    }
    void setIncrements(bool main) {
        if (main){
            i++;
            j++;
        }else{
            ni++;
            nj++;
        }
    }

}
