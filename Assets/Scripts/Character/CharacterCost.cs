using UnityEngine;
using System.Collections;

public class CharacterCost {

	#region variable
	private int mCost;
	private int mMaxCost;
	#endregion

	public int cost {
		get {
			return mCost;
		}
	}

	public void init (int _cost) {
		mCost = _cost;
		mMaxCost = _cost;
	}

	public void reduceCost (int _cost) {
		mCost -= _cost;
	}

	public void increaseCost (int _cost) {
		mCost += _cost;
		if (mCost > mMaxCost)
			mCost = mMaxCost;
	}
}
