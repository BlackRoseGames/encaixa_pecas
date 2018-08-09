using System.Collections;
using UnityEngine;

public class AudioDestroyer : MonoBehaviour {

	AudioSource audioSource;

 void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitForSound(audioSource.clip));
    }

    public IEnumerator WaitForSound(AudioClip Sound)
    {
    	yield return new WaitUntil(() => audioSource.isPlaying == false);
		Destroy(gameObject);
	}
}
