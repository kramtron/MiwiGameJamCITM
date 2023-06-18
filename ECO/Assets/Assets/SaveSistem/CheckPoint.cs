using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public static Action saveInput;
    [SerializeField] bool saved = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !saved)
        {
            Debug.Log("CheckPoint Save");
            saveInput?.Invoke();
            saved = true;
        }
    }
}
