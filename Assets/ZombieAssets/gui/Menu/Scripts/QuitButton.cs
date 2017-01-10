using UnityEngine;
using System.Collections;

public class QuitButton : MyButton 
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
		Application.Quit();
	}
}
