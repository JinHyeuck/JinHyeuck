using System.Collections.Generic;

public interface IJsonType {
	IPublicData Parse (Dictionary<string, object> _json);
}