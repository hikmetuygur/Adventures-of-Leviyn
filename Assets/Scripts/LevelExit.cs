using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadNextLevel() );
        }
    }
    
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        FindObjectOfType<ScenePersist>().ResetSession();
        SceneManager.LoadScene(nextSceneIndex);
    }
}