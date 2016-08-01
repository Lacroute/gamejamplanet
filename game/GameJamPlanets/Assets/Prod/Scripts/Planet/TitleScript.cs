using UnityEngine;
using System.Collections;

// Script qui instantit et fait bouger le tire du jeu sur la splashscreen. 
public class TitleScript : MonoBehaviour {

	public GameObject P_Title_Spawn_GO;
	public GameObject P_Title_Target_GO;
	private Vector3 V_Title_Target;
	public float titleMovement_Speed;
	public bool introIsFinished = false;

	public void Start(){
		V_Title_Target = GameObject.FindGameObjectWithTag ("TargetP_Title").transform.position;

	}

	public void Update(){
			transform.position = Vector3.Lerp (transform.position,V_Title_Target,titleMovement_Speed * 0.001F);
	}

	public void OnTriggerEnter(Collider theCollision){
		if (theCollision.gameObject.tag == "TargetP_Title") {
			introIsFinished = true;
		}
	}

}
