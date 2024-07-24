using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagement : MonoBehaviour
{

    [Header("Main Menu Panels")]
    public GameObject optionsPanel;
    public GameObject quotePanel;
    public GameObject infoPanel;
    public GameObject backPanel;

    public void Start()
    {
        infoPanel.SetActive(false);    
        backPanel.SetActive(false); 
    }

    public void StartScene()
    {
        SceneManager.LoadScene("SingleCharacter");
    }

    public void InfoPanel(bool activate)
    {
        optionsPanel.SetActive(!activate);
        quotePanel.SetActive(!activate);
        infoPanel.SetActive(activate);
        backPanel.SetActive(activate);
    }


    public void Exit()
    {
        // Exit the application
        Application.Quit();

        // (This will not work in the Unity editor, but will work in a build)
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
