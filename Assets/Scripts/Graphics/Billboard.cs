using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mainCamera;
    public float rotation = 0f;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (mainCamera == null) return;

        Quaternion targetRotation = Quaternion.Euler(-mainCamera.transform.rotation.eulerAngles.x, 90f, rotation);
        transform.rotation = targetRotation;
    }
}
