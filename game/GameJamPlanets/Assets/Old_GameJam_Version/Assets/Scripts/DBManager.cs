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


	public PlayerDBModel(){
		
	}
}


public class RecordDBModel{
	public int id;
	public string data;
	public int history;
	public int author_id;


	public RecordDBModel(){
		
	}
}


public class DBManager : MonoBehaviour {

	// Next step : store info in a local file.
	private int my_id_from_local_base = 1;
	private Player me;
	private const string BASE_URL = "http://0.0.0.0:3000/api/";
	private const string LOCAL_DATA_PATHFILE = "data.json";


	// Access to the player.
	public Player getPlayer(){
		return me;
	}


	// Init.
	void Start () {
//		StartCoroutine(findMe());
//		StartCoroutine(findMyRecord());
//		StartCoroutine(listenToSpace());
//		StartCoroutine(shareRecord(2));
		readLocalData(LOCAL_DATA_PATHFILE);
	}
		


	// Helper to build clean URL.
	public WWW buildRequest(string url, WWWForm form = null){
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
	public IEnumerator findMe() {
		WWW request = buildRequest ("Players/" + my_id_from_local_base);
		yield return request;

		if (request.error == null) 
		{
			// Build the player
			me = new Player(JsonUtility.FromJson<PlayerDBModel>(request.text));
			Debug.Log(string.Format("** Me > {0}", me.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		} 
	}


	// Find my record.
	IEnumerator findMyRecord() {
		WWW request = buildRequest ("Records/findOne?filter={\"where\":{\"id\":1}}");
		yield return request;

		if (request.error == null) 
		{
			// Build my Record and stick it to me.
			me.MyRecord = new Record (JsonUtility.FromJson<RecordDBModel> (request.text));
			Debug.Log(string.Format("** Me > {0}", me.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		} 
	}


	// TODO Handle the record.
	// Get a random record.
	IEnumerator listenToSpace() {
		WWW request = buildRequest ("Players/" + my_id_from_local_base + "/listenToSpace");
		yield return request;

		if (request.error == null) 
		{
			// Get the first string representation of complex JSONObject.
			string first_element = getFirstFromJSONString(request.text);
			// Build the random Record.
			Record from_space = new Record (JsonUtility.FromJson<RecordDBModel> (first_element));

			Debug.Log(string.Format("** Record from_space > {0}", from_space.ToString()));
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

		WWW request = buildRequest ("Players/" + my_id_from_local_base + "/shareRecord", form);
		yield return request;

		if (request.error == null) 
		{
			// Get the first string representation of complex JSONObject.
			string first_element = getFirstFromJSONString(request.text);
			// Update me.
			Player me_updated = new Player (JsonUtility.FromJson<PlayerDBModel> (first_element));
			me.SharingId = me_updated.SharingId;
			Debug.Log(string.Format("** Player me updated > {0}", me.ToString()));
		} 
		else 
		{
			Debug.Log(string.Format("** ERROR Request: {0}", request.text));
		} 
	}


	// Create a new player.
	IEnumerator createPlayer(string rgba) {

		WWWForm form = new WWWForm();
		form.AddField("rgba", rgba);

		WWW request = buildRequest ("Players/", form);
		yield return request;

		if (request.error == null) 
		{
			me = new Player (JsonUtility.FromJson<PlayerDBModel> (request.text));
			Debug.Log(string.Format("** Player me > {0}", me.ToString()));
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
	public void readLocalData(string path){
		try {  
			Debug.Log("Reading local data...");

			StreamReader sr = new StreamReader(LOCAL_DATA_PATHFILE);
			string local_data = sr.ReadToEnd();
			sr.Close();

			me = new Player(JsonUtility.FromJson<PlayerDBModel>(local_data));
			Debug.Log(string.Format("** Player me > {0}", me.ToString()));

		} catch (FileNotFoundException e) {
			Debug.Log ("** No local data found, creating a new Player...");

			Color random_color = Random.ColorHSV();
			string[] random_color_strings = new string[] {
				random_color.r.ToString ("0.0000"),
				random_color.g.ToString ("0.0000"),
				random_color.b.ToString ("0.0000"),
				random_color.a.ToString ("0.0000")
			};

			// Create a new Player with random rgba string
			StartCoroutine(
				createPlayer(string.Join(",", random_color_strings))
			);

		} catch (IOException e) {  
			Debug.Log(e.ToString());  
		}
	}
}
