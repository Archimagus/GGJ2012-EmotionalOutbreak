using UnityEngine;
using System.Collections;

public class ExitButton : MyButton 
{
    void Start()
    {
        if (Application.isWebPlayer)
        {
            GameObject.Destroy(gameObject);
        }
    }
	protected override void Click()
	{
		Debug.Log("Quitting");
		Application.Quit();
	}

	protected override void Update()
	{
		base.Update();

		if (!movedOnce && Time.timeSinceLevelLoad > 3)
		{
			movedOnce = true;
			transform.position = new Vector3(4f, 0.9f, 1f);
		}
	}
}
