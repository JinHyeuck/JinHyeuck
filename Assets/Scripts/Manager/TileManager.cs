using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileAdress {
	public int x, y;

	public TileAdress() {}
	public TileAdress(int _x, int _y) {
		x = _x;
		y = _y;
	}
}

public class TileManager : MonoBehaviour 
{
	private static TileManager mInstance = null;
	private static GameObject mContainer;

	public static TileManager instance {
		get {
			if (mInstance == null) {
				mContainer = new GameObject ("TileManager");
				mInstance = mContainer.AddComponent<TileManager> ();
			}
			return mInstance;
		}
	}

	public struct TILEDATA
	{
		private TILETYPE _type;
		private Vector3 _pos;
		private TileAdress _adress;

		public TILEDATA(TILETYPE _type, Vector3 _pos, TileAdress _adress)
		{
			this._type = _type;
			this._pos = _pos;
			this._adress = _adress;
		}

		public TILETYPE GetType(){ return this._type; }
		public Vector3 GetPosition() { return this._pos; }
		public TileAdress GetPath() { return this._adress; }
	}

	public List<GameObject> mTile;
	private List<GameObject> mModel;

	public List<Tile> mTileList;
	public TILEDATA[,] mTileData;

	private int mWidth;
	private int mHeight;

	private TilePainter mPainter;

	public Vector3 leftSpawnPos {
		get {
			return mTileData [mHeight / 2, 1].GetPosition ();
			//return mTileData[0, 0].GetPosition();
		}
	}

	public Vector3 rightSpawnPos {
		get {
			return mTileData [mHeight / 2, mWidth-2].GetPosition ();
		}
	}

	public TileAdress leftSpawnAdress {
		get {
			return mTileData [mHeight / 2, 1].GetPath ();
		}
	}

	public TileAdress rightSpawnAdress {
		get {
			return mTileData [mHeight / 2, mWidth - 2].GetPath ();
		}
	}

	public bool init() {
		mTile = new List<GameObject>();
		mModel = new List<GameObject>();

		for (int i = 0; i < 5; i++)
		{
			mTile.Add(ResourceManager.instance.Load<GameObject>("Prefabs/Tile/Tile" + i.ToString()));

			mModel.Add(ResourceManager.instance.Load<GameObject>("Prefabs/Tile/" + i.ToString()));
		}
		createAllTileInFile ("data/testtile");

		mPainter = new TilePainter ();
		mPainter.init ();

		return true;
	}

	private bool createAllTileInFile(string _filename) {
		StringReader strReader = new StringReader (ResourceManager.instance.Load<TextAsset>(_filename).text);

		string str = strReader.ReadLine ();
		string[] strArr = str.Split (',');

		mWidth = System.Convert.ToInt32 (strArr [0]);
		mHeight = System.Convert.ToInt32 (strArr [1]);

		mTileList = new List<Tile>(mWidth * mHeight);
		mTileData = new TILEDATA[mHeight, mWidth];

		int count = 0;

		for (int i = 0; i < mHeight; ++i) {
			int type;

			while ((type = strReader.Read()) != -1) {
				if (System.Convert.ToChar (type) != ',' && type != 10 && type != 13) {
					type -= 48;
					int x = count % mWidth;
					int y = count / mWidth;
					//Vector3 pos = new Vector3 ((-mWidth / 2) + x, (mHeight / 2) + y);
					Vector3 pos = new Vector3 ((-mWidth / 2) + x, 0, (mHeight / 2) + y);

					mTileData [y, x] = new TILEDATA ((TILETYPE)type, pos, new TileAdress (x, y));
					createTile (pos, type, new TileAdress (x, y));
					++count;
				}
			}
		}

		strReader.Close ();

		return true;
	}

	private void createTile(Vector3 _pos, int _type, TileAdress _adr)
	{
		GameObject cloneModel = Instantiate(mModel[_type]) as GameObject;
		cloneModel.transform.parent = transform;
		cloneModel.transform.position = _pos;

		GameObject clone = Instantiate(mTile[_type]) as GameObject;
		clone.transform.parent = transform;
		clone.transform.position = _pos + new Vector3(0.0f, -0.2f, 0.0f);
		clone.AddComponent<Tile>();
		Tile tile = clone.GetComponent<Tile>();
		tile.init(_type, _adr);
		mTileList.Add(tile);


	}

