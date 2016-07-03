using UnityEngine;
using System.Collections;

// 온라인대전 상대
public class Rival : Character
{
	public override void init(IPublicData _data) {
		//      mStatus.init (100, 20, 40);
		//      mCost.init (10);
		PVPInfo myInfo = (PVPInfo)_data.data;
		mStatus.init (myInfo.hp, myInfo.damage, myInfo.cost);
		mCost.init (myInfo.cost);
	}

	public override void updated (int _cost, IPublicData _data = null) {
		StartCoroutine (action (_data, _cost));
		ArbiterManager.instance.updated ();

		//      Debug.Log ("UI info update or data push to server");
		// arbiter에 메시지 보내면 됨
	}

	public override IEnumerator move (IPublicData _data, int _cost) {
		//      Client.instance.beginSend ("move");
		action = base.move;
		updated (_cost, _data);
		yield return null;
	}
}