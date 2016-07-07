using UnityEngine;
using System.Collections;

public class StatePatternPlanet : MonoBehaviour {

	[HideInInspector] public IPlanetState currentState;
	[HideInInspector] public DefaultPlanetState defaultPlanetState;

	// Awake s'effectue à la création de l'object sur la scène, AVANT Start().
	private void Awake(){
		defaultPlanetState = new DefaultPlanetState (this);
	}

	// Use this for initialization
	void Start () {
		currentState = defaultPlanetState;
	}
	
	// Update is called once per frame
	void Update () {
		currentState.UpdateState ();
	}
}
