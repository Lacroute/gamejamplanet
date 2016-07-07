using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameManager_old : MonoBehaviour {
	

	//UI
	public InputField inputfield;
	public Button introButton;
	public Button sondeButton;
	public Button acceptButton;
	public Button declineButton;
	public bool boolButton;

	public GameObject myIdeaCapsule;
	public GameObject ideaCapsuleOther;

	public gameState current_game_state;

	public enum request
	{
		FindExistingPlayer,
		FindPendingMessages,
		PostMessage,
	}

	public enum gameState
	{
		splashScreen,
		writeMessage,
		freeMessage,
		waitingSonde,
		sonding,
		acceptIdea,
		declineIdea,
		ideaHost,


		initializationGame,
		introRookie,
		gameStarted,
	}


	public void setGameState(gameState gs)
	{
		current_game_state = gs;

		if (current_game_state == gameState.splashScreen) 
		{
			
		}
		else if (current_game_state == gameState.writeMessage) 
		{
			// TODO Record
		}
		else if (current_game_state == gameState.freeMessage) 
		{
			myIdeaCapsule.SetActive (true);
			GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("freeMyCapsule");
			setGameState (gameState.waitingSonde);
		}
		else if (current_game_state == gameState.waitingSonde) 
		{
			
		}
		else if (current_game_state == gameState.sonding) 
		{
			ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ().enabled = true;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<DepartMessage> ().enabled = false;
			ideaCapsuleOther.transform.position = new Vector3 (0, 0, 0);
			ideaCapsuleOther.SetActive (true);
			GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("sonding");
		}
		else if (current_game_state == gameState.declineIdea) 
		{
			Debug.Log (ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ());
			ideaCapsuleOther.transform.GetChild (0).GetComponent<ArriveeMessage> ().messageHasReachedTarget = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ().enabled = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<DepartMessage> ().enabled = true;
			setGameState (gameState.waitingSonde);
		}
		else if (current_game_state == gameState.acceptIdea) 
		{
			ideaCapsuleOther.transform.GetChild (0).GetComponent<ArriveeMessage> ().messageHasReachedTarget = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<ArriveeMessage> ().enabled = false;
			ideaCapsuleOther.transform.GetChild(0).GetComponent<DepartMessage> ().enabled = true;
			setGameState (gameState.ideaHost);
		}

		else if (current_game_state == gameState.gameStarted) 
		{
			
		}
		Debug.Log ("NewState: " + current_game_state);
	}


	public void startGameButton(){

			GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("clickButton");
			setGameState (gameState.writeMessage);
		
	}


	public void sondeButtonAction()
	{
		Debug.Log ("click sonde");
		setGameState (gameState.sonding);
	}


	public void acceptButtonAction()
	{
		Debug.Log ("accept");
		GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("acceptIdeaWhenNoIdea");
		setGameState (gameState.acceptIdea);
	}


	public void declineButtonAction()
	{
		Debug.Log ("decline");
		GameObject.Find ("Game_Title").GetComponent<Animator> ().SetTrigger ("declineIdeaWhenNoIdea");
		setGameState (gameState.declineIdea);
	}
		



//####################### Methode native unity ###########################

	// Use this for initialization
	void Start () {

		ideaCapsuleOther = GameObject.Find ("MessageHolder");
		ideaCapsuleOther.SetActive(false);

		myIdeaCapsule = GameObject.Find ("MyRecordLaunch");
		myIdeaCapsule.SetActive(false);

		inputfield = GameObject.Find ("Game_Title").transform.GetChild(0).transform.GetChild(0).GetComponent<InputField>();
		Debug.Log (inputfield);

		//on lance le jeu, state = initializationGame 
		setGameState(gameState.splashScreen);

	}


	void Update (){
		if(current_game_state == gameState.splashScreen){

			if(Input.GetMouseButtonDown(0)){
				GameObject.Find("Game_Title").GetComponent<Animator> ().SetBool ("Title_Disappear",true);
			}

			introButton.onClick.RemoveAllListeners();
			introButton.onClick.AddListener (startGameButton);
		}

		if(current_game_state == gameState.waitingSonde){

			sondeButton.onClick.RemoveAllListeners();
			sondeButton.onClick.AddListener (sondeButtonAction);
		}


		if(current_game_state == gameState.sonding){

			if (ideaCapsuleOther.transform.GetChild (0).GetComponent<ArriveeMessage> ().messageHasReachedTarget) 
			{
				acceptButton.onClick.RemoveAllListeners();
				acceptButton.onClick.AddListener (acceptButtonAction);
			
				declineButton.onClick.RemoveAllListeners();
				declineButton.onClick.AddListener (declineButtonAction);
			}

		}

	}

}
