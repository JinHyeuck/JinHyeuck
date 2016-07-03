#define _DEBUG

using UnityEngine;
using System.Collections;

public class TileAction : MonoBehaviour
{
	private StateType mType;
	private Color mColor;
	private Vector3 mPosition;
	private int mCrossType;

	//   public delegate IEnumerator DelegateAction(IPublicData _data, int _cost);
	//   private DelegateAction delegateAction;
	//   public delegate void DelegateAction(StateType _type, IPublicData _data1, IPublicData _data2=null);
	public delegate void DelegateAction(StateType _type, object _data1=null);
	private DelegateAction delegateAction;


	public void init(StateType _type, Color _color, Vector3 _pos, int _crosstype) {
		mType = _type;
		mColor = _color;
		mPosition = _pos;
		mCrossType = _crosstype;
		if (_type == StateType.ATTACK) {
		}
		//            bind(ArbiterManager.instance.arbiter.player.attack);
		else if(_type == StateType.MOVE) {
			bind (ArbiterManager.instance.doAction);
		}
		//         bind(ArbiterManager.instance.arbiter.player.move);
		//         bind(ArbiterManager.instance.doAction("asd"));
		/*else if(_type == StateType.SKILL)
            bind(ArbiterManager.instance.arbiter.player.s);*/
	}

	public void bind(DelegateAction _action) {
		delegateAction = _action;
	}

	IEnumerator OnMouseDown () {
		//      // 임시 테스트
		/*IPublicData data = new AttackTypeData ();
      data.data = AttackType.X;
      delegateAction (data, 2);*/
		actionidelegateAction(mType);
		// 부모를통해 BroadcastMessage를 실행하여 이 스크립트를 제거한다.
		transform.parent.BroadcastMessage ("DestroyTileAction");
		return null;
	}

	private void actionidelegateAction(StateType _type)
	{
		if (_type == StateType.MOVE)
		{
			//            IPublicData data = new AttackTypeData();
			IPublicData pos = new Vector3Data(transform.position + (Vector3.up/2.0f));
			//         pos.data = transform.position + (Vector3.up/2.0f);
			#if _DEBUG
			if (mCrossType % 2 == 0)
			{
				delegateAction(StateType.MOVE, new MoveGroup(gameObject.GetComponent<Tile> ().adress, 2));
				//            delegateAction(StateType.MOVE, new TileAdressData(gameObject.GetComponent<Tile> ().adress), new IntData(2));
			}
			else
			{
				delegateAction(StateType.MOVE, new MoveGroup(gameObject.GetComponent<Tile> ().adress, 1));
				//            delegateAction(StateType.MOVE, new TileAdressData(gameObject.GetComponent<Tile> ().adress), new IntData(1));
			}
			#elif
			if (mCrossType % 2 == 0)
			{
			//                data.data = MOVEType.CROSS;
			//                delegateAction(data, 2);
			StartCoroutine(delegateAction(pos, 1));
			}
			else
			{
			StartCoroutine(delegateAction (pos, 2));
			//                data.data = MOVEType.X;
			//                delegateAction(data, 1);
			}
			#endif
			//            pos.data = mPosition;
		}
		//        else if (_type == StateType.ATTACK)
		//        {
		//            IPublicData data = new AttackTypeData ();
		//            if (mCrossType % 2 == 0)
		//                data.data = AttackType.CROSS;
		//            else
		//                data.data = AttackType.X;
		//            delegateAction(data, 2);
		//        }
	}

	// 이 스크립트를 제거한다.
	public void DestroyTileAction () {
		GetComponent<Renderer> ().material.color = mColor;
		/*IPublicData data = new AttackTypeData ();
      data.data = AttackType.CROSS;*/
		Destroy (this);
	}
}
