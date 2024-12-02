using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VictoryScreen : MonoBehaviour
{
    [SerializeField]
    Button backToMenuButton;

    public void Start()
    {
        MusicPlayer.Instance.PlayMusic();
    }

    public void Awake()
    {
        backToMenuButton.onClick.AddListener(() => {
            AudioManager.Instance.PlayMenu();
            SceneManager.LoadSceneAsync("MainMenu");
        }
        );
    }
}
