using UnityEngine;
using System.Collections;
using System.IO;

public sealed class ResourceManager {
	static readonly ResourceManager _instance = new ResourceManager ();
	public static ResourceManager instance {
		get {
			return _instance;
		}
	}

	public T Load <T> (string _filename) where T : Object {
		T data = Resources.Load (_filename) as T;
		return data;
	}

	// 안쓰는것들
//	public TextAsset LoadText (string _filename) {
//		TextAsset data = Resources.Load (_filename) as TextAsset;
//		return data;
//	}
//
//	public GameObject LoadPrefab (string _filename) {
//		GameObject data = Resources.Load (_filename) as GameObject;
//		return data;
//	}
//
//	public AudioClip LoadAudio (string _filename) {
//		AudioClip data = Resources.Load (_filename) as AudioClip;
//		return data;
//	}
//
//	public Sprite LoadSprite (string _filename) {
//		Sprite data = Resources.Load (_filename) as Sprite;
//		return data;
//	}

}

