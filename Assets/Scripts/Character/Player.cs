using UnityEngine;
using System.Collections;

// 플레이어
public class Player : Character
{

	public override void init(IPublicData _data) {
		PVPInfo myInfo = (PVPInfo)_data.data;
		mStatus.init (myInfo.hp, myInfo.damage, myInfo.cost);
		mCost.init (myInfo.cost);

		Debug.LogFormat (" hp : {0}\t\tdamage : {1}\nskill damage : {2}\tcost : {3}",
			mStatus.stamina, mStatus.damage, mStatus.skillDamage, mCost.cost);
	}

//	void Start() {
//		init ();
//	}

	public override void updated (int _cost, IPublicData _data = null) {
		StartCoroutine (action (_data, _cost));
		ArbiterManager.instance.updated ();
	}

	public override IEnumerator move (IPublicData _data, int _cost) {
		action = base.move;
		updated (_cost, _data);
		yield return null;
	}

	public override void attack (IPublicData _data, int _cost) {
//		action = base.attack;
//		updated (_cost, _data);
	}

	public override void defend (IPublicData _data, int _cost) {
//		action = base.defend;
//		updated (_cost);
	}

	public override void useSkill (IPublicData _data, int _cost) {
//		action = base.useSkill;
//		updated (_cost, _data);
	}

	// 공격받음
	public override void undergoAttack (int _damage) {
		Client.instance.beginSend ("undergo attack");
//		action = base.undergoAttack;
//		updated (_damage);
	}
}

