using TMPro;
using UnityEngine;

public class ScoreGUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI scoreText;

    [SerializeField]
    public TextMeshProUGUI multiplierText;

    private GameStateManager stateManager;

    public void Start()
    {
        stateManager = GameStateManager.Instance;
    }

    public void Update()
    {
        scoreText.text = $"{stateManager.GetScore():D6}";
        multiplierText.text = $"X{stateManager.GetMultiplier()}";
    }
}
