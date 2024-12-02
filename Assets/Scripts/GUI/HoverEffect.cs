using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    [SerializeField] private float offset = 1.0f;
    [SerializeField] private float timeValue = 1.0f;

    private Vector3 initialPosition;
    private bool movingUp = true;
    private float timer;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeValue)
        {
            timer = 0f;
            movingUp = !movingUp;

            Vector3 targetPosition = movingUp
                ? initialPosition + Vector3.up * offset
                : initialPosition - Vector3.up * offset;

            transform.position = targetPosition;
        }
    }
}
