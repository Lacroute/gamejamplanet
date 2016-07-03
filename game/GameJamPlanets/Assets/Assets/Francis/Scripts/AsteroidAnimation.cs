using UnityEngine;
using System.Collections;

public class AsteroidAnimation : MonoBehaviour {
	public Vector3 Asteroid_Pivot;
	private int Asteroid_Speed;
	private Vector3 Asteroid_Up;
	private Vector3 Asteroid_Down;
	// Use this for initialization
	void Start () {
		Asteroid_Pivot = GameObject.FindGameObjectWithTag ("Planet").transform.position;
		Asteroid_Speed = Random.Range (1,5);


	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (Asteroid_Pivot,Vector3.up,0.07F);

}

}
