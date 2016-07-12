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

	private const string BASE_URL = "http://0.0.0.0:3000/api/";
	private const string LOCAL_DATA_PATHFILE = "data.json";

	private Player player;
	public Record tmp_record;



	// Init.
	void Start () {
		Debug.Log ("START");
		StartCoroutine(findMe());
		Debug.Log (string.Format("END"));
//		StartCoroutine(findMyRecord());
//		StartCoroutine(listenToSpace());
//		StartCoroutine(shareRecord(2));
//		getLocalData();
	}


	// 
	void Update(){
//		if (player == null) {
//			Debug.Log ("not loaded yet.");
//		} else {
//			Debug.Log ("PLAYER LOADED !");
//		}
	}


	// Helper to build clean URL.
	public WWW buildRequest(string url, WWWForm form = null){
		Debug.Log ("buildRequest");
		url = BASE_URL + url;
		Debug.Log(string.Format("** Request URL : {0}", url));
		WWW request = new WWW(url);

		if (form != null) {
			request = new WWW(url, form);
		}
			
		return request;
	}


	// Get the first object from complex JSON string.
	public string getFirstFromJSONString(string json){
		JSONObject result = new JSONObject (json);
		result = (JSONObject)result.list [0];
		return result.Print();
	}


	// Find me.
	IEnumerator findMe() {
		Debug.Log ("FindMe");
		int the_id = 1;
		if (player != null) {
			the_id = player.Id;
		}
		WWW request = buildRequest ("Players/" + the_id);
		yield return request;

		if (request.error == null) 
		{
			// Build the player
			player = new Player(JsonUtility.FromJson<PlayerDBModel>(request.text));
			Debug.Log(string.Format("** Me > {0}", player.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		}
		yield return null;
	}


	// Find my record.
	IEnumerator findMyRecord() {
		WWW request = buildRequest ("Records/findOne?filter={\"where\":{\"id\":1}}");
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


	// TODO Handle the record.
	// Get a random record.
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


	// Update Database with a new shared record.
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


	// Create a new player.
	IEnumerator createPlayer(Color rgba) {
		// 
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
			Debug.Log(string.Format("** Player me > {0}", player.ToString()));
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


	// Read local data
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


	// Access to the player.
	public Player Player{
		get { return this.player;}
	}
}
