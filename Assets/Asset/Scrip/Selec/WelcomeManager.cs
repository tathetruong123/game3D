using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(LoadScene), 2);

    }

    // Update is called once per frame
    void LoadScene()
    {
        SceneManager.LoadScene("Selection");
    }
}
