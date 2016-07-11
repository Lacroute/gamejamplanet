using UnityEngine;
using System.Collections;

public interface IGameState
{
	void Start ();

	void UpdateState();

	void ToSplashState();

	void ToIntroState();

	void ToInGameState();

}