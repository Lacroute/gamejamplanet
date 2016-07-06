using UnityEngine;
using System.Collections;

public class Title_Movement : MonoBehaviour {
	
	private Vector3 Title_Target;
	public float TitleMovement_Speed;

	public void Start(){
		Title_Target = GameObject.FindGameObjectWithTag ("Title_Target").transform.position;
	}

	public void Update(){

		if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<StatePatternGame>().currentState.ToString() == "SplashState" ){
			transform.position = Vector3.Lerp (transform.position,Title_Target,TitleMovement_Speed * 0.001F);
		}

	}

}
