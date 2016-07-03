using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private static GameManager mInstance;
	private static GameObject mContainer;

	public static GameManager instance {
		get {
			if (!mInstance) {
				mInstance = GameObject.FindObjectOfType<GameManager> ();
			}
			return mInstance;
		}
	}

	private ISocket mTargetObject;
	public ISocket target {
		get {
			return mTargetObject;
		}
		set {
			mTargetObject = value;
		}
	}

	void Awake () {
		string scene_name = SceneManager.GetActiveScene ().name;

		// pvp
		if (scene_name == "fight") {
			Debug.Log ("GameManager init");
			StartCoroutine (ArbiterManager.instance.init (ArbiterType.PVP));
//			ArbiterManager.instance.init (ArbiterType.PVP);
		}
		// lobby
		else if (scene_name == "lobby") {
			LobbyManager.instance.init ();
		} 
		//main
		else if (scene_name == "main") {
		}
	}
}