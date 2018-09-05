using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour {

    public Animator transitionPanel;
    private bool transition = false;

    private IEnumerator Start () 
    {
        if (transition) {
            yield return null;
        }

        while (!LocalizationManager.instance.GetIsReady ()) 
        {
            yield return null;
        }
        transitionPanel.Play("transitionIn");
        transition = true;
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene (1);
    }

}