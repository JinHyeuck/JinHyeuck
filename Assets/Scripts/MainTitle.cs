using UnityEngine;
using System.Collections;

public class MainTitle : MonoBehaviour {


	void Update()
	{
		if (Input.GetMouseButton (0)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("lobby");
		}
	}


}
