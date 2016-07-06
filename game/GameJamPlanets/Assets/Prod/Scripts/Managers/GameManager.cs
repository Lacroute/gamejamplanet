using UnityEngine;
using System.Collections;

	/// <summary>
	/// Singleton Pattern pour Gamanager
	/// Init Game (via LevelManager)
	/// </summary>

	public class GameManager : MonoBehaviour
	{
		//Static instance du GameManager pour le rendre facilement accessible depuis n'importe quel script.
		public static GameManager GM_instance = null;
		// Déclaration du levelManager qui s'occupera d'instantier le level.
		public LevelManager LevelScript;
		public StatePatternGame GameStateScript;

		void Awake()
		{
			
			// Pattern Singleton toujours à mettre en place pour les managers. 
			// Il ne doit jamais y avoir plus d'1 seul Manager de même type.
			// 


			// Check si y'a déjà une Instance
			if (GM_instance == null)

				//if not, ce script est l'instance, set GM_instance to this.
				GM_instance = this;

			// Sinon, si y'a déjà une instance autre que celle-ci.
			else if (GM_instance != this)

				// Destroy this.
				Destroy(gameObject);    

			// On s'assure que le GO contenant le GM n'est pas détruit en cas de chargement.
			DontDestroyOnLoad(gameObject);

		// Le GM est dans la place, on peut intitialisé le jeu.
			InitGame();
		}
		
		void InitGame()
		{

			// Set Up de la scène
			// Mais on peut lui passer l'état du joueur (à savoir s'il a déjà joué ou non
			// Pour skip la sélection de couleur et l'intro par exemple.
			LevelScript.SetupScene();

		}
		
	}