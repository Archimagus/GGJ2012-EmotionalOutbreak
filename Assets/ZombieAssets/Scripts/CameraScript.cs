using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private float targetZoom = 100;
    private float currentZoomSpeed;
    public float zoomMax = 250;
	private Vector3 smoothSpeed = Vector3.zero;
    public AudioClip[] HordeSounds;
    int currentHordeIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		int zombieCount = Map.Instance.aliveZombies;
        if (zombieCount < 20)
        {
            if (currentHordeIndex != -1)
            {
                audio.Stop();
            }
            currentHordeIndex = -1;
        }
        else if (zombieCount < 100)
        {
            if (currentHordeIndex != 0)
            {
                audio.Stop();
                audio.clip = HordeSounds[0];
                audio.loop = true;
                audio.Play();
            }

            currentHordeIndex = 0;
        }
        else
        {
            currentHordeIndex = 1;
            if (currentHordeIndex != 1)
            {
                audio.Stop();
                audio.clip = HordeSounds[1];
                audio.loop = true;
                audio.Play();
            }
        }

		Vector3 position = this.transform.position;
		if (!Map.Instance.gameOver)
		{
			targetZoom = Mathf.SmoothDamp(targetZoom, Mathf.Min(100 + (Map.Instance.aliveZombies / 8), 800), ref currentZoomSpeed, 0.2f);

            if (targetZoom > zoomMax)
            {
                targetZoom = zoomMax;
            }
			position.y = targetZoom;
		}
		else
		{
			transform.parent = null;
			position = Vector3.SmoothDamp(position, new Vector3(0, 800, 0), ref smoothSpeed, 1f);
			if (smoothSpeed.magnitude < 1.0f)
			{
				Map.Instance.FullyZoomed = true;
			}
		}

		this.transform.position = position;
	}
}
