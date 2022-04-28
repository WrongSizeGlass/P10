using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HammerDialogScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ThrowableHammer th;
    [SerializeField] private MainQuestController mqc;
    [SerializeField] private Quest2Controller q2;
    [SerializeField] private FireSpiretWalkRoute walk1;
    [SerializeField] private FireSpiretWalkRoute walk2;
    [SerializeField] private BossCutScne bcs;
    bool hammerIntro = false;
    bool lvl1Intro = false;
    bool lvl1Active = false;
    bool lvl1Banter = false;
    bool playLvl2Intro = false;
    bool playLvl2BonfireIntro = false;
    bool playLvl2TrappedSpirit = false;
    bool lvl2Banter1 = false;
    bool lvl2Banter2 = false;
    bool lvl3CutSceneDone = false;
    int i = 0;
    AudioSource sound;
    [SerializeField] private List<AudioClip> audioTrack;
    [TextArea(4, 10)]
    [SerializeField] private string[] sentences;
    public TextMeshProUGUI textComponent;
    public GameObject dialogueBox;
    public float textSpeed;
    void Start()
    {
        textComponent.text = string.Empty;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (th.getPlayHammerIntroVoiceLine() && !hammerIntro && !sound.isPlaying)
        {
            // play hammer intro dialog
            playSound();
            hammerIntro = true;
        }

        if (hammerIntro && !lvl1Intro && !sound.isPlaying)
        {
            // quest 1 intro
            i++;
            textComponent.text = "";
            playSound();
            lvl1Intro = true;
        }
        if (mqc.getLvl1Active() && !lvl1Active && !sound.isPlaying)
        {
            i++;
            // play trapped fire spirit dialog
            textComponent.text = "";
            playSound();
            lvl1Active = true;
        }
        // banter
        if (walk1.getBanter1() && !lvl1Banter && !sound.isPlaying)
        {
            i++;
            // player lvl 1 Banter
            textComponent.text = "";
            playSound();
            lvl1Banter = true;
        }
        if (mqc.getLvl1Complete() && !playLvl2Intro && !sound.isPlaying)
        {
            i++;
            // play intro to lvl 2
            textComponent.text = "";
            playSound();
            playLvl2Intro = true;
        }
        if (playLvl2Intro  && !playLvl2BonfireIntro && !sound.isPlaying)
        {
            i++;
            // play intro to lvl 2
            textComponent.text = "";
            playSound();
            playLvl2BonfireIntro = true;
        }
        if (q2.getChurchComplete() && !playLvl2TrappedSpirit && !sound.isPlaying)
        {
            i++;
            // play trapped firespirit 2
            textComponent.text = "";
            playSound();
            playLvl2TrappedSpirit = true;
        }
   
        if (walk2.getBanter2() && !lvl2Banter2 && !sound.isPlaying)
        {
            i++;
            // player lvl 2.2 Banter
            textComponent.text = "";
            playSound();
            lvl2Banter2 = true;
        }
        if (bcs.getCutSceneDone() && !lvl3CutSceneDone && !sound.isPlaying)
        {
            i++;
            // voiceline f�rdig med cutscene
            textComponent.text = "";
            playSound();
            lvl3CutSceneDone = true;
        }

        if (!sound.isPlaying)
        {
            dialogueBox.SetActive(false);
        }

    }
    bool isPlaying = false;
    void playSound()
    {
        if (!sound.isPlaying)
        {
            isPlaying = false;
            StopAllCoroutines();
            sound.Stop();
        }
        if (!isPlaying)
        {
            isPlaying = true;
            dialogueBox.SetActive(true);
            StartDialogue();
            // audioSource.pitch = picth;
            sound.PlayOneShot(audioTrack[i]);
            sound.Play();
        }

    }
    void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in sentences[i].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
