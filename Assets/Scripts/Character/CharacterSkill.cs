using UnityEngine;
using System.Collections;

public class CharacterSkill {

	public void attackSkill(int _damage, AttackType _type) {
		switch (_type) {
		case AttackType.CROSS:
			Debug.LogFormat ("{0}damage skill attack: cross", _damage);
			break;
		case AttackType.X:
			Debug.LogFormat ("{0}damage skill attack : x", _damage);
			break;
		case AttackType.RECT:
			Debug.LogFormat ("{0}damage skill attack : rect", _damage);
			break;
		}
	}

	public void restoreStamina(ref CharacterStatus _status) {
		_status.stamina += _status.staminaRegen;

		if (_status.stamina >= _status.maxStamina)
			_status.stamina = _status.maxStamina;
		
		Debug.Log ("restore stamina");
	}

	public void restoreCost(ref CharacterCost _cost) {
		_cost.increaseCost (4);

		Debug.Log ("restore cost");
	}


}
