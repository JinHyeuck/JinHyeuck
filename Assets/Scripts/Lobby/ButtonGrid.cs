using UnityEngine;
using System.Collections;

public class ButtonGrid : MonoBehaviour {

	private GRIDBUTTON mbuttonType;
	private int mbuttonNum;

	public void Init(GRIDBUTTON _type, int _num)
	{
		mbuttonType = _type;
		mbuttonNum = _num;
	}

	public GRIDBUTTON getbuttonType(){return mbuttonType;}

	public int getbuttonNum(){return mbuttonNum;}
}
