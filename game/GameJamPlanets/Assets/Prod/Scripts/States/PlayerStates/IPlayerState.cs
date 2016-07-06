using UnityEngine;
using System.Collections;

public interface IPlayerState
{

	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToDefaultState();

	void ToSearchIdeaState();

	void ToRecordingIdeaState();

}