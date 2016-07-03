using UnityEngine;
using System.Collections;

public class PVEArbiter : IArbiter
{
	private Character mPlayer;
	private Character mAI;

	public Character player {
		get {
			return mPlayer;
		}
	}

	public Character rival {
		get {
			return mAI;
		}
	}

	public IEnumerator init () {
		Debug.Log ("init PVE Arbiter");
		yield return null;
	}

	public IEnumerator updated () {
		yield return null;
	}

	public void doAction (StateType _type, object _data) {
	}
}

