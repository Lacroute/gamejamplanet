using UnityEngine;
using System.Collections;

public class AsteroidAnimation : MonoBehaviour {
	public Vector3 Asteroid_Pivot;
	public float Asteroid_Speed = 0.07F;
	public int asteroidMinSize;
	public int asteroidMaxSize;
	public float asteroidMinHeight;
	public float asteroidMaxHeight;
	// Use this for initialization
	void Start () {
		float randomY = Random.Range (asteroidMinHeight,asteroidMaxHeight);
		int randomSize = Random.Range (asteroidMinSize,asteroidMaxSize);
		transform.localScale = new Vector3 (randomSize,randomSize,randomSize);
		Asteroid_Pivot = GameObject.FindGameObjectWithTag ("Planet").transform.position;
		transform.position = new Vector3 (transform.position.x,transform.position.y + randomY,transform.position.z);

	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (Asteroid_Pivot,Vector3.up,Asteroid_Speed);

}

}
