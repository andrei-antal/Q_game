using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {

	public float speed = 8;
	public float acceleration = 30;
	public float gravity = 20;
	public float jumpHeight = 10; 

	private float currentSpeed;
	private float targetSpeed;

	private Vector2 amountToMove;

	private PlayerPhysics playerPhysics ;

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	
	// Update is called once per frame
	void Update () {

		if(playerPhysics.movementStopped)
		{
			targetSpeed = 0;
			currentSpeed = 0;
		}

		targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
		currentSpeed = IncrementTwoards(currentSpeed,targetSpeed, acceleration);

		if(playerPhysics.grounded)
		{
			amountToMove.y = 0;

			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = jumpHeight;
			}
		}


		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;




		playerPhysics.Move(amountToMove * Time.deltaTime);



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
