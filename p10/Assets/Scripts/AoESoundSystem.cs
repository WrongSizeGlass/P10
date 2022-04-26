using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AoESoundSystem : MonoBehaviour
{
    private ParticleSystem _parentParticleSystem;
    public AudioSource audioSource;
    public AudioClip Anticipation;
    public AudioClip Explosion;
    public float timer = 0.0f;
    public float volume = 0.5f;

    // Update is called once per frame
    public void Update()
    {
        timer += Time.deltaTime;
        
        if(timer < 0.1f)
        {
            StartCoroutine(PlaySounds());
            
            //StopAllCoroutines();
        }
    }

    public IEnumerator PlaySounds()
    {
        //Print the time of when the function is first called.
        audioSource.PlayOneShot(Anticipation, volume);
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.05f);

        //After we have waited 2 seconds print the time again.
        audioSource.PlayOneShot(Explosion, volume);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
