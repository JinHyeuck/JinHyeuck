using UnityEngine;
using System.Collections;

public class CoordinateSystem
{
	static readonly CoordinateSystem _instance = new CoordinateSystem ();

	public static CoordinateSystem instance {
		get {
			return _instance;
		}
	}
}