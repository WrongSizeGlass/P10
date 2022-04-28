using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    bool playLvl2TrappedSpirit = false;
    bool lvl2Banter1 = false;
    bool lvl2Banter2 = false;
    bool lvl3CutSceneDone = false;
    AudioSource sound;
    [SerializeField] private List<AudioClip> audioTrack;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (th.getPlayHammerIntroVoiceLine() && !hammerIntro && !sound.isPlaying)
        {
            // play hammer intro dialog
            playSound(0);
            hammerIntro = true;
        }

        if (hammerIntro && !lvl1Intro && !sound.isPlaying)
        {
            // quest 1 intro
            playSound(1);
            lvl1Intro = true;
        }
        if (mqc.getLvl1Active() && !lvl1Active && !sound.isPlaying)
        {
            // play trapped fire spirit dialog
            playSound(2);
            lvl1Active = true;
        }
        // banter
        if (walk1.getBanter1() && !lvl1Banter && !sound.isPlaying)
        {
            // player lvl 1 Banter
            playSound(3);
            lvl1Banter = true;
        }
        if (mqc.getLvl1Complete() && !playLvl2Intro && !sound.isPlaying)
        {
            // play intro to lvl 2
            playSound(4);
            playLvl2Intro = true;
        }
        if (q2.getChurchComplete() && !playLvl2TrappedSpirit && !sound.isPlaying)
        {
            // play trapped firespirit 2
            playSound(5);
            playLvl2TrappedSpirit = true;
        }
        if (walk2.getBanter1() && !lvl2Banter1 && !sound.isPlaying)
        {
            // player lvl 2.1 Banter
            playSound(6);
            lvl2Banter1 = true;
        }
        if (walk2.getBanter2() && !lvl2Banter2 && !sound.isPlaying)
        {
            // player lvl 2.2 Banter
            playSound(7);
            lvl2Banter2 = true;
        }
        if (bcs.getCutSceneDone() && !lvl3CutSceneDone && !sound.isPlaying)
        {
            // voiceline f�rdig med cutscene
            playSound(8);
            lvl3CutSceneDone = true;
        }

    }
    bool isPlaying = false;
    void playSound(int index)
    {
        if (!sound.isPlaying)
        {
            isPlaying = false;

            sound.Stop();
        }
        if (!isPlaying)
        {
            isPlaying = true;

            // audioSource.pitch = picth;
            sound.PlayOneShot(audioTrack[index]);
            sound.Play();
        }

    }
}
