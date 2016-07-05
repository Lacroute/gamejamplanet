using UnityEngine;
using System.Collections;

	public class GameManager : MonoBehaviour
	{
		//Static instance du GameManager pour le rendre facilement accessible depuis n'importe quel script.
		public static GameManager GM_instance = null;
		// Déclaration du levelManager qui s'occupera d'instantier le level.
		private LevelManager LevelScript;

		void Awake()
		{
			
			// Pattern Singleton toujours à mettre en place pour les managers. 
			// Il ne doit jamais y avoir plus d'1 seul Manager de même type.


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

			// On pécho l'instant du LevelManager.
			LevelScript = GetComponent<LevelManager>();

		// Le GM est dans la place, on peut intitialisé le jeu.
			InitGame();
		}
		
		void InitGame()
		{
			// Pour l'insatnt on call Setup Scene sans paramètre.
			// Mais on peut lui passer l'état du joueur (à savoir s'il a déjà joué ou nou
			// Pour skip la sélection de couleur et l'intro.
			LevelScript.SetupScene();

		}
	}