	//   public void setPaintedTile(Vector3 _pos, StateType _type) {
	//        /*IPublicData data = new StateTypeData();
	//        data.data = _type;*/
	//        
	//      Tile tile = findStandardTile (_pos);
	//      int x = tile.adress.x;
	//      int y = tile.adress.y;
	//
	//        int crosstype = 0;
	//        Tile findtile;
	//      // rect
	//      if ((_type == StateType.ATTACK) || (_type == StateType.MOVE)) 
	//        {
	//            
	//         for (int i = -1; i <= 1; ++i) {
	//            for (int j = -1; j <= 1; ++j) {
	//                    crosstype++;
	//               if ((x + i < 0) || (y + j < 0))
	//                  continue;
	//               /*else if ((x + i >= mWidth) || (y + j >= mHeight))
	//                  continue;*/
	//               //mPainter.Add (findAdressByTile (x + i, y + j));
	//                    /*mPainter.Add(findAdressByTile(x + i, y + j));
	//                    findAdressByTile(x + i, y + j).setCrossType(crosstype);*/
	//                    findtile = findAdressByTile(x + i, y + j);
	//                    mPainter.Add(findtile);
	//                    findtile.setCrossType(crosstype);
	//                    /*TileAction ta = findAdressByTile(x + i, y + j).gameObject.AddComponent<TileAction>();
	//                    ta.init(_type, tile.GetComponent<Renderer>().material.color, mTileData[x + i, y + j].GetPosition(), crosstype);*/
	//            }
	//         }
	//      }
	//
	//      // X
	//      else if (_type == StateType.SKILL) {
	//            
	//         for (int i = -2; i <= 2; ++i) {
	//            for (int j = -2; j <= 2; ++j) {
	//               if ((x + i < 0) || (y + j < 0))
	//                  continue;
	//               else if ((x + i >= mWidth) || (y + j >= mHeight))
	//                  continue;
	//
	//               // X
	//               if (((x + i + y + j) % 2 == (x + y) % 2) && ((x-i != x) && (y-j != y)))
	//                  mPainter.Add (findAdressByTile (x + i, y + j));
	//            }
	//         }
	//            mPainter.Add (findAdressByTile (x, y));  
	//      }
	//
	//   }

	public void setPaintedTile(TileAdress _adress, StateType _type) {
		/*IPublicData data = new StateTypeData();
        data.data = _type;*/

		//      Tile tile = findStandardTile (_pos);
		Tile tile = findAdressByTile (_adress.x, _adress.y);
		int x = tile.adress.x;
		int y = tile.adress.y;

		int crosstype = 0;
		Tile findtile;
		// rect
		if ((_type == StateType.ATTACK) || (_type == StateType.MOVE)) 
		{

			for (int i = -1; i <= 1; ++i) {
				for (int j = -1; j <= 1; ++j) {
					crosstype++;
					if ((x + i < 0) || (y + j < 0))
						continue;
					/*else if ((x + i >= mWidth) || (y + j >= mHeight))
                  continue;*/
					//mPainter.Add (findAdressByTile (x + i, y + j));
					/*mPainter.Add(findAdressByTile(x + i, y + j));
                    findAdressByTile(x + i, y + j).setCrossType(crosstype);*/
					findtile = findAdressByTile(x + i, y + j);
					mPainter.Add(findtile);
					findtile.setCrossType(crosstype);
					/*TileAction ta = findAdressByTile(x + i, y + j).gameObject.AddComponent<TileAction>();
                    ta.init(_type, tile.GetComponent<Renderer>().material.color, mTileData[x + i, y + j].GetPosition(), crosstype);*/
				}
			}
		}

		// X
		else if (_type == StateType.SKILL) {

			for (int i = -2; i <= 2; ++i) {
				for (int j = -2; j <= 2; ++j) {
					if ((x + i < 0) || (y + j < 0))
						continue;
					else if ((x + i >= mWidth) || (y + j >= mHeight))
						continue;

					// X
					if (((x + i + y + j) % 2 == (x + y) % 2) && ((x-i != x) && (y-j != y)))
						mPainter.Add (findAdressByTile (x + i, y + j));
				}
			}
			mPainter.Add (findAdressByTile (x, y));  
		}

	}

	private void actionPainter(int _x, int _y)
	{
		Tile tile = findAdressByTile(_x, _y);
		TileAction ta = tile.gameObject.AddComponent<TileAction>();


	}

	public Vector3 findTilePosition(int _x, int _y) {
		return mTileData [_x, _y].GetPosition ();
	}

	private Tile findAdressByTile(int x, int y) {
		return mTileList[x+y*mWidth];
	}

	public Tile findStandardTile(Vector3 _pos) {
		foreach (Tile tile in mTileList) {
			if (Vector3.Normalize(_pos - tile.transform.position) == Vector3.up) {
				return tile;
			}
		}
		// not found
		return null;
	}

	public void paintTile (StateType _type) {
		if (transform.GetComponentsInChildren<TileAction> ().Length > 0)
			transform.BroadcastMessage ("DestroyTileAction");

		mPainter.paintTile (_type);
	}
	//   public Vector3 findTilePosition(Vector2 _vector) {
	//      return mTileData [(int)_vector.x, (int)_vector.y].GetPosition ();
	//   }
}