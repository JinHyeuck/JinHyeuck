using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum LOBBYBUTTON{FRIEND = 0, SETTING, VSRANK, VSFRIEND, STORE, CHARBOOK, MANAGEMENT, CHANGELEFT, CHANGERIGHT};
public enum EXITBUTTON{FRIEND = 0, STORE, CHARBOOK, SETTING}
public enum GRIDBUTTON{STORE = 0, CHARBOOK}
public class UILobby : LobbyInfo, IUserInterface
{
	//private StaticEventDelegate.Rvoid_VtypeLobbyButton lobby_button_actoin;
	private List<UILabel> mUILabel;
	//private LobbyInfo mLobbyInfo;

	private string[] mbuttonUIName;
	private string[] mExitName;


	public void init () 
	{ 
		mbuttonUIName = new string[9]{"friend", "setting", "vsrank", "vsfriend", "store", "charbook", "management", "changeleft", "changeright"};
		mExitName = new string[4]{ "Friendexit" , "Storeexit", "Charbookexit", "Settingexit"};
		mUILabel = new List<UILabel> (4);
		base.init();

		addUI ();
		create ();

		LobbyActionButton.instance.Init ();
	}

	public void updated () 
	{
		updateUI ();
	}

	public void create () 
	{
		updateUI ();

		for (int i = 0; i < mbuttonUIName.Length; i++) 
		{
			bindButton (mbuttonUIName[i], (LOBBYBUTTON)i);
		}
		for (int i = 0; i < mExitName.Length; i++) 
		{
			exitButton (mExitName[i], (EXITBUTTON)i);
		}

	}


	private void updateUI()
	{
		//자주 변화되는 UI들
		/*mUILabel [0].text = mLobbyInfo.mLobbyInfoData.level.ToString();
		mUILabel [1].text = mLobbyInfo.mLobbyInfoData.name;
		mUILabel [2].text = mLobbyInfo.mLobbyInfoData.gold.ToString();
		mUILabel [3].text = mLobbyInfo.mLobbyInfoData.rankgrade.ToString();
		mUILabel [4].text = mLobbyInfo.mLobbyInfoData.rankpoint.ToString();*/
		mUILabel [0].text = mLobbyInfoData.level.ToString();
		mUILabel [1].text = mLobbyInfoData.name;
		mUILabel [2].text = mLobbyInfoData.gold.ToString();
		mUILabel [3].text = mLobbyInfoData.rankgrade.ToString();
		mUILabel [4].text = mLobbyInfoData.rankpoint.ToString() + "P";
	}



	private void addUI()
	{
		mUILabel.Add(GameObject.Find ("User_Level").GetComponent<UILabel> ());
		mUILabel.Add(GameObject.Find ("User_Name").GetComponent<UILabel> ());
		mUILabel.Add(GameObject.Find ("User_Gold").GetComponent<UILabel> ());
		mUILabel.Add(GameObject.Find ("User_RankGrade").GetComponent<UILabel> ());
		mUILabel.Add(GameObject.Find ("User_RankPoint").GetComponent<UILabel> ());
	}
		
	private void bindButton(string _name, LOBBYBUTTON _type) {
		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (
			StaticEventDelegate.instance.makeEventDelegate<StaticEventDelegate.Rvoid_VtypeLobbyButton> (LobbyActionButton.instance.UIAction,_type)
		);
	}

	private void exitButton(string _name, EXITBUTTON _type) {
		GameObject.Find (_name).GetComponent<UIButton> ().onClick.Add (
			StaticEventDelegate.instance.makeEventDelegate<StaticEventDelegate.Rvoid_VtypeExitButton> (LobbyActionButton.instance.ExitAction,_type)
		);
	}


}