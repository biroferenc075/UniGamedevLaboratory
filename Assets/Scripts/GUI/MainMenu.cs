using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Button playButton;
    [SerializeField]
    Button creditsButton;
    [SerializeField]
    Button exitButton;
    [SerializeField]
    Button backButton;

    [SerializeField]
    GameObject titleParent;
    [SerializeField]
    GameObject creditsParent;
    [SerializeField]
    GameObject loadingParent;

    public void Start()
    {
        MusicPlayer.Instance.PlayMusic();
    }

    public void StartGame()
    {
        AudioManager.Instance.PlayMenu();
        SceneManager.LoadSceneAsync("DesertLevel");
        titleParent.SetActive(false);
        loadingParent.SetActive(true);
    }

    public void ShowCredits()
    {
        titleParent.SetActive(false);
        creditsParent.SetActive(true);
    }

    public void ExitCredits()
    {
        titleParent.SetActive(true);
        creditsParent.SetActive(false);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Awake()
    {
        playButton.onClick.AddListener(() => StartGame());
        creditsButton.onClick.AddListener(() => ShowCredits());
        backButton.onClick.AddListener(() => ExitCredits());
        exitButton.onClick.AddListener(() => Exit());
    }
}
