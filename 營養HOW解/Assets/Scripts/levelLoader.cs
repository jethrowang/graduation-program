using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }

    public void LoadThisLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadPreviousLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex-1));
    }

    public void LoadSomeLevel(int levelNum)
    {
        StartCoroutine(LoadLevel(levelNum));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // var loadingOperation = SceneManager.LoadSceneAsync(levelIndex);
        // loadingOperation.allowSceneActivation = false;
        transition.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
        // loadingOperation.allowSceneActivation = true;
    }
}