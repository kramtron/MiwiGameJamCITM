using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    enum active_note
    {
        DO,
        RE,
        MI
    }
    UIDocument hud;

    private active_note activeNote = active_note.DO;

    public GameObject player;
    PlayerStats stats;

    private VisualElement DO;
    private VisualElement RE;
    private VisualElement MI;

    [SerializeField] private KeyCode leftKey = KeyCode.Q;
    [SerializeField] private KeyCode rightKey = KeyCode.E;

    private VisualElement separation;
    private bool escActive = false;

    Button continueButton;
    Button menuButton;
    Button exitButton;

    VisualElement buttonsHolder;

    Slider healthSlider;
    VisualElement healthAmount;
    Texture2D healthBar;

    public static Action saveEvent;

    // Start is called before the first frame update
    void OnEnable()
    {
        activeNote = active_note.DO;
        player = GameObject.FindGameObjectWithTag("Player");
        stats = player.GetComponent<PlayerStats>();
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

        healthSlider = root.Q<Slider>("health");
        healthAmount = root.Q<VisualElement>("progress");

        continueButton.RegisterCallback<ClickEvent>(continueEvent);
        menuButton.RegisterCallback<ClickEvent>(menuEvent);
        exitButton.RegisterCallback<ClickEvent>(exitEvent);

        DO.AddToClassList("note-active");
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
            SetActiveDo();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveRe();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveMi();
        }

        SetActiveNote(activeNote);
        

        healthSlider.value = stats.hp / stats.maxHp * 100;
        healthAmount.style.width = stats.hp * 140 / stats.maxHp;
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
        saveEvent?.Invoke();

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

    private void SetActiveNote(active_note n)
    {
        if (Input.GetKeyDown(leftKey))
        {
            switch (n)
            {
                case active_note.DO:
                    SetActiveMi();
                    break;
                case active_note.RE:
                    SetActiveDo();
                    break;
                case active_note.MI:
                    SetActiveRe();
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(rightKey))
        {
            switch (n)
            {
                case active_note.DO:
                    SetActiveRe();
                    break;
                case active_note.RE:
                    SetActiveMi();
                    break;
                case active_note.MI:
                    SetActiveDo();
                    break;
                default:
                    break;
            }
        }
    }

    private void SetActiveDo()
    {
        CleanNoteStyles();
        DO.AddToClassList("note-center");
        RE.AddToClassList("note-right");
        MI.AddToClassList("note-left");
        activeNote = active_note.DO;
    }

    private void SetActiveRe()
    {
        CleanNoteStyles();
        RE.AddToClassList("note-center");
        DO.AddToClassList("note-left");
        MI.AddToClassList("note-right");
        activeNote = active_note.RE;
    }

    private void SetActiveMi()
    {
        CleanNoteStyles();
        MI.AddToClassList("note-center");
        RE.AddToClassList("note-left");
        DO.AddToClassList("note-right");
        activeNote = active_note.MI;
    }

    private void CleanNoteStyles()
    {
        DO.RemoveFromClassList("note-center");
        RE.RemoveFromClassList("note-center");
        MI.RemoveFromClassList("note-center");
        DO.RemoveFromClassList("note-right");
        RE.RemoveFromClassList("note-right");
        MI.RemoveFromClassList("note-right");
        DO.RemoveFromClassList("note-left");
        RE.RemoveFromClassList("note-left");
        MI.RemoveFromClassList("note-left");
    }
}
