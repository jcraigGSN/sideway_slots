using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextFieldScript : MonoBehaviour {
	private TextMesh M_TextField;

	// Use this for initialization
	void Start () {
		M_TextField = this.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setValue(int value) {
		if (value > 0) {
				M_TextField.text = value.ToString ();
		} else {
				M_TextField.text = "";
		}
	}
}
