using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class BattleTransition : MonoBehaviour
{
    public Animator anim;

    public void Update()
    {
        // Check for user input to trigger the scene transition
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            NextScene();
        }
    }
    public void NextScene()
    {
        // Load the next scene in the build settings
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within bounds
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            StartCoroutine(LoadScene());
        }
        else
        {
            Debug.LogWarning("No more scenes to load.");
        }
    }
    IEnumerator LoadScene()
    {
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

