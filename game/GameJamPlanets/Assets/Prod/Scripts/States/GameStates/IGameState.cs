using UnityEngine;
using System.Collections;

public interface IGameState 
{
	void Start ();

	void UpdateState();

	void DoBeforeEntering();

	void DoBeforeLeaving();

	void ToSplashState();

	void ToIntroState();

	void ToViewPlanetState();

	void ToRecordingPlanetState();

	void ToListeningRecordState();

	void ToSendingRecordState();


}