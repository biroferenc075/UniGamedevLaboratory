using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    Button menuButton;

    [SerializeField]
    GameObject loadingParent;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Awake()
    {
        menuButton.onClick.AddListener(() => Menu());
    }
    private void Menu()
    {
        AudioManager.Instance.PlayMenu();
        SceneManager.LoadSceneAsync("MainMenu");
        loadingParent.SetActive(true);
    }
}
