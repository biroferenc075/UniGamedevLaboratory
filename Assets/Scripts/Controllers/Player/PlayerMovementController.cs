using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private int targetRoll = 0;
    private int currentRoll = 0;
    [SerializeField] int topRollValue = 3;
    [SerializeField] float topRollAngle = 15f;

    private int targetPitch = 0;
    private int currentPitch = 0;

    [SerializeField] int topPitchValue = 2;
    [SerializeField] float topPitchAngle = 10f;
    [SerializeField] int sidePitchValueY = 2;
    [SerializeField] int sidePitchValueZ = 2;
    [SerializeField] float sidePitchAngle = 10f;

    [SerializeField] float boundsPosX = 0f;
    [SerializeField] float boundsNegX = 0f;

    [SerializeField] float boundsPosY = 0f;
    [SerializeField] float boundsNegY = 0f;

    [SerializeField] float boundsPosZ = 0f;
    [SerializeField] float boundsNegZ = 0f;


    [SerializeField] float moveSpeed = 5f;

    private Animator thrusterAnimator = null;

    // Start is called before the first frame update
    void Start()
    {
        IEnumerator coroutine = rotateShip();
        StartCoroutine(coroutine);

        thrusterAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;

        if(GameStateManager.Instance.isTopView())
        {
            bool isTurbo = false;
            if (Input.GetAxis("Horizontal") < 0f)
            {
                targetRoll = topRollValue;
            }
            else if (Input.GetAxis("Horizontal") > 0f)
            {
                targetRoll = -topRollValue;
                
            }
            else
            {
                targetRoll = 0;
            }

            if (Input.GetAxis("Vertical") < 0f)
            {
                targetPitch = topPitchValue;
            }
            else if (Input.GetAxis("Vertical") > 0f)
            {
                targetPitch = -topPitchValue;
                isTurbo = true;
            }
            else
            {
                targetPitch = 0;
            }

            thrusterAnimator.SetBool("isTurbo", isTurbo);
            transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * dt,0f, Input.GetAxis("Vertical") * moveSpeed * dt);
        } else
        {
            bool isTurbo = false;
            targetPitch = 0;

            if (Input.GetAxis("Vertical") < 0f)
            {
                targetPitch+= sidePitchValueY;
            }
            else if (Input.GetAxis("Vertical") > 0f)
            {
                targetPitch -= sidePitchValueY;
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                targetPitch += sidePitchValueZ;
            }
            else if (Input.GetAxis("Horizontal") > 0f)
            {
                targetPitch -= sidePitchValueZ;
                isTurbo = true;
            }

            thrusterAnimator.SetBool("isTurbo", isTurbo);
            transform.position += new Vector3(0f, Input.GetAxis("Vertical") * moveSpeed * dt, Input.GetAxis("Horizontal") * moveSpeed * dt);

        }

        transform.position = new Vector3(
               Mathf.Clamp(transform.position.x, boundsNegX, boundsPosX),
               Mathf.Clamp(transform.position.y, boundsNegY, boundsPosY),
               Mathf.Clamp(transform.position.z, boundsNegZ, boundsPosZ));
    }

    IEnumerator rotateShip()
    {
        while(true)
        {
            if(targetRoll - currentRoll > 0)
            {
                currentRoll++;
            } else if (targetRoll - currentRoll < 0)
            {
                currentRoll--;
            }

            if (targetPitch - currentPitch > 0)
            {
                currentPitch++;
            }
            else if (targetPitch - currentPitch < 0)
            {
                currentPitch--;
            }

            gameObject.transform.rotation = Quaternion.AngleAxis(topRollAngle * currentRoll, Vector3.forward) * Quaternion.AngleAxis(-topPitchAngle * currentPitch, Vector3.right);

            yield return new WaitForSeconds(0.125f);
        }
    }

    private Coroutine moveCoroutine;

    public void StartMovingToCenter()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        if(GameStateManager.Instance.isTopView())
        {
            moveCoroutine = StartCoroutine(MoveToXZero());
        } else
        {
            moveCoroutine = StartCoroutine(MoveToYZero());
        }
    }

    
    private IEnumerator MoveToYZero()
    {
        while (Mathf.Abs(transform.position.y) > 0f)
        {
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.MoveTowards(transform.position.y, 0f, moveSpeed * Time.deltaTime);

            transform.position = newPosition;

            yield return null;
        }
        targetPitch = 0;
        moveCoroutine = null;
    }

    private IEnumerator MoveToXZero()
    {
        while (Mathf.Abs(transform.position.x) > 0f)
        {
            
            Vector3 newPosition = transform.position;
            newPosition.x = Mathf.MoveTowards(transform.position.x, 0f, moveSpeed * Time.deltaTime);

            transform.position = newPosition;

            yield return null;
        }
        targetRoll = 0;
        moveCoroutine = null;
    }

    public void ToggleView()
    {
        StartMovingToCenter();
    }
}
