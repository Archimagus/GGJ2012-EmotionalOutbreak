using UnityEngine;
using System.Collections;

public class CreditsButtonScript
    : MyButton {
    protected override void Click()
    {
            Camera.main.transform.position = new Vector3(0, 8, 15);
    }

	protected override void Update()
	{
		base.Update();

		if (!movedOnce && Time.timeSinceLevelLoad > 3)
		{
			movedOnce = true;
			transform.position = new Vector3(1.6f, 0.9f, 1f);
		}
	}
}
