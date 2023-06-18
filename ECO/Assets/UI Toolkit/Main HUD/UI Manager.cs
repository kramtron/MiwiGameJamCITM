using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    UIDocument hud;

    private VisualElement DO;
    private VisualElement RE;
    private VisualElement MI;

    private VisualElement separation;
    private bool escActive = false;

    Button continueButton;
    Button menuButton;
    Button exitButton;

    VisualElement buttonsHolder;
    // Start is called before the first frame update
    void OnEnable()
    {
        escActive = false;
        hud = GetComponent<UIDocument>();
        VisualElement root = hud.rootVisualElement;
        Debug.Log(root.name);

        DO = root.Q<VisualElement>("Do");
        RE = root.Q<VisualElement>("Re");
        MI = root.Q<VisualElement>("Mi");

        separation = root.Q<VisualElement>("separation");
        buttonsHolder = root.Q<VisualElement>("buttons-center");

        continueButton = root.Q<Button>("continue");
        menuButton = root.Q<Button>("backtomenu");
        exitButton = root.Q<Button>("exit");

        continueButton.RegisterCallback<ClickEvent>(continueEvent);
        menuButton.RegisterCallback<ClickEvent>(menuEvent);
        exitButton.RegisterCallback<ClickEvent>(exitEvent);
        Debug.Log(root.name);
    }

    private void Update()
    {
        TogglePause(escActive);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escActive = !escActive;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DO.AddToClassList("note-active");
            RE.RemoveFromClassList("note-active");
            MI.RemoveFromClassList("note-active");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RE.AddToClassList("note-active");
            DO.RemoveFromClassList("note-active");
            MI.RemoveFromClassList("note-active");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MI.AddToClassList("note-active");
            RE.RemoveFromClassList("note-active");
            DO.RemoveFromClassList("note-active");
        }
    }

    private void continueEvent(ClickEvent ev)
    {
        escActive = false;
    }
    private void menuEvent(ClickEvent ev)
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void exitEvent(ClickEvent ev)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void TogglePause(bool i)
    {
        if(i)
        {
            separation.AddToClassList("esc-active");
            Time.timeScale = 0f;
        }
        else
        {
            separation.RemoveFromClassList("esc-active");
            Time.timeScale = 1f;
        }

        buttonsHolder.SetEnabled(i);
        buttonsHolder.visible = i;
        continueButton.SetEnabled(i);
        menuButton.SetEnabled(i);
        exitButton.SetEnabled(i);
    }
}
