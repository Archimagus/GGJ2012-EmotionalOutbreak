using UnityEngine;
using System.Collections;

public class StartTargetButton : MyButton 
{
	//public GameObject chooseraceobject;
	public GameObject []thingsToMove;
	public GameObject slideShow;
	public AudioSource menuMusic;
	public AudioSource outro;
	//public GameObject player1object;
	protected override void Click()
	{
        base.Click();

		foreach (GameObject o in thingsToMove)
		{
			o.transform.position = new Vector3(-15.2f, 1.0f, 1.0f);
		}
        transform.position = new Vector3(-15.0f, 0.9f, 1.0f);
		menuMusic.Stop();
		outro.Play();
		slideShow.GetComponent<SlideShowScript>().NextSlide();
		
        //player1object.transform.position= new Vector3(-20.0f,0.9f,-2.246367f);
				
	}
	protected override void Update()
	{
		base.Update();

		if (!movedOnce && Time.timeSinceLevelLoad > 3)
		{
			movedOnce = true;
			menuMusic.Play();
			transform.position = new Vector3(-3.75f, 0.9f, 1f);
		}
	}
}
