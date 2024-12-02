using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] float distance = 10.0f;
    [SerializeField] float transitionSpeed = 2.0f;  

    // Views (rotation + up vectors)
    private Vector3 abovePosition;   
    private Vector3 sidePosition;    
    private Vector3 targetPosition;  
    private Vector3 targetUp;        


    [SerializeField] bool isAboveView; 
    private bool isTransitioning = false; 

    void Start()
    {
        if (pivot == null)
        {
            Debug.LogWarning("Pivot is not assigned.");
            return;
        }

        
        abovePosition = pivot.position + Vector3.up * distance; 
        sidePosition = pivot.position + Vector3.right * distance; 

        targetPosition = abovePosition;
        targetUp = Vector3.forward; 
        MoveToTargetPosition();
    }

    void Update()
    {
        if (pivot == null) return;

        if (isTransitioning)
        {
            SmoothTransitionToTarget();
        }
    }

    public void ToggleView()
    {
        if (isAboveView)
        {
            targetPosition = sidePosition;
            targetUp = Vector3.up; 
        }
        else
        {
            targetPosition = abovePosition;
            targetUp = Vector3.forward; 
        }

        isAboveView = !isAboveView;
        isTransitioning = true; 
    }

    private void SmoothTransitionToTarget()
    {
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);

        
        Vector3 direction = pivot.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction, targetUp);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

       
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f && Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            isTransitioning = false; 
        }
    }
    private void MoveToTargetPosition()
    {
        transform.position = targetPosition;
        Vector3 direction = pivot.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, targetUp);
    }
}
