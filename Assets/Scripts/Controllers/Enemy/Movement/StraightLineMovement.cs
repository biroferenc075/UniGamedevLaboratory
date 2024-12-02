using UnityEditor.Rendering;
using UnityEngine;


[CreateAssetMenu(fileName = "MoveInStraightLineStrategy", menuName = "Shmup/MovementStrategy/InStraightLine")]
public class StraightLineMovement : MovementStrategy
{
	[SerializeField]
	Vector3 dir;
	[SerializeField]
	float speed;

    public override void Move(Transform transform)
	{
		transform.position += dir * speed * Time.deltaTime;
	}
}