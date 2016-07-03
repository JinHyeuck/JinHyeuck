using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// 전투 씬의 UI들을 관리
public class UIBattle : MonoBehaviour, IUserInterface
{
	private StaticEventDelegate.Rvoid_Vint1 action_void_int;
	private StaticEventDelegate.Rvoid_Void action_void_void;
	private StaticEventDelegate.Rvoid_Vtype1 action_void_type;

	#region IAction
	public interface IAction {
		void updated ();
	}

	// 추후 제네릭으로 수정필요
	public class UIData_Label : IAction {
		private UILabel ui;
		private StaticEventDelegate.Rint_Void action;

		public UIData_Label(UILabel _ui, StaticEventDelegate.Rint_Void _action) {
			ui = _ui;
			action = _action;
		}

		public void updated () {
			ui.text = action ().ToString();
		}
	}

	//   public class UIData_Sprite

	public class UIData_Slider : IAction {
		private UISlider ui;
		private StaticEventDelegate.Rint_Void action;

		public UIData_Slider(UISlider _ui, StaticEventDelegate.Rint_Void _action) {
			ui = _ui;
			action = _action;

			//         updated ();
			// 임시코드
			//         ui.gameObject.AddComponent<test> ();
			//         ui.gameObject.GetComponent<test> ().bindCharacter (ArbiterManager.instance.arbiter.player);
		}

		public void updated () {
			ui.value = (float)action () / 50.0f;
		}
	}
	#endregion

	//   private delegate void UIAction1();
	//   private delegate void UIAction2(int _value);
	//   private UIAction1 ui_action1;
	//   private UIAction2 ui_action2;

	private List<IAction> mUI_CharInfo;
	private delegate void UIAction ();
	private UIAction action;

	#region inheritance
	public void init () {
		// 테스트중
		mUI_CharInfo = new List<IAction> ();

		create ();
		updated ();
		Debug.Log ("init UIBattle");
	}

	public void updated () {
		foreach (IAction ui in mUI_CharInfo) {
			ui.updated ();
		}

		Debug.Log ("update UIBattle");
	}

	public void create () {

		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_player_hp").GetComponent<UILabel> (), GameObject.Find("Player").GetComponent<Player> ().getStamina));
		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_player_cost").GetComponent<UILabel> (), GameObject.Find ("Player").GetComponent<Player> ().getCost));
		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_player_damage").GetComponent<UILabel> (), GameObject.Find ("Player").GetComponent<Player> ().getDamage));
		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_player_skill_damage").GetComponent<UILabel> (), GameObject.Find ("Player").GetComponent<Player> ().getSkillDamage));

		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_rival_hp").GetComponent<UILabel> (), GameObject.Find("Rival").GetComponent<Rival> ().getStamina));
		//      mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_rival_cost").GetComponent<UILabel> (), GameObject.Find("Rival").GetComponent<Rival> ().getStamina));
		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_rival_damage").GetComponent<UILabel> (), GameObject.Find("Rival").GetComponent<Rival> ().getDamage));
		mUI_CharInfo.Add (new UIData_Label (GameObject.Find ("label_rival_skill_damage").GetComponent<UILabel> (), GameObject.Find("Rival").GetComponent<Rival> ().getSkillDamage));

		//      mUI_CharInfo.Add (new UIData_Slider (GameObject.Find ("slider_player_hp").GetComponent<UISlider> (), GameObject.Find ("Player").GetComponent<Player> ().getStamina));



		//      bindButton ("button_action_attack", StateType.ATTACK);
		//      bindButton ("button_action_move", StateType.MOVE);
		//      bindButton ("button_action_defend", StateType.DEFENSE);
		//      bindButton ("button_action_hp", StateType.HEAL);
		//      bindButton ("button_action_skill", StateType.SKILL);

		GameObject.Find ("player_picture").GetComponent<UISprite> ().spriteName = findSprite (ArbiterManager.instance.arbiter.player.toyType);
		GameObject.Find ("rival_picture").GetComponent<UISprite> ().spriteName = findSprite (ArbiterManager.instance.arbiter.rival.toyType);


		bindButton ("button_action_move", bMove);
		bindButton ("button_action_defend", bDefense);
		bindButton ("button_action_hp", bRestoreHP);
		bindButton ("button_action_skill", bSkill);

//		bindButton ("button_undergo_attack", action_void_int = ArbiterManager.instance.arbiter.player.undergoAttack, ArbiterManager.instance.arbiter.player.charInfo.damage);


		// 이부분 수정필요
		//      bindButton ("button_action_attack", action_void_int = ArbiterManager.instance.doAction, 2);
		//      bindButton ("button_action_attack", action_void_void = , action_void_int = ArbiterManager.instance.arbiter.player.attack);
		//      bindButton ("button_action_attack", action_void_type = 
		//      bindButton ("button_action_skill", action_void_int = ArbiterManager.instance.arbiter.player.useSkill, (int)SkillType.CROSS);
		//      bindButton ("button_action_move", action_void_int = ArbiterManager.instance.arbiter.player.move, (int)StateType.MOVE);
		//


		bindButton ("button_popup_exchange", this.exchangePopup);

		// concealable ui
		// inconcealable ui
		Debug.Log ("create UIBattle");
	}
	#endregion

