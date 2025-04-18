using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public Button buttonMale;
    public Button buttonFemale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonMale.onClick.AddListener(() => OnButtonClick("Selection1"));
        buttonFemale.onClick.AddListener(() => OnButtonClick("Selection2"));


    }

    // Update is called once per frame
    void OnButtonClick(string playerClass)
    {
        var playerName = nameInputField.text;

        PlayerPrefs.GetString("PlayerName", playerName);
        PlayerPrefs.SetString("PlayerClass", playerClass);
        SceneManager.LoadScene("Game1");

    }
}
