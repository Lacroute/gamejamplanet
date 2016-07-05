using UnityEngine;
using System.Collections;

public class StatePatternPlayer : MonoBehaviour 
{

	[HideInInspector] public IPlayerState currentState;
	[HideInInspector] public DefaultState defaultState;
	[HideInInspector] public SearchIdeaState searchIdeaState;
	[HideInInspector] public RecordingIdeaState recordingIdeaState;
	[HideInInspector] public NavMeshAgent navMeshAgent;

	private void Awake()
	{
		defaultState = new DefaultState (this);
		searchIdeaState = new SearchIdeaState (this);
		recordingIdeaState = new RecordingIdeaState (this);

		navMeshAgent = GetComponent<NavMeshAgent> ();
	}

	void Start () 
	{
		currentState = defaultState;
	}

	void Update () 
	{
		currentState.UpdateState ();
	}

	private void OnTriggerEnter(Collider other)
	{
		currentState.OnTriggerEnter (other);
	}
}