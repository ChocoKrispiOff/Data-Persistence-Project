using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI highscorePlayerText;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TextMeshProUGUI errorText;

    private void Awake()
    {
        highscoreText.gameObject.SetActive(false);
        highscorePlayerText.gameObject.SetActive(false);
        errorText.gameObject.SetActive(false);
    }

    private void Start()
    {
        SaveManager.instance.LoadInfo();

        if (SaveManager.instance.highscore != 0)
        {
            highscoreText.gameObject.SetActive(true);
            highscorePlayerText.gameObject.SetActive(true);
            highscoreText.text = $"Highscore: {SaveManager.instance.highscore}";
            highscorePlayerText.text = $"Name: {SaveManager.instance.highscoreName}";
        }
    }

    public void PlayClicked()
    {
        CheckName();
    }

    public void OptionsClicked()
    {

    }

    public void ExitClicked()
    {
        SaveManager.instance.SaveInfo();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void CheckName()
    {
        string username = nameField.text;
        
        if (string.IsNullOrEmpty(username))
        {
            errorText.gameObject.SetActive(true);
        }
        else
        {
            SaveManager.instance.username = username;
            SceneManager.LoadScene(1);
        }
    }
}
