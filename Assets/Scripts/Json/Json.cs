using System.Collections.Generic;
using JsonFx.Json;

public class Json
{
	public static string Write(Dictionary<string, object> dic) {
		return JsonWriter.Serialize (dic);
	}

	public static Dictionary<string, object> Read(string json) {
		return JsonReader.Deserialize<Dictionary<string, object>> (json);
	}

	public static T Deserialize<T>(object json) {
		JsonReader reader = new JsonReader (JsonWriter.Serialize (json));
		T response = reader.Deserialize<T> ();

		return response;
	}

	public static IPublicData Parse<T>(string _data) where T : IJsonType {
		IJsonType jsonType = System.Activator.CreateInstance<T> ();

		Dictionary<string, object> json = Json.Read (_data);

		return jsonType.Parse (json);
	}
}