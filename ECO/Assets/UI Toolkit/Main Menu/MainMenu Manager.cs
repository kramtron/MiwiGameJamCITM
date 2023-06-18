using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    UIDocument menu;

    Button playButton;
    Button creditsButton;
    Button exitButton;

    [SerializeField] private string playScene;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        playButton = root.Q<Button>("play");
        creditsButton = root.Q<Button>("credits");
        exitButton = root.Q<Button>("exit");

        playButton.RegisterCallback<ClickEvent>(playEvent);
        creditsButton.RegisterCallback<ClickEvent>(creditsEvent);
        exitButton.RegisterCallback<ClickEvent>(exitEvent);
    }

    void playEvent(ClickEvent ev)
    {
        SceneManager.LoadScene(playScene);
    }
    void creditsEvent(ClickEvent ev)
    {

    }
    void exitEvent(ClickEvent ev)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
