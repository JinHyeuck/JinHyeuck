using UnityEngine;

public interface IPublicData {
	object data {
		get;
		set;
	}
}

public class IntData : IPublicData {

	private int mData;

	public object data {
		get {
			return mData;
		}
		set {
			mData = (int)value;
		}
	}

	public IntData() {}
	public IntData(int _data) {
		mData = _data;
	}
}

public class Vector3Data : IPublicData {

	private Vector3 mData;

	public object data {
		get {
			return mData;
		}
		set {
			mData = (Vector3)value;
		}
	}

	public Vector3Data () {}
	public Vector3Data(Vector3 _data) {
		mData = _data;
	}
}

public class FloatData : IPublicData {

	private float mData;

	public object data {
		get {
			return mData;
		}
		set {
			mData = (float)value;
		}
	}

	public FloatData() {}
	public FloatData(float _data) {
		mData = _data;
	}
}

public class AttackTypeData : IPublicData {

	private AttackType mData;

	public object data {
		get {
			return mData;
		}
		set {
			mData = (AttackType)value;
		}
	}

	public AttackTypeData () {}
	public AttackTypeData(AttackType _data) {
		mData = _data;
	}
}

public class MOVETypeData : IPublicData
{

	private MOVEType mData;

	public object data
	{
		get
		{
			return mData;
		}
		set
		{
			mData = (MOVEType)value;
		}
	}
}

public class SkillTypeData : IPublicData {

	private SkillType mData;

	public object data {
		get {
			return mData;
		}
		set {
			mData = (SkillType)value;
		}
	}
}


public class StateTypeData : IPublicData
{

	private StateType mData;

	public object data
	{
		get
		{
			return mData;
		}
		set
		{
			mData = (StateType)value;
		}
	}
}

public class PVPInfoData : IPublicData {
	private PVPInfo mData;

	public object data {
		get {
			return (PVPInfo)mData;
		}
		set {
			mData = (PVPInfo)value;
		}
	}

	public PVPInfoData () {}
	public PVPInfoData(PVPInfo _data) {
		mData = _data;
	}
}

public class TileAdressData : IPublicData {
	private TileAdress mData;

	public object data {
		get {
			return mData;
		}
		set {
			mData = (TileAdress)value;
		}
	}

	public TileAdressData() {}
	public TileAdressData(TileAdress _data) {
		mData = _data;
	}
}