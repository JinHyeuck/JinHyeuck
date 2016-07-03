//#define _DEBUG

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PVPInfo {
	// 유저 이름
	public int id {get; set;}

	// toy 스테이터스
	public int hp {get; set;}
	public int cost {get; set;}
	public int damage {get; set;}

	// toy 정보
	public ToyType toy_type {get; set;}
	public int toy_lvl {get; set;}

	// 초기위치 결정 우선순위 변수
	public int priority {get; set;}

	// toy 위치
	public TileAdress adress {get; set;}

	// toy 상태
	public int state {get; set;}
}

public class PVPArbiter : IArbiter
{
	private Character mPlayer;
	private Character mRival;

	public Character player {
		get {
			return mPlayer;
		}
	}

	public Character rival {
		get {
			return mRival;
		}
	}

	public IEnumerator init () {
		#if _DEBUG
		Client.instance.init ();

		//      GameManager.instance.target = this;

		PVPInfo myInfo = new PVPInfo ();

		int[,] test = new int[5,5];
		for (int i = 0; i < 5; ++i) {
		for (int j = 0; j < 5; ++j) {
		test[i,j] = i*5+j;
		}
		}

		myInfo.hp = 50;
		myInfo.cost = 10;
		myInfo.damage = 10;
		myInfo.priority = 1;

		Dictionary<string, object> dic = new Dictionary<string, object> ();
		dic.Add ("type", "pvp_init");
		dic.Add ("user_info", myInfo);

		// pvp_init 전송
		//      send (Json.Write (dic));

		dic.Clear();

		PVPInfo rivalInfo = new PVPInfo ();
		rivalInfo.hp = 100;
		rivalInfo.cost = 50;
		rivalInfo.damage = 20;
		rivalInfo.priority = 1;

		dic.Add ("type", "pvp_init");
		dic.Add ("user_info", rivalInfo);

		Client.instance.recvQueue.Enqueue (Json.Write(dic));

		rivalInfo = (PVPInfo)Json.Parse<JsonPVP>(Client.instance.recvQueue.Dequeue ()).data;

		TileManager.instance.init ();

		mPlayer = createCharacter<Player> (ToyType.FLOWER, myInfo, 1 - rivalInfo.priority);
		mRival = createCharacter<Rival> (ToyType.PIG, rivalInfo, rivalInfo.priority);

		mPlayer.transform.LookAt (mRival.transform.position);
		mRival.transform.LookAt (mPlayer.transform.position);

		//      if (bInfo.priority == 1) {
		//         setCharacter (mPlayer, TileManager.instance.leftSpawnPos);
		//         setCharacter (mRival, TileManager.instance.rightSpawnPos);
		//      } else {
		//         setCharacter (mPlayer, TileManager.instance.rightSpawnPos);
		//         setCharacter (mRival, TileManager.instance.leftSpawnPos);
		//      }

		UIManager.instance.init (UIType.BATTLE);

		#else
		Client.instance.init (); // test code

		PVPInfo myInfo = new PVPInfo ();
		PVPInfo rivalInfo = new PVPInfo ();

		myInfo.hp = 50;
		myInfo.cost = 10;
		myInfo.damage = 10;
		myInfo.priority = 0;
		myInfo.toy_type = (ToyType)Random.Range(0,8);

		Dictionary<string, object> dic = new Dictionary<string, object> ();
		dic.Add ("type", "pvp_init");
		dic.Add ("user_info", myInfo);

		Client.instance.beginSend (Json.Write (dic));

		while (true) {
			if (Client.instance.recvQueue.Count > 0) {
				break;
			}
			yield return new WaitForSeconds (0.5f);
		}

		Dictionary<string, object> dic2 = Json.Read (Client.instance.recvQueue.Dequeue());
		rivalInfo = Json.Deserialize<PVPInfo> (dic2["user_info"]);

		TileManager.instance.init ();

		mPlayer = createCharacter<Player> (myInfo, 1 - rivalInfo.priority);
		mRival = createCharacter<Rival> (rivalInfo, rivalInfo.priority);

		mPlayer.toyType = myInfo.toy_type;
		mRival.toyType = rivalInfo.toy_type;

		mPlayer.transform.LookAt (mRival.transform.position);
		mRival.transform.LookAt (mPlayer.transform.position);

		UIManager.instance.init (UIType.BATTLE);   

		#endif

		Debug.Log ("init PVP Arbiter");

		StaticCoroutine.DoCoroutine (this.CheckClientQueue ());
		yield return null;
	}

	public IEnumerator updated () {
		player.transform.LookAt (rival.transform.position);
		rival.transform.LookAt (player.transform.position);

		return null;
	}

