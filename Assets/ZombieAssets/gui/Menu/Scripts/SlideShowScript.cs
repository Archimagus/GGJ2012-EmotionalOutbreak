using UnityEngine;
using System.Collections;

public class SlideShowScript : MonoBehaviour
{
    public Material slideShowMaterial;
    public Texture[] slideShowImages;
    /// <summary>
    /// The time when the respective texture finishes.
    /// </summary>
    public float[] textureChangeTimes;
    public bool destroyWhenFinished = true;

    public int textureIndex;

	public float texSwitchTime;
	// Use this for initialization
	void Start () {
        textureIndex = 0;
        slideShowMaterial.SetTexture("_MainTex", slideShowImages[textureIndex]);
		texSwitchTime = Time.timeSinceLevelLoad;
	}
	public void NextSlide()
	{
                textureIndex++;
                if (textureIndex >= slideShowImages.Length)
                {
					if (destroyWhenFinished)
					{
						Application.LoadLevel(1);
					}
                }
                else
                {
					slideShowMaterial.SetTexture("_MainTex", slideShowImages[textureIndex]);
					texSwitchTime = Time.timeSinceLevelLoad;
                }
	}
	// Update is called once per frame
	protected virtual void Update () 
    {
        if (textureIndex < slideShowImages.Length)
        {
            if (textureChangeTimes[textureIndex] != -1 && Time.timeSinceLevelLoad-texSwitchTime > textureChangeTimes[textureIndex])
            {
				NextSlide();
            }
        }
	}
}
