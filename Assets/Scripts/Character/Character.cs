using UnityEngine;
using System.Collections;

// 상태머신을 사용해야 할지도모름
// 방어상태 같은거 체크할 때 필요할듯
public enum StateType {
	IDLE, MOVE, ATTACK, DEFENSE, HEAL, SKILL, COMPLETE
}
	
public enum SkillType {
	CROSS, X, RECT, HEAL, HEAL_COST
}

public enum ToyType {
	FLOWER, LION, PENGUIN, PIG
}

public abstract class Character : MonoBehaviour {

	#region struct
	public struct CharInfo {
		public int			stamina;
		public int			damage;
		public int			skillDamage;
		public StateType	state;

		public CharInfo(int _stamina, int _damage, int _skillDamage, StateType _state) {
			stamina = _stamina;
			damage = _damage;
			skillDamage = _skillDamage;
			state = _state;
		}
	}
	#endregion

	#region variable
	protected CharacterBehaviour	mBehaviour	= new CharacterBehaviour ();
	protected CharacterCost			mCost		= new CharacterCost ();
	protected CharacterStatus		mStatus		= new CharacterStatus ();

	protected StateType				mState	= StateType.IDLE;

	public int id {
		get;
		set;
	}

	public delegate IEnumerator DelegateAction (IPublicData _data, int _cost);
	protected DelegateAction action;
	#endregion

	protected TileAdress mAdress;
	public TileAdress adress {
		get {
			return mAdress;
		}
		set {
			mAdress = value;
		}
	}

	protected ToyType mToyType;
	public ToyType toyType {
		get {
			return mToyType;
		}
		set {
			mToyType = value;
		}
	}

	public abstract void init (IPublicData _data);
	public abstract void updated (int _cost, IPublicData _data = null);


	public CharInfo charInfo {
		get {
			return new CharInfo (mStatus.stamina, mStatus.damage, mStatus.skillDamage, mState);
		}
	}

	public int getStamina() {
		return mStatus.stamina;
	}

	public int getDamage() {
		return mStatus.damage;
	}

	public int getSkillDamage() {
		return mStatus.skillDamage;
	}

	public int getState() {
		return (int)mState;
	}

	public int getCost() {
		return mCost.cost;
	}

	#region Behaviour
	public virtual IEnumerator move(IPublicData _data, int _cost) { // _value isn't using.
		mState = StateType.MOVE;
		mAdress = (TileAdress)_data.data;
		mCost.reduceCost (_cost);
		yield return StartCoroutine (mBehaviour.move (transform, TileManager.instance.findTilePosition (mAdress.y, mAdress.x)));
	}

	public virtual void attack(IPublicData _data, int _cost) {
//		if (mCost.cost < 2)
//			return;

		mState = StateType.ATTACK;
		mBehaviour.attack (mStatus.damage, (AttackType)_data.data);
		mCost.reduceCost (2);
	}

	public virtual void defend(IPublicData _data, int _cost) { // _value isn't using.
//		if (mCost.cost < 1)
//			return;

		mBehaviour.defend ();
		mCost.reduceCost (1);
	}

	// 0 : skill cross / 1 : skill X / 2 : skill rect / 3 : restore stamina / 4 : restore cost
	public virtual void useSkill(IPublicData _data, int _cost) {
		switch ((int)_data.data) {
		case 0:
//			if (mCost.cost < 5)
//				break;

			mState = StateType.SKILL;
			mBehaviour.attackSkill (mStatus.damage, AttackType.CROSS);
			mCost.reduceCost (5);
			break;

		case 1:
//			if (mCost.cost < 5)
//				break;

			mState = StateType.SKILL;
			mBehaviour.attackSkill (mStatus.damage, AttackType.X);
			mCost.reduceCost (5);
			break;

		case 2:
//			if (mCost.cost < 5)
//				break;

			mState = StateType.SKILL;
			mBehaviour.attackSkill (mStatus.damage, AttackType.RECT);
			mCost.reduceCost (5);
			break;

		case 3:
//			if (mCost.cost < 4 || mStatus.stamina <= 0)
//				break;

			mState = StateType.HEAL;
			mBehaviour.restoreStamina (ref mStatus);
			mCost.reduceCost (4);
			break;

		case 4:
			mState = StateType.SKILL;
			mBehaviour.restoreCost (ref mCost);
			break;
		}
	}

	//	public virtual void restoreStamina(int _value) { // _value isn't using.
	//		if (mCost.cost < 4)
	//			return;
	//
	//		mBehaviour.restoreStamina ();
	//		mCost.reduceCost (4);
	//	}
	//
	//	public virtual void restoreCost(int _value) { // _value isn't using.
	//		mCost.increaseCost (4);
	//	}

	// _type을 int가 아닌 열거형으로 변경하여야 함
	//	public virtual void useSkill(int _type) {
	//		if (mCost.cost < 5)
	//			return;
	//		
	//		mBehaviour.useAttackSkill (mStatus.skillDamage, (ATTACK_TYPE)_type);
	//		mCost.reduceCost (5);
	//	}

	#endregion

	public virtual void undergoAttack(int _damage) {
		mStatus.stamina -= _damage;
		if (mStatus.stamina < 0) {
			mStatus.stamina = 0;
		}
	}

	public bool haveCost (int _value) {
		if (mCost.cost >= _value) {
			Debug.LogFormat ("more {0}cost", _value);
			return true;
		}
		else
			return false;
	}

//	public virtual StateType doAction(StateType _type) {
//
//		if (_type == StateType.ATTACK) {
//			if (haveCost (2)) {
//				return _type;
//			}
//		} else if (_type == StateType.MOVE) {
//			if (haveCost (1)) {
//				return _type;
//			}
//		} else if (_type == StateType.SKILL) {
//			if (haveCost (4)) {
//				return _type;
//			}
//		}
//
//
//		else if (_type == StateType.HEAL) {
//			if (haveCost (4)) {
//				mState = _type;
//			}
//		} else if (_type == StateType.DEFENSE) {
//			if (haveCost (1)) {
//				mState = _type;
//			}
//		}
//
//		return StateType.IDLE;
//	}

}
