using UnityEngine;
using System.Collections;

public class CharacterStatus {

	private	int	mStamina;
	private int mDamage;
	private int mSkillDamage;

	private int mStaminaRegen = 10;
	private int mMaxStamina;

	#region getter/setter
	public int stamina {
		get {
			return mStamina;
		}
		set {
			mStamina = value;
		}
	}

	public int damage {
		get {
			return mDamage;
		}
	}

	public int skillDamage {
		get {
			return mSkillDamage;
		}
	}

	public int staminaRegen {
		get {
			return mStaminaRegen;
		}
	}

	public int maxStamina {
		get {
			return mMaxStamina;
		}
	}
	#endregion

	public void init (int _stamina, int _damage, int _skillDamage) {
		mStamina = _stamina;
		mDamage = _damage;
		mSkillDamage = _skillDamage;

		mMaxStamina = _stamina;
	}

}
