using UnityEngine;

public class HealthGUI : MonoBehaviour
{
    [SerializeField]
    public GameObject heart1;

    [SerializeField]
    public GameObject heart2;

    [SerializeField]
    public GameObject heart3;


    private Player player;
    public void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public void Update()
    {
        switch (player.GetHealth()) // this is very ugly, but alas
        {
            case 0:
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 1:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 2:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                break;
            case 3:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                break;
        }
    }
}
