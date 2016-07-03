using UnityEngine;

// singleton
public class StaticEventDelegate : MonoBehaviour
{
	
	private static StaticEventDelegate mInstance = null;
	private static GameObject mContainer;

	public static StaticEventDelegate instance {
		get {
			if (mInstance == null) {
				mContainer = new GameObject ("StaticEventDelegate");
				mInstance = mContainer.AddComponent<StaticEventDelegate> ();
			}
			return mInstance;
		}
	}

	#region delegate variable
	public delegate int Rint_Vint1(int _value);
	public delegate int Rint_Void();
	public delegate void Rvoid_Vint1(int _value);
	public delegate void Rvoid_Void ();
	public delegate bool Rbool_Vint1(int _value);
	public delegate void Rvoid_Vtype1(StateType _type);
	public delegate void Rvoid_VtypeLobbyButton(LOBBYBUTTON _type);
	public delegate void Rvoid_VtypeExitButton(EXITBUTTON _type);

	#endregion

	#region MakeEvetnDelegate
	//MonoBehaviour를 상속받지 않은 클래스의 메서드를 EventDelegate 인스턴스에 포함시켜주는 대리 메서드.
//	public EventDelegate makeEventDelegate(Rint_Vint1 _action, int _obj) {
//		return make<Rint_Vint1> (_action, _obj);
//	}
//
//	//MonoBehaviour를 상속받지 않은 클래스의 메서드를 EventDelegate 인스턴스에 포함시켜주는 대리 메서드.
//	public EventDelegate makeEventDelegate (Rint_Void _action) {
//		return make<Rint_Void> (_action);
//	}
//
//	//MonoBehaviour를 상속받지 않은 클래스의 메서드를 EventDelegate 인스턴스에 포함시켜주는 대리 메서드.
//	public EventDelegate makeEventDelegate (Rvoid_Vint1 _action, int _obj) {
//		return make<Rvoid_Vint1> (_action, _obj);
//	}
//
//	//MonoBehaviour를 상속받지 않은 클래스의 메서드를 EventDelegate 인스턴스에 포함시켜주는 대리 메서드.
//	public EventDelegate makeEventDelegate (Rvoid_Void _action) {
//		return make<Rvoid_Void> (_action);
//	}
//
//	public EventDelegate makeEventDelegate (Rbool_Vint1 _action, int _obj) {
//		return make<Rbool_Vint1> (_action, _obj);
//	}

	public EventDelegate makeEventDelegate<T> (T _action, object _obj = null) {
		return make<T> (_action, _obj);
	}

	//
	private EventDelegate make<T> (T _action, object _obj = null) {
		EventDelegate evt_dele = null;

		if (_action.GetType () == typeof(Rint_Vint1))
			evt_dele = new EventDelegate (this, "Delegate_Rint_Vint1");
		else if (_action.GetType () == typeof(Rint_Void))
			evt_dele = new EventDelegate (this, "Delegate_Rint_Void");
		else if (_action.GetType () == typeof(Rvoid_Vint1))
			evt_dele = new EventDelegate (this, "Delegate_Rvoid_Vint1");
		else if (_action.GetType () == typeof(Rvoid_Void))
			evt_dele = new EventDelegate (this, "Delegate_Rvoid_Void");
		else if (_action.GetType () == typeof(Rbool_Vint1))
			evt_dele = new EventDelegate (this, "Delegate_Rbool_Vint1");
		else if (_action.GetType () == typeof(Rvoid_Vtype1))
			evt_dele = new EventDelegate (this, "Delegate_Rvoid_Vtype1");
		else if (_action.GetType () == typeof(Rvoid_VtypeLobbyButton))
			evt_dele = new EventDelegate (this, "Delegate_Rvoid_VtypeLobbyButton");
		else if (_action.GetType () == typeof(Rvoid_VtypeExitButton))
			evt_dele = new EventDelegate (this, "Delegate_Rvoid_VtypeExitButton");
		else
			return evt_dele;

		evt_dele.parameters [0] = new EventDelegate.Parameter (_action);
		if (_obj != null) {
			evt_dele.parameters [1] = new EventDelegate.Parameter (_obj);
		}
		return evt_dele;
	}
	#endregion

	#region Delegate
	private int Delegate_Rint_Vint1(Rint_Vint1 _action, int _value) {
		return _action (_value);
	}

	private int Delegate_Rint_Void(Rint_Void _action) {
		return _action ();
	}

	private void Delegate_Rvoid_Vint1(Rvoid_Vint1 _action, int _value) {
		_action (_value);
	}

	private void Delegate_Rvoid_Void(Rvoid_Void _action) {
		_action ();
	}

	private bool Delegate_Rbool_Vint1(Rbool_Vint1 _action, int _value) {
		_action (_value);
		return true;
	}

	private void Delegate_Rvoid_Vtype1(Rvoid_Vtype1 _action, StateType _type) {
		_action (_type);
	}

	private void Delegate_Rvoid_VtypeLobbyButton(Rvoid_VtypeLobbyButton _action, LOBBYBUTTON _type) {
		_action (_type);
	}

	private void Delegate_Rvoid_VtypeExitButton(Rvoid_VtypeExitButton _action, EXITBUTTON _type) {
		_action (_type);
	}
	#endregion

//	private EventDelegate.Parameter makeParameter(Object _obj, System.Type _type) {
//		EventDelegate.Parameter param = new EventDelegate.Parameter ();
//		param.obj = _obj;
//		param.expectedType = _type;
//
//		return param;
//	}

}

