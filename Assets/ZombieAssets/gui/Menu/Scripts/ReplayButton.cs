using UnityEngine;
using System.Collections;

public class ReplayButton : MyButton {

	protected override void Click()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
}
