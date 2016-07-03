using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JsonSample : MonoBehaviour
{

	void Start () {
		Dictionary<string, object> dicJson = new Dictionary<string, object> ();
		dicJson.Add ("name", "erikanes");
		dicJson.Add ("age", 26);

		ArrayList arr = new ArrayList ();
		arr.Add ("c");
		arr.Add ("c++");
		arr.Add ("c#");
		arr.Add ("php");
		arr.Add ("java script");
		dicJson.Add ("program language", arr);

		string strJson = Json.Write (dicJson);
		Debug.Log ("json : " + strJson);

		dicJson = Json.Read (strJson);

		Debug.Log ("name : " + dicJson ["name"]);
		Debug.Log ("age : " + dicJson ["age"]);

		string[] strs = (string[])dicJson ["program language"];

		for (int i = 0; i < strs.Length; i++) {
			Debug.Log ("program language : " + strs [i]);
		}
			
	}
}