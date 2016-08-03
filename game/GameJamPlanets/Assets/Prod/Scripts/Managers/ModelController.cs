using UnityEngine;
using System.Collections;
using System.IO;

// Special class for json library mapping.
public class PlayerDBModel
{
	public int id;
	public string rgba;
	public bool message_sent;
	public int message_count;
	public int sharing_id;
}


public class RecordDBModel{
	public int id;
	public string data;
	public int history;
	public int author_id;
}


public class ModelController : MonoBehaviour {

	private const string BASE_URL = "http://record-gamejam.rhcloud.com/api/";
	private const string LOCAL_DATA_PATHFILE = "data.json";

	private Player player;
	public Record tmp_record;

	// Init.
	void Start () {
		initPlayer ();
	}


	void Update(){

	}
		

	/////////////////////
	//    COROUTINE    //
	//     WRAPPERS    //
	/////////////////////


	public void initPlayer(){
		StartCoroutine( FindMeAndMyRecord() );
	}



	public IEnumerator FindMeAndMyRecord(){
		print( "Find or Create player. t=" + Time.time );
		yield return StartCoroutine( findMe() );
		print( "Find his record. t=" + Time.time );
		yield return StartCoroutine( findMyRecord() );
		print( "Data cached. t=" + Time.time );
	}



	////////////////////
	//    DATABASE    //
	//    REQUESTS    //
	////////////////////


	/// <summary>
	/// Finds me. If no player is set, then read the local data first.
	/// </summary>
	/// <returns>Set me.</returns>
//	IEnumerator findMe(System.Action<Player> callback) {
	IEnumerator findMe() {
		if (player == null) {
			getLocalData ();
		} else {
			WWW request = buildRequest ("Players/" + player.Id);
			yield return request;

			if (request.error == null) {
				// Build the player
				player = new Player (JsonUtility.FromJson<PlayerDBModel> (request.text));
				Debug.Log (string.Format ("** Me > {0}", player.ToString ()));
			} else {
				Debug.Log (string.Format ("** ERROR Request: {0}", request.text));
			}
		}
	}


