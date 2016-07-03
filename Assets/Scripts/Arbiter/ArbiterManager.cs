using UnityEngine;
using System.Collections;

public enum ArbiterType { PVP, PVE }

public class ArbiterManager : MonoBehaviour // 중재자를 통해서 서버랑 UI에 메시지를 보낸다
{
	private static ArbiterManager mInstance;
	private static GameObject mContainer;

//	public static ArbiterManager instance {
//		get {
//			if (!mInstance) {
//				mInstance = GameObject.FindObjectOfType<ArbiterManager> ();
//			}
//			return mInstance;
//		}
//	}

	public static ArbiterManager instance {
		get {
			if(!mInstance) {
				mContainer = new GameObject("ArbiterManager");
				mInstance = mContainer.AddComponent<ArbiterManager> ();
			}
			return mInstance;
		}
	}
		
	[SerializeField]
	private ArbiterType mArbiterType;

	private IArbiter mArbiter;
	public IArbiter arbiter {
		get {
			return mArbiter;
		}
	}

//	void Awake () {
//		init ();
//	}

	public IEnumerator init (ArbiterType _type) {
//		Client.instance.init (); // 수정필요

		switch (_type) {
		case ArbiterType.PVP:
			mArbiter = new PVPArbiter ();
			yield return StartCoroutine (mArbiter.init ());

			break;

		case ArbiterType.PVE:
			mArbiter = new PVEArbiter ();
				// 수정해야됨
			break;

		default:
			
			break;
		}
		Debug.Log ("ArbiterManager init");



		yield return null;
	}

	public void updated() {
		UIManager.instance.updated ();
		mArbiter.updated ();
	}

	public void doAction(StateType _type, object _data) {
		mArbiter.doAction (_type, _data);
	}
}

