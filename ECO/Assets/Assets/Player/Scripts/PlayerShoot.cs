using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    enum active_note
    {
        DO,
        RE,
        MI
    }

    private active_note activeNote = active_note.DO;
    public static Action shootInput;
    public static Action reloadInput;
    public static Action changeDoInput;
    public static Action changeReInput;
    public static Action changeMiInput;

    [SerializeField] private KeyCode leftKey = KeyCode.Q;
    [SerializeField] private KeyCode rightKey = KeyCode.E;

    [SerializeField] private KeyCode DoKey;
    [SerializeField] private KeyCode ReKey;
    [SerializeField] private KeyCode MiKey;

    private void Start()
    {
        activeNote = active_note.DO;
    }

    void Update()
    {
        if(Input.GetKeyDown(leftKey))
        {
            switch (activeNote)
            {
                case active_note.DO:
                    changeMiInput?.Invoke();
                    activeNote = active_note.MI;
                    break;
                case active_note.RE:
                    changeDoInput?.Invoke();
                    activeNote = active_note.DO;
                    break;
                case active_note.MI:
                    changeReInput?.Invoke();
                    activeNote = active_note.RE;
                    break;
                default:
                    break;
            }
        }
        else if (Input.GetKeyDown(rightKey))
        {
            switch (activeNote)
            {
                case active_note.DO:
                    changeReInput?.Invoke();
                    activeNote = active_note.RE;
                    break;
                case active_note.RE:
                    changeMiInput?.Invoke();
                    activeNote = active_note.MI;
                    break;
                case active_note.MI:
                    changeDoInput?.Invoke();
                    activeNote = active_note.DO;
                    break;
                default:
                    break;
            }
        }


        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
        if (Input.GetKeyDown(DoKey)){

            changeDoInput?.Invoke();
            activeNote = active_note.DO;
        }
        if (Input.GetKeyDown(ReKey)){

            changeReInput?.Invoke();
            activeNote = active_note.RE;
        }
        if (Input.GetKeyDown(MiKey)){

            changeMiInput?.Invoke();
            activeNote = active_note.MI;
        }
        
    }
}
