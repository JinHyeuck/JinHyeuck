using UnityEngine;
using System.Collections.Generic;

public class JsonLobby : IJsonType {

	public struct LobbyInfoData
	{
		public int level;
		public int exp;
		public string name;
		public int gold;
		public int rankgrade;
		public int rankpoint;
		public int user_image;



		public LobbyInfoData(int _level, int _exp, string _name, int _gold, int _rankgrade, int _rankpoint, int _user_image)
		{
			level = _level;
			exp = _exp;
			name = _name;
			gold = _gold;
			rankgrade = _rankgrade;
			rankpoint = _rankpoint;
			user_image = _user_image;
		}
	}

	public LobbyInfoData mLobbyInfoData;

	public IPublicData Parse(Dictionary<string, object> _json) 
	{
		return null;
	}
}
