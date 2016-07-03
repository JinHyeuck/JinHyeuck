using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TilePainter
{

	private List<Tile> mList;

	public void init() {
		mList = new List<Tile> ();
	}
    
	public void paintTile (StateType _type) {
		foreach (Tile tile in mList) {
			TileAction ta = tile.gameObject.AddComponent<TileAction> ();//타일매니저로빼고

            ta.init(_type, tile.GetComponent<Renderer>().material.color, tile.getPosition(), tile.getCrossType());
            if (_type == StateType.MOVE || _type == StateType.ATTACK)
            {
                if (tile.getCrossType() % 2 == 0)
                    tile.GetComponent<Renderer>().material.color = new Color(.0f, 255.0f, 0.0f);
                else
                    tile.GetComponent<Renderer>().material.color = new Color(255.0f, 0.0f, 0.0f);
            }
            else
                tile.GetComponent<Renderer>().material.color = new Color(255.0f, 0.0f, 0.0f);
		}

		Reset ();
	}

	public void Add (Tile _tile) {
		mList.Add (_tile);
	}

	private void Reset () {
		mList.Clear ();
	}
}

