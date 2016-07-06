using UnityEngine;
using System.Collections;

// Script qui instantit et fait bouger le tire du jeu sur la splashscreen. 
public class TitleScript : MonoBehaviour {

	public GameObject P_Title_Spawn_GO;
	public GameObject P_Title_Target_GO;
	private Vector3 Title_Target;
	public float TitleMovement_Speed;

	public void Start(){
		Title_Target = GameObject.FindGameObjectWithTag ("TargetP_Title").transform.position;

	}

	public void Update(){

		if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatePatternGame>().currentState.ToString() == "SplashState" ){
			transform.position = Vector3.Lerp (transform.position,Title_Target,TitleMovement_Speed * 0.001F);
		}

	}

}
