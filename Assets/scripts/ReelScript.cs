using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReelScript : MonoBehaviour {
	public float OffsetX;
	private Vector2 currentOffset;
	private GameObject spawnedSymbol1, spawnedSymbol2, spawnedSymbol3;

	public GameObject[] symbols = new GameObject[6];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSpin () {
		//Clear old symbosl
		if (spawnedSymbol1)
			DestroyObject (spawnedSymbol1);
		if (spawnedSymbol2)
			DestroyObject (spawnedSymbol2);
		if (spawnedSymbol3)
			DestroyObject (spawnedSymbol3);

		iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", .818181f, "time", 1.5f, "onupdate", "OnTweenUpdate"));
	}

	public void OnTweenUpdate(float value) {
		renderer.material.mainTextureOffset = new Vector2 (value, 0);
	}

	public void OnStopReel (List<string> Result) {
		Debug.Log (" reelSymbols " + Result[0] + "," + Result[1] + "," + Result[2]);
		//Stop tweens in this object only.
		iTween.Stop (gameObject);
		float stopX = 2 * 0.091f;
		renderer.material.mainTextureOffset = new Vector2 (stopX, 0);

		
		//Create new symbolto place.
		GameObject tempSymbol1 = symbols [ convertSymbolString(Result[0])]as GameObject;
		spawnedSymbol1 = GameObject.Instantiate(tempSymbol1, new Vector3 (transform.localPosition.x - 2, transform.localPosition.y, -3), Quaternion.identity) as GameObject;
		GameObject tempSymbol2 = symbols [ convertSymbolString(Result[1])] as GameObject;
		spawnedSymbol2 = GameObject.Instantiate(tempSymbol2, new Vector3 (transform.localPosition.x, transform.localPosition.y, -3), Quaternion.identity) as GameObject;
		GameObject tempSymbol3 = symbols [ convertSymbolString(Result[2])] as GameObject;
		spawnedSymbol3 = GameObject.Instantiate(tempSymbol3, new Vector3 (transform.localPosition.x + 2, transform.localPosition.y, -3), Quaternion.identity) as GameObject;
	}

	protected void clearStillSymbols() {

	}

	protected int convertSymbolString(string value) {
		switch (value) {
			case "A":
					return 0;
					break;
			case "B":
					return 1;
					break;
			case "C":
					return 2;
					break;
			case "D":
					return 3;
					break;
			case "E":
					return 4;
					break;
			case "J":
					return 5;
					break;
			default: return 1; break;
			}
	}
}
