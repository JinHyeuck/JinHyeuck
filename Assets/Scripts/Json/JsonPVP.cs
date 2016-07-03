using System.Collections.Generic;
using UnityEngine;

public class JsonPVP : IJsonType {

	public IPublicData Parse(Dictionary<string, object> _json) {
		IPublicData pData = null;

		if (_json ["type"].ToString() == "pvp_init") {
			//         pData = new PVPInfoData (Json.Deserialize<PVPInfo> (_json ["user_info"]));
			pData = new PVPInfoData ();
			pData.data = Json.Deserialize<PVPInfo> (_json ["user_info"]);

			// pvp_init

		} else {
			// nothing
		}
		return pData;
	}
}