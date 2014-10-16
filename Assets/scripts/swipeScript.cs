using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class swipeScript : MonoBehaviour
{
	
		// Use this for initialization
		void Start ()
		{
		
		}
	
		private float length = 0;
		private bool SW = false;
		private Vector3 final;
		private Vector3 startpos;
		private Vector3 endpos;
	
		// Update is called once per frame
		void Update ()
		{
		
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {
						final = Vector3.zero;
						length = 0;
						SW = false;
						Vector2 touchDeltaPosition = Input.GetTouch (0).position;
						startpos = new Vector3 (touchDeltaPosition.x, 0, touchDeltaPosition.y);
				}      
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
						SW = true;
				}
		
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Canceled) {
						SW = false;
				}
		
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Stationary) {
						SW = false;
				}
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {
						if (SW) {
								Vector2 touchPosition = Input.GetTouch (0).position;
								endpos = new Vector3 (touchPosition.x, 0, touchPosition.y);
								final = endpos - startpos;
								length = final.magnitude;
								int direction = 0;
								if (endpos.x < startpos.x) {
										direction = -1; 
								} else {
										direction = 1;
								}
								this.gameObject.GetComponent<GameController> ().OnStartSpin (direction, touchPosition.y);
						}
				}   
		}
	
		void OnGUI ()
		{
				//GUI.Box (new Rect (275, 300, 500, 30), "length: " + length);
		}
}