	// 메쉬랑 텍스처를 여기서 적용할건지, 따로 만들건지.
	// 우선은 적용하겠음.
	private Character createCharacter <T> (PVPInfo _info, int _priority) where T : Character {
		string toy_name = "";

		switch (_info.toy_type) {
		case ToyType.FLOWER:
			toy_name = "flower";
			break;
		case ToyType.LION:
			toy_name = "lion";
			break;
//		case ToyType.MOUSE:
//			toy_name = "mouse";
//			break;
//		case ToyType.NORU:
//			toy_name = "noru";
//			break;
		case ToyType.PENGUIN:
			toy_name = "penguin";
			break;
		case ToyType.PIG:
			toy_name = "pig";
			break;
//		case ToyType.PIG2:
//			toy_name = "pig2";
//			break;
//		case ToyType.SNAKE:
//			toy_name = "snake";
//			break;
		default:
			toy_name = "flower";
			break;
		}

		GameObject go = GameObject.Instantiate (ResourceManager.instance.Load<GameObject> ("Prefabs/Character/"+toy_name)) as GameObject;
		go.name = typeof(T).ToString ();
		go.AddComponent<T> ();

		PVPInfoData pInfoData = new PVPInfoData (_info);

		go.GetComponent<T> ().init (pInfoData);
		if (_priority == 1) {
			go.GetComponent<T> ().adress = TileManager.instance.leftSpawnAdress;
			go.transform.position = TileManager.instance.leftSpawnPos;
		} else {
			go.GetComponent<T> ().adress = TileManager.instance.rightSpawnAdress;
			go.transform.position = TileManager.instance.rightSpawnPos;
		}

		return go.GetComponent<T> ();
	}

	public void doAction (StateType _type, object _data=null) {
		#if _DEBUG
		switch (_type) {
		case StateType.ATTACK:
		break;
		case StateType.MOVE:
		// server 전송
		MoveGroup ms = (MoveGroup)_data;
		PVPInfo myInfo = new PVPInfo();

		myInfo.adress = ms.adress;
		myInfo.hp = player.getStamina();
		myInfo.state = player.getState();

		player.transform.position = TileManager.instance.findTilePosition(myInfo.adress.y, myInfo.adress.x);
		player.adress = myInfo.adress;

		Dictionary<string, object> dic = new Dictionary<string, object> ();
		dic.Add ("type", "pvp_behaviour");
		dic.Add ("user_info", myInfo);

		Client.instance.recvQueue.Enqueue (Json.Write(dic));
		break;
		case StateType.SKILL:
		break;
		default:
		break;
		}
		#else
		switch (_type) {
		case StateType.ATTACK:
			break;
		case StateType.MOVE:
			// server 전송
			MoveGroup ms = (MoveGroup)_data;
			PVPInfo myInfo = new PVPInfo();

			myInfo.adress = ms.adress;
			myInfo.hp = player.getStamina();
			myInfo.state = player.getState();

			//      player.transform.position = TileManager.instance.findTilePosition(myInfo.adress.y, myInfo.adress.x);
			//      player.adress = myInfo.adress;

			Dictionary<string, object> dic = new Dictionary<string, object> ();
			dic.Add ("type", "pvp_behaviour");
			dic.Add ("user_info", myInfo);

			Client.instance.beginSend (Json.Write(dic));

			break;
		case StateType.SKILL:
			break;
		default:
			break;
		}
		#endif
	}

	//   public void send(string _message) {
	//      Client.instance.beginSend (_message);
	//   }
	//
	//   public void receive(string _message) {
	//      Json.Parse<JsonPVP> (_message);
	//   }

	private void setCharacter (Character _char, Vector3 _pos) {
		_char.init (null);

		_char.transform.position = _pos;
		_char.transform.Translate (0.0f, 0.5f, 0.0f);
	}

	private IEnumerator CheckClientQueue () {
		while (true) {
			if (Client.instance.recvQueue.Count > 0) {
				doBehaviour ();
				// something
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	private void doBehaviour() {
		string str = Client.instance.recvQueue.Dequeue ();
		string[] strArr = str.Split ('/');

		Dictionary<string, object> myDic = Json.Read (strArr [0]);
		Dictionary<string, object> rivalDic = Json.Read (strArr [1]);

		if (myDic ["type"].ToString() == "pvp_behaviour") {
			PVPInfo myInfo = Json.Deserialize<PVPInfo> (myDic ["user_info"]);
			PVPInfo rivalInfo = Json.Deserialize<PVPInfo> (rivalDic ["user_info"]);

			player.adress = myInfo.adress;
			rival.adress = rivalInfo.adress;

			player.StartCoroutine (player.move (new TileAdressData(myInfo.adress), 1));
			rival.StartCoroutine (rival.move (new TileAdressData(rivalInfo.adress), 1));

			ArbiterManager.instance.updated ();
		}
	}
}