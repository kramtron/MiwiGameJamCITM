using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    UIDocument menu;

    Button playButton;
    Button continueButton;
    Button creditsButton;
    Button exitButton;

    public static Action cargarEvent;

    [SerializeField] private string playScene;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        playButton = root.Q<Button>("play");
        continueButton = root.Q<Button>("continue");
        creditsButton = root.Q<Button>("credits");
        exitButton = root.Q<Button>("exit");

        playButton.RegisterCallback<ClickEvent>(playEvent);
        continueButton.RegisterCallback<ClickEvent>(continueEvent);
        creditsButton.RegisterCallback<ClickEvent>(creditsEvent);
        exitButton.RegisterCallback<ClickEvent>(exitEvent);
    }

    void playEvent(ClickEvent ev)
    {
        SceneManager.LoadScene(playScene);
    }
    void continueEvent(ClickEvent ev)
    {
        cargarEvent?.Invoke();
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
