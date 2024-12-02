using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected int scoreValue = 50;

    [SerializeField] float fallSpeed = 1.0f;
    [SerializeField] float floatAmplitude = 1.0f;
    [SerializeField] float floatFrequency = 1.0f;

    public void OnCollisionEnter(Collision collision)
    {
        ItemEffect(collision);
        GameStateManager.Instance.ItemPickedUp(scoreValue);
        AudioManager.Instance.PlayPickup();
        Destroy(gameObject);
    }

    protected abstract void ItemEffect(Collision collision);

    private Vector3 initialPosition; 
    private float time;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;

        float xOffset = Mathf.Cos(time * floatFrequency) * floatAmplitude;
        float yOffset = -fallSpeed * time;

        transform.position = initialPosition + (GameStateManager.Instance.isTopView() ? new Vector3(xOffset, 0, yOffset) : new Vector3(0, xOffset, yOffset));
    }
}