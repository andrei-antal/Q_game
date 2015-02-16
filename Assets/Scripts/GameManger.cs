using UnityEngine;
using System.Collections;

public class GameManger : MonoBehaviour {

	public GameObject player;
	private GameCamera camera;

	private void SpawnPlayer() 
	{
		camera.SetTarget((Instantiate(player,new Vector2(0,0),Quaternion.identity) as GameObject).transform);
	}

	// Use this for initialization
	void Start () {
		camera = GetComponent<GameCamera>();
		SpawnPlayer();
	}
}
