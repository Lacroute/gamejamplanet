using UnityEngine;
using System.Collections;

public class DefaultPlanetState : IPlanetState {

	private readonly StatePatternPlanet planet;

	public DefaultPlanetState(StatePatternPlanet statePatternPlanet){
		this.planet = statePatternPlanet;
	}

	public void UpdateState(){
		Debug.Log ("Updating defaultState");
	}
		
	public void ToDefaultState(){
		Debug.Log ("Can't transition to same state");
	}
}
