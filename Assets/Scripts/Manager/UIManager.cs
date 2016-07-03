using UnityEngine;
using System.Collections;
using JsonFx.Json;

public enum UIType { LOBBY, BATTLE };

// singleton
public class UIManager : MonoBehaviour
{
	private static UIManager mInstance = null;
	private static GameObject mContainer;

	public static UIManager instance {
		get {
			if (mInstance == null) {
				mContainer = new GameObject ("UIManager");
				mInstance = mContainer.AddComponent<UIManager> ();

//				mInstance.init ();
			}
			return mInstance;
		}
	}

	private IUserInterface mUI;

	public IUserInterface ui {
		get {
			return mUI;
		}
	}

	public void init (UIType _type) {
		switch (_type) {
			case UIType.LOBBY:
				mUI = new UILobby ();
				break;
			case UIType.BATTLE:
				GameObject go = new GameObject ("UIBattle");
				go.AddComponent<UIBattle> ();
				go.transform.parent = this.transform;
				mUI = go.GetComponent<UIBattle> ();
				break;
			default:
				break;
		}
		mUI.init ();
		// 씬매니저 이용해서 씬 변경될때마다 맞는 UI 호출하는 부분 추가 필요함
	}

	public void updated () {
		mUI.updated ();
	}

}

