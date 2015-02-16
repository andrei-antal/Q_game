using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;

	private float skin = .005f;

	Ray ray;
	RaycastHit hit;

	[HideInInspector]
	public bool grounded;

	[HideInInspector]
	public bool movementStopped;

	void Start() {
		collider = GetComponent<BoxCollider>();
		size = collider.size;
		center = collider.center;
	}

	public void Move(Vector2 moveAmount)
	{
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 playerPosition = transform.position;

		// top and bottom collision
		grounded = false;
		for(int i=0; i<3; i++)
		{
			float moveDirection  = Mathf.Sign(deltaY);
			float x = (playerPosition.x + center.x - size.x/2) + (size.x/2 * i);
			float y = (playerPosition.y + center.y + size.y/2 * moveDirection);

			ray = new Ray(new Vector2(x,y), new Vector2(0,moveDirection));

			Debug.DrawRay(ray.origin, ray.direction);

			if(Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + skin, collisionMask))
			{
				float hitDistance = Vector3.Distance(ray.origin, hit.point);

				if(hitDistance > skin)
				{
					deltaY = (hitDistance - skin) * moveDirection;
				}
				else
				{
					deltaY = 0;
				} 
				grounded = true;
				break;
			}

		}  

		// left and right collision
		movementStopped = false;
		for(int i=0; i<3; i++)
		{
			float moveDirection  = Mathf.Sign(deltaX);
			float x = (playerPosition.x + center.x + size.x/2 * moveDirection);
			float y = (playerPosition.y + center.y - size.y/2) + (size.y/2 * i);
			
			ray = new Ray(new Vector2(x,y), new Vector2(moveDirection, 0));
			
			Debug.DrawRay(ray.origin, ray.direction);
			
			if(Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + skin, collisionMask))
			{
				float hitDistance = Vector3.Distance(ray.origin, hit.point);
				
				if(hitDistance > skin)
				{
					deltaX = (hitDistance - skin) * moveDirection;
				}
				else
				{
					deltaX = 0;
				}
				movementStopped = true;
				break;
			}
			
		}  
		// angle collision
		if(!grounded && !movementStopped)
		{
			Vector3 playerDirection = new Vector3(deltaX, deltaY);
			Vector3 orientation = new Vector3(playerPosition.x + center.x + size.x/2 * Mathf.Sign(deltaX), (playerPosition.y + center.y + size.y/2 * Mathf.Sign(deltaY)));
			
			Debug.DrawRay(orientation,playerDirection.normalized);
			ray = new Ray(orientation, playerDirection.normalized);
			
			if(Physics.Raycast(ray,Mathf.Sqrt(deltaX*deltaX + deltaY*deltaY)))
			{
				grounded = true;
				deltaY = 0;
			}
		}



		Vector2 finalTransform = new Vector2(deltaX, deltaY);
		transform.Translate(finalTransform);
	}
}
