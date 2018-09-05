using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	private bool transition = false;
    public Animator transitionAnimator;
	public Animator animCreditos;

	public void CloseCreditos() {
		animCreditos.SetTrigger("Close");
	}

	public void OpenCreditos() {
		animCreditos.SetTrigger("Open");
	}
	
    public void GoToGame(int sceneIndex) {
        if (!transition) {
            transitionAnimator.Play("transitionIn");
            transition = true;
            StartCoroutine(WaitAndGoToScene(1f, sceneIndex));
        }
    }

     private IEnumerator WaitAndGoToScene(float waitTime, int sceneIndex) {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneIndex);
     }
}
