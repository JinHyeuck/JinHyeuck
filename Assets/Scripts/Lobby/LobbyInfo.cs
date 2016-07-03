using UnityEngine;
using System.Collections;

public class LobbyInfo : MonoBehaviour{
	//private static LobbyInfo mInstance = null;
	//private static GameObject mContainer;

	/*public static LobbyInfo instance {
		get {
			if (mInstance == null) {
				mContainer = new GameObject ("LobbyInfo");
				mInstance = mContainer.AddComponent<LobbyInfo> ();
			}
			return mInstance;
		}
	}*/

	public struct LobbyInfoData
	{
		public int level;
		public int exp;
		public string name;
		public int gold;
		public string rankgrade;
		public int rankpoint;
		public int user_image;


		public LobbyInfoData(int _level, int _exp, string _name, int _gold, string _rankgrade, int _rankpoint, int _user_image)
		{
			level = _level;
			exp = _exp;
			name = _name;
			gold = _gold;
			rankgrade = _rankgrade;
			rankpoint = _rankpoint;
			user_image = _user_image;
		}

		public int getLevel()
		{
			return level;
		}
		/*public int setGold(int _gold)
		{
			gold -= _gold;
		}*/
	}



	public struct toy_info
	{
		public int type;
		public int level;
		public int upgrade_value;
		public int hp;
		public int damage;
		public int exp;

		public toy_info(int _type, int _level, int _upgrade_value, int _hp, int _damage, int _exp)
		{
			type = _type;
			level = _level;
			upgrade_value = _upgrade_value;
			hp = _hp;
			damage = _damage;
			exp = _exp;
		}

	}

	public static toy_info[] toy_list;

	public struct friend_info
	{
		public string name;
		public int rankgrade;
		public int rankpoint;

		public friend_info(string _name, int _rankgrade, int _rankpoint)
		{
			name = _name;
			rankgrade = _rankgrade;
			rankpoint = _rankpoint;
		}

	}

	public static friend_info[] friend_list;


	public static LobbyInfoData mLobbyInfoData;

	/*public LobbyInfo()
	{
		Debug.Log ("LobbyInfo");
		//서버와 연동해서 데이터를 받아야하는 부분
		mLobbyInfoData = new LobbyInfoData (1, 10, "user1", 435543, 2, 1234, 1);
	}*/

	public void init()
	{
		Debug.Log ("LobbyInfo");
		//서버와 연동해서 데이터를 받아야하는 부분
		toy_list = new toy_info[8];

		toy_list[0] = new toy_info(1, 3, 2, 50, 20, 32);
		toy_list[1] = new toy_info(2, 3, 2, 50, 20, 32);
		toy_list[2] = new toy_info(3, 3, 2, 50, 20, 32);
		toy_list[3] = new toy_info(4, 3, 2, 50, 20, 32);
		toy_list[4] = new toy_info(5, 3, 2, 50, 20, 32);
		toy_list[5] = new toy_info(6, 3, 2, 50, 20, 32);
		toy_list[6] = new toy_info(7, 3, 2, 50, 20, 32);
		toy_list[7] = new toy_info(8, 3, 2, 50, 20, 32);


		friend_list = new friend_info[5];

		friend_list [0] = new friend_info ("오진혁", 10, 12312);
		friend_list [1] = new friend_info ("성인영", 9, 1231);
		friend_list [2] = new friend_info ("이현호", 8, 123);
		friend_list [3] = new friend_info ("안진서", 7, 12);
		friend_list [4] = new friend_info ("김다슬", 6, 1);


		mLobbyInfoData = new LobbyInfoData (1, 10, "user1", 435543, "A+", 1234, 1);



	}

	public void send(string _msg)
	{
		Client.instance.beginSend (_msg);
	}

	public object receive(string _msg)
	{
		return Json.Parse<JsonLobby> (_msg);
	}


		
}
