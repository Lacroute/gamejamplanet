using UnityEngine;
using System.Collections;

public class AsteroidInstantiation : MonoBehaviour {
	// Prefab des astéroides
	public GameObject AsteroidPrefab;
	// Nombre d'astéroides
	public int numObjects = 30;
	// Rayon du cercle sur lequel les astéroides sont dispercés à l'instantiation
	public float rayonAsteroides;
	// Le centre autour duquel chaque astéroide tourne
	public Vector3 Asteroid_Pivot;
	// Vitesse de rotation de l'astéroide autour du pivot (la planète)
	public float Asteroid_Speed = 0.07F;
	// Tailles de l'asétéroide;
	public int asteroidMinSize;
	public int asteroidMaxSize;
	// Répartition verticale de l'astéroide dans le cercle (pour qu'ils ne soient pas tous à la même hauteur)
	public float asteroidMinHeight;
	public float asteroidMaxHeight;

	void Start() {
		// Définition du centre de rotation des astéroides.
		GameObject center = GameObject.FindGameObjectWithTag ("Planet");

		for (int i = 0; i < numObjects; i++){

			// Création du cercle sur lequel les astéroides sont ensuite dispercés
			Vector3 pos = RandomCircle(center.transform.position, rayonAsteroides);
			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center.transform.position-pos);

			// Instantiation d'un astéroide et positionnement sur le cercle
			GameObject AsteroidInstance = Instantiate(AsteroidPrefab, pos, rot) as GameObject;
			// Attribution d'un parent (l'objet autour duquel on le fait tourner)
			AsteroidInstance.transform.parent = center.transform;

			// Attribution d'un size aléatoire.
			int randomSize = Random.Range (asteroidMinSize,asteroidMaxSize);
			AsteroidInstance.transform.localScale = new Vector3 (randomSize,randomSize,randomSize);
		}
	}

	// Création du cercle de dispertion des astéroides.
	Vector3 RandomCircle ( Vector3 center ,   float radius  ){
		float randomY = Random.Range (asteroidMinHeight,asteroidMaxHeight);
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
		pos.y = center.y + randomY;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos; 
	}
}