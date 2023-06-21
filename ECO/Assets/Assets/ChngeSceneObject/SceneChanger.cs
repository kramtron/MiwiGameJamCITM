using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private string actualScene;
    // Start is called before the first frame update
    void Start()
    {
        actualScene = SceneManager.GetActiveScene().name;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (actualScene)
            {
                case "PlayerShoot":
                    SceneManager.LoadScene("SecondLVL");
                    break;
                case "SecondLVL":
                    SceneManager.LoadScene("ThirdLVL");
                    break;
                case "ThirdLVL":
                    SceneManager.LoadScene("ThirdLVL");
                    break;
            }
        }
    }
}
