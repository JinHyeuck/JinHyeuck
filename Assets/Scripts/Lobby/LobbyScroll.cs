using UnityEngine;
using System.Collections;
public enum SCROLLTYPE{FRIEND = 0, CHARBOOk}
public enum SCROLLDIRECTION{HEIGHT = 0, WIDTH}

public class LobbyScroll : MonoBehaviour 
{
	private SCROLLTYPE mScrollType;
	private SCROLLDIRECTION mScrollDirecton;

	public void init(SCROLLTYPE _scrolltype, SCROLLDIRECTION _scrolldirection)
	{
		mScrollType = _scrolltype;
		mScrollDirecton = _scrolldirection;
	}
}
