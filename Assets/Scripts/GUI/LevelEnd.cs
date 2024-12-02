using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI enemiesText;

    [SerializeField]
    public TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject[] grades;

    [SerializeField]
    Button continueButton;

    [SerializeField]
    GameObject loadingParent;

    [SerializeField]
    string nextScene;

    public void Awake()
    {
        continueButton.onClick.AddListener(() => Continue());
    }


    public int getGradeIndex()
    {
        int sum = 0;

        float percent = (float) GameStateManager.Instance.GetKilledEnemies() / GameStateManager.Instance.GetTotalEnemies();
        
        if (percent > 0.5f)
        {
            sum++;
        }

        if (percent > 0.8f)
        {
            sum++;
        }

        float avgMultiplier = (float)GameStateManager.Instance.GetScore() / GameStateManager.Instance.GetRawScore();

        if (avgMultiplier > 3.5f)
        {
            sum++;
        }

        return sum;
    }

    public IEnumerator Show()
    {
        gameObject.SetActive(true);
        MusicPlayer.Instance.StopMusic();
        enemiesText.text = $"{Math.Min(GameStateManager.Instance.GetKilledEnemies(), GameStateManager.Instance.GetTotalEnemies()):D3}/{GameStateManager.Instance.GetTotalEnemies():D3}";
        scoreText.text = $"{GameStateManager.Instance.GetScore():D6}";

        yield return new WaitForSeconds(1.5f);

        enemiesText.gameObject.SetActive(true);
        AudioManager.Instance.PlaySlam();

        yield return new WaitForSeconds(1.5f);

        scoreText.gameObject.SetActive(true);
        AudioManager.Instance.PlaySlam();

        yield return new WaitForSeconds(1.5f);

        AudioManager.Instance.PlaySlam();
        grades[getGradeIndex()].SetActive(true);
    }

    private void Continue()
    {
        AudioManager.Instance.PlayMenu();
        SceneManager.LoadSceneAsync(nextScene);
        loadingParent.SetActive(true);
    }
}
