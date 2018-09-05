using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDestroyer : MonoBehaviour {

	public float timerParaDestruir = 1f;

	public void StartDestroyBlock() {
		GetComponent<BoxCollider2D>().enabled = false;
		StartCoroutine(WaitAndDestroy(timerParaDestruir));
	}

	private IEnumerator WaitAndDestroy(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }
}