	#region behaviour
	//   private void bindButton(string _name, StaticEventDelegate.Rvoid_Void _action) {
	//      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate (_action));
	//   }

	private void bindButton(string _name, StaticEventDelegate.Rvoid_Vint1 _action, int _value) {
		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate (_action, _value));
		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate (new StaticEventDelegate.Rvoid_Void (this.updated)));
	}

	//   private void bindButton(string _name, StaticEventDelegate.Rvoid_Vtype1 _action, StateType _type) {
	////      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate<typeof(_action)> (_action, _type));
	//      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate<StaticEventDelegate.Rvoid_Vtype1> (_action, _type));
	//   }

	private void bindButton(string _name, StateType _type) {
		//      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (
		//         StaticEventDelegate.instance.makeEventDelegate<StaticEventDelegate.Rvoid_Vtype1> (ArbiterManager.instance.doAction, _type)
		//      );
		EventDelegate dele = new EventDelegate(ArbiterManager.instance, "doAction");
		dele.parameters [0] = new EventDelegate.Parameter (_type);

		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (dele);
	}

	private void bindButton(string _name, string _type) {
		//      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (
		//         StaticEventDelegate.instance.makeEventDelegate<StaticEventDelegate.Rvoid_Vtype1> (ArbiterManager.instance.doAction, _type)
		//      );
		EventDelegate dele = new EventDelegate(ArbiterManager.instance, "doAction");
		dele.parameters [0] = new EventDelegate.Parameter (_type);

		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (dele);
	}

	private void bindButton(string _name, UIAction _action) {
		EventDelegate dele = new EventDelegate (this, _action.Method.Name);
		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (dele);
	}

	//   private void bindButton(string _name, StaticEventDelegate.Rvoid_Void _action, int _value) {
	//      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate (_action));
	//   }

	//   private void bindButtonAndTileAction(string _name, StaticEventDelegate.Rvoid_Vint1 _action) {
	//      GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (StaticEventDelegate.instance.makeEventDelegate (new StaticEventDelegate.Rvoid_Void (this.action_void_int)
	//   }

	private void exchangePopup() {
		GameObject go = GameObject.Find ("Panel_action");
		Vector3 pos = new Vector3 (.0f, 200.0f, .0f);

		if (go.transform.localPosition.y < -300.0f)
			go.transform.localPosition += pos;
		else
			go.transform.localPosition -= pos;
	}
	#endregion

	private void bMove () {
		if (ArbiterManager.instance.arbiter.player.getCost () > 0) {
			TileManager.instance.setPaintedTile (ArbiterManager.instance.arbiter.player.adress, StateType.MOVE);
			TileManager.instance.paintTile (StateType.MOVE);
		}
	}

	private void bAttack () {
		TileManager.instance.setPaintedTile (ArbiterManager.instance.arbiter.player.adress, StateType.ATTACK);
		TileManager.instance.paintTile (StateType.ATTACK);
	}

	private void bDefense () {
		//      ArbiterManager.instance.doAction (StateType.DEFENSE, 
	}

	private void bRestoreHP () {
	}

	private void bRestoreCost () {
	}

	private void bSkill () {
	}

	private string findSprite(ToyType toy_type) {
		string toy_name;

		switch (toy_type) {
		case ToyType.FLOWER:
			toy_name = "flower";
			break;
		case ToyType.LION:
			toy_name = "lion";
			break;
//		case ToyType.MOUSE:
//			toy_name = "mouse";
//			break;
//		case ToyType.NORU:
//			toy_name = "noru";
//			break;
		case ToyType.PENGUIN:
			toy_name = "penguin";
			break;
		case ToyType.PIG:
			toy_name = "pig";
			break;
//		case ToyType.PIG2:
//			toy_name = "pig";
//			break;
//		case ToyType.SNAKE:
//			toy_name = "snake";
//			break;
		default:
			toy_name = "flower";
			break;
		}
		return toy_name;
	}

	//   private void initListFromFile2 (out List<IAction> _list, string _filename) {
	//      if (_list == null) {
	//         _list = new List<IAction> ();
	//      }
	//
	//      StringReader sr = new StringReader (ResourceManager.instance.Load<TextAsset> (_filename).text);
	//
	//      while (sr.Peek () != -1) {
	//         _list.Add (new IAction (GameObject.Find (sr.ReadLine()).GetComponent<Text> ().text, 0));
	//      }
	//
	//      sr.Close ();
	//   }



	//   private void initListFromFile<T>(out List<T> _list, string _filename) {
	//      StringReader sr = new StringReader (ResourceManager.instance.Load<TextAsset> (_filename).text);
	//      _list = new List<T> ();
	//
	//      while (sr.Peek () != -1) {
	//         _list.Add (GameObject.Find (sr.ReadLine ()).GetComponent<T> ());
	//      }
	//      sr.Close ();
	//   }
}