using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReelScript : MonoBehaviour {
	public float OffsetX;
	private Vector2 currentOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSpin () {
		Debug.Log ("  -- OnSpin!");
		iTween.ValueTo (gameObject, iTween.Hash ("from", 0.0f, "to", .818181f, "time", 1.5f, "onupdate", "OnTweenUpdate"));
	}

	public void OnTweenUpdate(float value) {
		renderer.material.mainTextureOffset = new Vector2 (value, 0);
	}

	public void OnStopReel (int index) {
		//Stop tweens in this object only.
		iTween.Stop (gameObject);
		float stopX = index * 0.091f;
		Debug.Log ("  ---- OnStopReel!");
		renderer.material.mainTextureOffset = new Vector2 (stopX, 0);
	}
}
