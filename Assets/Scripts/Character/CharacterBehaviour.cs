using UnityEngine;
using System.Collections;

public enum AttackType : int {
	CROSS, X, RECT
}
public enum MOVEType : int
{
    CROSS, X
}

public class MoveGroup {
	public TileAdress adress;
	public int cost;

	public MoveGroup () {}
	public MoveGroup (TileAdress _adress, int _cost) {
		adress = _adress;
		cost = _cost;
	}
}

public class CharacterBehaviour {

	#region variable
	private CharacterSkill	mSkill = new CharacterSkill ();
	#endregion

	#region behaviour
	public IEnumerator move(Transform _target, Vector3 _targetPos) {
		_target.LookAt (_targetPos);

		_target.position = _targetPos;
		yield return null;
	}

	public void attack(int _damage, AttackType _type) {
		switch (_type) {
		case AttackType.CROSS:
			Debug.LogFormat ("{0}damage attack : cross", _damage);
			break;
		case AttackType.X:
			Debug.LogFormat ("{0}damage attack : x", _damage);
			break;
		}
		// attack
	}

	public void defend() {
		Debug.Log ("defend");
		// defense
	}
	#endregion
	#region skill
	public void attackSkill(int _damage, AttackType _type) {
		mSkill.attackSkill (_damage, _type);
	}

	// 수정후 삭제요망
	public void restoreStamina(ref CharacterStatus _status) {
		mSkill.restoreStamina (ref _status);
	}
	// 수정후 삭제요망
	public void restoreCost(ref CharacterCost _cost) {
		mSkill.restoreCost (ref _cost);
	}
	#endregion
}
