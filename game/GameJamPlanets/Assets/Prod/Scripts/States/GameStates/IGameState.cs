using UnityEngine;
using System.Collections;

public interface IGameState
{
	void UpdateState();

	void ToSplashState();

	void ToIntroState();

	void ToInGameState();

}