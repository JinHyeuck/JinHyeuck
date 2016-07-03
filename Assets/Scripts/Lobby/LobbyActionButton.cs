using UnityEngine;
using System.Collections;

public class LobbyActionButton : LobbyInfo {

	private static LobbyActionButton mInstance;
	private static GameObject mContainer;

	private GameObject mFriendPopUp;
	private GameObject mSettingPopUP;
	private GameObject mStoreScene;
	private GameObject mCharbookScene;
	private GameObject mCharbookInfo;
	private UIDragScrollView mCharbookUIDragScrollView;
	private GameObject mManageMentScene;


	private CreateGrid mCreateGrid;


	public static LobbyActionButton instance {
		get {
			if(!mInstance) {
				mContainer = new GameObject("LobbyActionButton");
				mInstance = mContainer.AddComponent<LobbyActionButton> ();
			}
			return mInstance;
		}
	}

	public void Init()
	{
		GameObject[] gm = GameObject.FindGameObjectsWithTag ("Grid") as GameObject[];
		for (int i = 0; i < gm.Length; i++) {
			mCreateGrid = gm [i].GetComponent<CreateGrid> ();
			mCreateGrid.Init ();
		}


			
		addGameObject ();
		mFriendPopUp.SetActive (false);
		mStoreScene.SetActive (false);
		mCharbookScene.SetActive (false);
		mCharbookInfo.SetActive (false);
		mSettingPopUP.SetActive (false);
	}


	private void addGameObject()
	{
		mFriendPopUp = GameObject.Find ("FriendPopUp");
		mStoreScene = GameObject.Find ("storeScene");
		mCharbookScene = GameObject.Find ("charbookScene");
		mCharbookInfo = GameObject.Find ("CharInfo");
		mSettingPopUP = GameObject.Find ("SettingPopUp");

		mCharbookUIDragScrollView = mCharbookScene.GetComponent<UIDragScrollView> ();
	}

	public void UIAction(LOBBYBUTTON _type)
	{
		switch (_type) {
		case LOBBYBUTTON.FRIEND:
			mFriendPopUp.SetActive (true);
			break;
		case LOBBYBUTTON.STORE:
			mStoreScene.SetActive (true);
			break;
		case LOBBYBUTTON.CHARBOOK:
			mCharbookScene.SetActive (true);
			break;
		case LOBBYBUTTON.SETTING:
			mSettingPopUP.SetActive (true);
			break;
		case LOBBYBUTTON.VSRANK:
			UnityEngine.SceneManagement.SceneManager.LoadScene ("fight");
			break;
		}


		Debug.Log ("LobbyActionButtonUIAction");
		Debug.Log (_type);
	}

	public void ExitAction(EXITBUTTON _type)
	{
		switch (_type) 
		{
		case EXITBUTTON.FRIEND:
			
			mFriendPopUp.SetActive (false);	
			break;
		case EXITBUTTON.STORE:
			mStoreScene.SetActive (false);
			break;
		case EXITBUTTON.CHARBOOK:
			if (mCharbookInfo.activeSelf) 
			{
				mCharbookInfo.SetActive (false);
				mCharbookUIDragScrollView.enabled = true;
				return;
			}
			mCharbookInfo.SetActive (false);
			mCharbookScene.SetActive (false);
			break;
		case EXITBUTTON.SETTING:
			mSettingPopUP.SetActive (false);
			break;
		}
	

		Debug.Log ("LobbyActionButtonExitAction");
		Debug.Log (_type);
	}

	public void actionGridButton(GRIDBUTTON _type, int _num)
	{
		switch (_type) 
		{
		case GRIDBUTTON.STORE:
			actionStoreGridButton (_num);
			break;
		case GRIDBUTTON.CHARBOOK:
			actionCharbookGridButton (_num);
			break;
		}
	}

	private void actionStoreGridButton(int _num)
	{
		switch (_num) 
		{
		case 1: 
			mLobbyInfoData.gold -= 100;
			//mLobbyInfoData.setGold (100);
			break;
		case 2: 
			mLobbyInfoData.gold -= 200;
			//mLobbyInfoData.setGold (200);
			break;
		case 3: 
			mLobbyInfoData.gold -= 400;
			//mLobbyInfoData.setGold (400);
			break;
		case 4: 
			mLobbyInfoData.gold -= 300;
			//mLobbyInfoData.setGold (300);
			break;
		}
		UIManager.instance.ui.updated ();
	}

	private void actionCharbookGridButton(int _num)
	{
		UISprite uisprite = mCharbookInfo.transform.FindChild("charsprite").GetComponent<UISprite> ();
		uisprite.spriteName = "UI_bookpro00" + _num.ToString();
		mCharbookInfo.SetActive (true);
		mCharbookUIDragScrollView.enabled = false;
	}
}
