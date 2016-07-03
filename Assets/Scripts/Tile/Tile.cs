using UnityEngine;
using System.Collections;

public enum TILETYPE{LAND = 0,FOREST, RIVER, MOUNTIN, DUNGEON };

public class Tile : MonoBehaviour {
	private int type;
	private Renderer m_Renderer;
	private TileAdress mAdress;

	private int mCrosstype;

	public Vector3 position {
		get {
			return transform.position;
		}
	}

	public TileAdress adress
	{
		get
		{
			return mAdress;
		}
	}

	public void init(int _type, TileAdress _adr)
	{
		type = 0;
		m_Renderer = gameObject.GetComponent<Renderer>();
		mAdress = _adr;

		//Select_Type((TILETYPE)_type);
	}

	private void Select_Type(TILETYPE _type)
	{
		switch(_type)
		{
		case TILETYPE.LAND:
			m_Renderer.material.color = new Color(255.0f, 0.0f, 0.0f, 255.0f);
			break;
		case TILETYPE.FOREST:
			m_Renderer.material.color = new Color(255.0f, 255.0f, 0.0f, 255.0f);
			break;
		case TILETYPE.RIVER:
			m_Renderer.material.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
			break;
		case TILETYPE.MOUNTIN:
			m_Renderer.material.color = new Color(0.0f, 255.0f, 255.0f, 255.0f);
			break;
		case TILETYPE.DUNGEON:
			m_Renderer.material.color = new Color(0.0f, 0.0f, 0.0f, 255.0f);
			break;
		}
	}

	public void Delete_me() {Destroy(this.gameObject);}

	public int get_type() { return type; }

	public TileAdress getAdress() { return mAdress; }

	public void setCrossType(int _crosstype) { mCrosstype = _crosstype; } 
	public int getCrossType() { return mCrosstype; }
	public Vector3 getPosition() { return gameObject.transform.position; }
}