	/// <summary>
	/// Finds my record.
	/// </summary>
	/// <returns>Set my record.</returns>
	IEnumerator findMyRecord() {
		WWW request = buildRequest ("Records/findOne?filter={\"where\":{\"author_id\":"+ player.Id + "}}");
		yield return request;

		if (request.error == null) 
		{
			// Build my Record and stick it to me.
			player.MyRecord = new Record (JsonUtility.FromJson<RecordDBModel> (request.text));
			Debug.Log(string.Format("** Me > {0}", player.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		}
	}


	/// <summary>
	/// Listens to the space.
	/// </summary>
	/// <returns>Set the tmp_record.</returns>
	IEnumerator listenToSpace() {
		WWW request = buildRequest ("Players/" + player.Id + "/listenToSpace");
		yield return request;

		if (request.error == null) 
		{
			// Get the first string representation of complex JSONObject.
			string first_element = getFirstFromJSONString(request.text);
			this.tmp_record = new Record (JsonUtility.FromJson<RecordDBModel> (first_element));

			Debug.Log(string.Format("** Record from_space > {0}", this.tmp_record.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		} 
	}


	/// <summary>
	/// Shares the record, update the bdd.
	/// </summary>
	/// <returns>The shared record.</returns>
	/// <param name="id_new_record">Identifier new record.</param>
	IEnumerator shareRecord(int id_new_record) {

		WWWForm form = new WWWForm();
		form.AddField("id_new_record", id_new_record);

		WWW request = buildRequest ("Players/" + player.Id + "/shareRecord", form);
		yield return request;

		if (request.error == null) 
		{
			string first_element = getFirstFromJSONString(request.text);

			// Update me.
			Player me_updated = new Player (JsonUtility.FromJson<PlayerDBModel> (first_element));
			player.SharingId = me_updated.SharingId;
			Debug.Log(string.Format("** Player me updated > {0}", player.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		} 
	}


	/// <summary>
	/// Creates the player.
	/// </summary>
	/// <returns>The player.</returns>
	/// <param name="rgba">Rgba.</param>
	IEnumerator createPlayer(Color rgba) {
		string[] rgba_strings = new string[] {
			rgba.r.ToString ("0.0000"),
			rgba.g.ToString ("0.0000"),
			rgba.b.ToString ("0.0000"),
			rgba.a.ToString ("0.0000")
		}; 

		WWWForm form = new WWWForm();
		form.AddField("rgba", string.Join(",", rgba_strings));

		WWW request = buildRequest ("Players/", form);
		yield return request;

		if (request.error == null) 
		{
			player = new Player (JsonUtility.FromJson<PlayerDBModel> (request.text));
			Debug.Log(string.Format("** Me > {0}", player.ToString()));
			// Write file
			using (FileStream fs = new FileStream(LOCAL_DATA_PATHFILE, FileMode.Create)){
				using (StreamWriter writer = new StreamWriter(fs)){
					writer.Write(request.text);
				}
			}

		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		} 
	}



	/// <summary>
	/// Create the record.
	/// </summary>
	/// <returns>The record.</returns>
	/// <param name="data">Data.</param>
	IEnumerator createRecord(string data) {
		WWWForm form = new WWWForm();
		form.AddField("data", data);

		WWW request = buildRequest ("Players/" + player.Id + "/author", form);
		yield return request;

		if (request.error == null) {
			player.MyRecord = new Record (JsonUtility.FromJson<RecordDBModel> (request.text));
			Debug.Log(string.Format("** MyRecord created > {0}", player.ToString()));
		} else {
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		}
	}


	///////////////////
	//    HELPERS    //
	///////////////////
 


	/// <summary>
	/// Gets the local data.
	/// </summary>
	/// <param name="path">Path of data.json</param>
	public void getLocalData(string path = LOCAL_DATA_PATHFILE){
		try {  
			Debug.Log("Reading local data...");

			StreamReader sr = new StreamReader(LOCAL_DATA_PATHFILE);
			string local_data = sr.ReadToEnd();
			sr.Close();

			player = new Player(JsonUtility.FromJson<PlayerDBModel>(local_data));
			Debug.Log(string.Format("** Player me > {0}", player.ToString()));
		} catch (FileNotFoundException e) {
			Debug.Log ("** No local data found, creating a new Player...");

			// Create a new Player with random rgba string
			StartCoroutine(
				createPlayer(Random.ColorHSV())
			);

		} catch (IOException e) {  
			Debug.Log(e.ToString());  
		}
	}


	/// <summary>
	/// Builds the request.
	/// </summary>
	/// <returns>The request.</returns>
	/// <param name="url">API's URL.</param>
	/// <param name="form">Form (optional)</param>
	public WWW buildRequest(string url, WWWForm form = null){
		url = BASE_URL + url;
		Debug.Log(string.Format("** Request URL : {0}", url));
		WWW request = new WWW(url);

		if (form != null) {
			request = new WWW(url, form);
		}
			
		return request;
	}


	/// <summary>
	/// Gets the first element from JSON string.
	/// </summary>
	/// <returns>The first JSON string.</returns>
	/// <param name="json">Json.</param>
	public string getFirstFromJSONString(string json){
		JSONObject result = new JSONObject (json);
		result = (JSONObject)result.list [0];
		return result.Print();
	}


	/// <summary>
	/// Gets the player.
	/// </summary>
	/// <value>The player.</value>
	public Player Player{
		get { return this.player;}
	}


	/// <summary>
	/// Player sent his message ?
	/// </summary>
	/// <returns><c>true</c>, if sent was sent, <c>false</c> otherwise.</returns>
	public bool isSent(){
		bool is_sent = false;
		if (Player == null) return is_sent;

		return (player.MyRecord != null);
	}
}
