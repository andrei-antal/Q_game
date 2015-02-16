using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	private Transform target;
	private float trackSpeed = 10;

	public void SetTarget(Transform t)
	{
		target = t;
	}

	public void LateUpdate()
	{
		if(target)
		{
			float x = IncrementTwoards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTwoards(transform.position.y, target.position.y, trackSpeed);

			transform.position = new Vector3(x,y,transform.position.z);
		}
	}

	private float IncrementTwoards(float currentSpeed, float targetSpeed, float acceleration)
	{
		if (currentSpeed == targetSpeed) 
		{
			return currentSpeed;
		}
		else
		{
			float dir = Mathf.Sign(targetSpeed - currentSpeed);
			currentSpeed += acceleration * Time.deltaTime * dir;
			return (dir == Mathf.Sign(targetSpeed - currentSpeed)) ? currentSpeed : targetSpeed;
			
		}
	}
}
