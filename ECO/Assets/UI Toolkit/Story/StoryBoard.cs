using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StoryBoard : MonoBehaviour
{
    UIDocument menu;

    Label a;
    Label b;
    Label c;
    Label d;
    Label e;
    Label f;

    Label space;

    int actual;

    private bool textEnabled;
    int textTimer;
    // Start is called before the first frame update
    void OnEnable()
    {
        textEnabled = false;
        actual = 0;
        textTimer = 0;

        menu = GetComponent<UIDocument>();
        VisualElement root = menu.rootVisualElement;

        a = root.Q<Label>("1");
        b = root.Q<Label>("2");
        c = root.Q<Label>("3");
        d = root.Q<Label>("4");
        e = root.Q<Label>("5");
        f = root.Q<Label>("6");

        space = root.Q<Label>("space");
    }

    // Update is called once per frame
    void Update()
    {
        textTimer++;
        if(textTimer > 300)
        {
            StartCoroutine(alternateOpacity());
            textTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (actual)
            {
                case 0:
                    a.AddToClassList("unenabled");
                    b.RemoveFromClassList("unenabled");
                    actual++;
                    break;
                case 1:
                    b.AddToClassList("unenabled");
                    c.RemoveFromClassList("unenabled");
                    actual++;
                    break;
                case 2:
                    c.AddToClassList("unenabled");
                    d.RemoveFromClassList("unenabled");
                    actual++;
                    break;
                case 3:
                    d.AddToClassList("unenabled");
                    e.RemoveFromClassList("unenabled");
                    actual++;
                    break;
                case 4:
                    e.AddToClassList("unenabled");
                    f.RemoveFromClassList("unenabled");
                    actual++;
                    break;
                case 5:
                    f.AddToClassList("unenabled");
                    SceneManager.LoadScene("PlayerShoot");
                    break;
            }
        }
    }

    private IEnumerator alternateOpacity()
    {
        yield return new WaitForSeconds(1);

        if(textEnabled)
        {
            space.AddToClassList("unenabled");
            textEnabled = false;
        }
        else
        {
            space.RemoveFromClassList("unenabled");
            textEnabled = true;
        }
    }
}
