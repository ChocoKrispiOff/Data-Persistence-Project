using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIHandler : MonoBehaviour
{
    private GameObject button;
    public Material brickMaterial;

    [SerializeField] TextMeshProUGUI highscoreText;
    [SerializeField] TextMeshProUGUI highscorePlayerText;
    [SerializeField] TMP_InputField nameField;
    [SerializeField] TextMeshProUGUI errorText;

    [SerializeField] GameObject optionsPanel;

    private void Awake()
    {
        optionsPanel.SetActive(false);
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
        optionsPanel.SetActive(true);
    }

    public void OptionsBackClicked()
    {
        optionsPanel.SetActive(false);
    }

    public void ExitClicked()
    {
        SaveManager.instance.SaveInfo();

#if UNITY_EDITOR
        brickMaterial.color = Color.white;
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

    public void ChangeColor()
    {
        Color color = new Color();
        button = EventSystem.current.currentSelectedGameObject;
        color = button.GetComponent<ButtonManager>().button.buttonColor;
        brickMaterial.color = color;
    }
}
