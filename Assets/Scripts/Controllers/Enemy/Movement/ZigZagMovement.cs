using System;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;


[CreateAssetMenu(fileName = "ZigZagStrategy", menuName = "Shmup/MovementStrategy/ZigZag")]
public class ZigZagMovement : MovementStrategy
{
    [SerializeField] float freq = 1.0f;
    [SerializeField] float speed = 100f;

    [SerializeField] Vector3 position1;
    [SerializeField] Vector3 position2;

    private Vector3 initialPosition;
    [NonSerialized]
    private bool init = false;
    private bool arrived = false;
    private float time;

    public override void Move(Transform transform)
	{
        if(!init)
        {
            initialPosition = transform.position;
            init = true;
            arrived = false;
            time = 0;
        }

        if(arrived)
        {
            time += Time.deltaTime;

            float coeff = Mathf.Cos(time * freq);

            transform.position = coeff * position1 + (1 - coeff) * position2;
        } else
        {
            Vector3 diff = position1 - transform.position;
            float dist = diff.magnitude;

            if(dist > speed * Time.deltaTime)
            {
                transform.position += diff.normalized * speed * Time.deltaTime;
            } else
            {
                transform.position = position1;
                arrived = true;
            } 
        }
    }
}