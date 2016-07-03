using UnityEngine;
using System.Collections;

public class LobbyManager : MonoBehaviour {

	private static LobbyManager mInstance;
	private static GameObject mContainer;

	private Camera mCamera;

	private LobbyActionButton mLobbyActionButton;

	//private BoxCollider2D mfriendScrollerCollider;

	public static LobbyManager instance {
		get {
			if(!mInstance) {
				mContainer = new GameObject("LobbyManager");
				mInstance = mContainer.AddComponent<LobbyManager> ();
			}
			return mInstance;
		}
	}


	public void init()
	{
		
		mCamera = GameObject.Find ("Camera").GetComponent<Camera> ();
	
		UIManager.instance.init (UIType.LOBBY);
	}

	public void shootRay()
	{
		//int layerMask = 1 << LayerMask.NameToLayer ("UI");
		//layerMask = ~layerMask;
		Ray ray = mCamera.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 30.0f))//, layerMask)) 
		{
			print (hit.transform.name);
			try
			{
			ButtonGrid bg = hit.transform.gameObject.GetComponent<ButtonGrid> ();
			LobbyActionButton.instance.actionGridButton (bg.getbuttonType (), bg.getbuttonNum());
			
			}
			catch 
			{
				print ("return");
				return;
			}
		}
		Debug.Log ("shootRay");
	}
}
