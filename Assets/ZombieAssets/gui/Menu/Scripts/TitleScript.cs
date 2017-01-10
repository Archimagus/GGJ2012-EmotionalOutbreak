using UnityEngine;
using System.Collections;

public class TitleScript : MonoBehaviour {

	bool movedOnce = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!movedOnce && Time.timeSinceLevelLoad > 3)
		{
			movedOnce = true;
			transform.position = new Vector3(0, 0.9f, 3f);
		}
	}
}
