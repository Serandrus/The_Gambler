using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject menuPanel;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        instructionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
        SceneManager.LoadScene("Main");
    }

    public void ActivateInstructionsPanel()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
        instructionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void DeactivateInstructionsPanel()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
        instructionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClicked");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
