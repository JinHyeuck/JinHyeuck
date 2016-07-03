using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateGrid : LobbyInfo {
	[SerializeField]
	private GameObject mfriendGrid;
	[SerializeField]
	private GameObject mcharbook;
	[SerializeField]
	private GameObject mstore;
	[SerializeField]
	private GameObject mscharbookscreen;

	private string[] mstoreLabel;
	private string[] mstoreprice;

	public void Init()
	{
		mstoreLabel = new string[4]{ "저가형 완제품", "일반 완제품", "프리미엄 완제품", "랜덤 완제품"};
		mstoreprice = new string[4]{ "100", "200", "400", "300"};
			

		if (this.gameObject.name == "FriendGrid") {
			for (int i = 0; i < friend_list.Length; i++) {
				createFriendGrid (new Vector3 (100.0f, 150.0f + (i * -100.0f), 0.0f), i);
			}
		} 

		else if(this.gameObject.name == "StoreGrid")
		{
			int number = 1;
			for (int i = 0; i < 4; i++) 
			{
				createStoreGrid (new Vector3 (-100.0f + (1000.0f * i), 100.0f, 0.0f), number);
				number++;
			}
		}

		else if(this.gameObject.name == "CharbookGrid")
		{
			int toytype = 0;
			for (int y = 0; y < 5; y++) 
			{
				for (int x = 0; x < 4; x++) 
				{
					createCharbook (new Vector3 (-200.0f + (200.0f * x), 50.0f + (-200.0f * y), 0.0f), toytype);
					toytype++;
				}
			}
		}
	}

	private void createCharbook(Vector3 _pos, int _type)
	{
		GameObject clone = Instantiate (mcharbook) as GameObject;
		clone.transform.parent = transform;
		clone.transform.localPosition = _pos;
		clone.transform.localScale = new Vector3 (1.8f, 1.8f, 1.0f);
		clone.name = _type.ToString ();




		if (_type >= toy_list.Length) 
		{
			GameObject clonescreen = Instantiate (mscharbookscreen) as GameObject;
			clonescreen.transform.parent = transform;
			clonescreen.transform.localPosition = _pos + new Vector3(0.0f, 0.0f, -1.0f);
			clonescreen.transform.localScale = new Vector3 (1.8f, 1.8f, 1.0f);
			_type = 0;
		}

		UISprite uisprite = clone.GetComponent<UISprite> ();
		uisprite.spriteName = "UI_getnum00" + _type.ToString();



		ButtonGrid bg = clone.GetComponent<ButtonGrid> ();
		bg.Init (GRIDBUTTON.CHARBOOK, _type);

			
	}

	private void createStoreGrid(Vector3 _pos, int _type)
	{
		GameObject clone = Instantiate (mstore) as GameObject;
		clone.transform.parent = transform;
		clone.transform.localPosition = _pos;
		clone.transform.localScale = new Vector3 (1.4f, 1.6f, 1.0f);

		UILabel storeLabel = clone.transform.FindChild ("storeLabel").GetComponent<UILabel> ();
		storeLabel.text = mstoreLabel[_type-1];
		UILabel storeprice = clone.transform.FindChild ("storeprice").GetComponent<UILabel> ();
		storeprice.text = mstoreprice[_type-1];

		UISprite uisprite = clone.GetComponent<UISprite> ();
		uisprite.spriteName = "IC_item" + _type.ToString();



		ButtonGrid bg = clone.GetComponent<ButtonGrid> ();
		bg.Init (GRIDBUTTON.STORE, _type);



	}

	private void createFriendGrid(Vector3 _pos, int _type)
	{
		GameObject clone = Instantiate (mfriendGrid) as GameObject;
		clone.transform.parent = transform;
		clone.transform.localPosition = _pos;
		clone.transform.localScale = new Vector3 (3.0f, 0.4f, 1.0f);

		UILabel cloneRank = clone.transform.FindChild ("FriendRanking").GetComponent<UILabel> ();
		cloneRank.text = (_type+1).ToString();
		UILabel clonename = clone.transform.FindChild ("FriendName").GetComponent<UILabel> ();
		clonename.text = friend_list [_type].name;
		UILabel cloneRankgrade = clone.transform.FindChild ("FriendRankgrade").GetComponent<UILabel> ();
		cloneRankgrade.text = friend_list [_type].rankgrade.ToString();
		UILabel clonepoint = clone.transform.FindChild ("FriendRankpoint").GetComponent<UILabel> ();
		clonepoint.text = friend_list [_type].rankpoint.ToString();

		//UISprite uisprite = clone.GetComponent<UISprite> ();
		//uisprite.spriteName = "KakaoTalk_20160602_023925590";
	}


}
