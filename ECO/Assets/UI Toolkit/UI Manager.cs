using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    UIDocument hud;

    VisualElement DO;
    VisualElement RE;
    VisualElement MI;
    // Start is called before the first frame update
    void OnEnable()
    {
        hud = GetComponent<UIDocument>();
        VisualElement root = hud.rootVisualElement;
        Debug.Log(root.name);

        DO = root.Q<VisualElement>("Do");
        RE = root.Q<VisualElement>("Re");
        MI = root.Q<VisualElement>("Mi");
        Debug.Log(root.name);
    }

    private void Update()
    {
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
}
