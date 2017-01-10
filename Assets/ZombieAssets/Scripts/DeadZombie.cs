using UnityEngine;
using System.Collections;

public class DeadZombie : MonoBehaviour {

	public Texture[] possibleTextures;

    public AudioClip[] DeathSounds;

    private static float lastScream = 0;
	private int frameCount;
	// Use this for initialization
	void Start () 
	{
		this.renderer.material.mainTexture = possibleTextures[Random.Range(0, possibleTextures.Length)];

        if (Time.realtimeSinceStartup - lastScream > 3)
        {
            lastScream = Time.realtimeSinceStartup;
            int index = Random.Range(0, 4);
            audio.volume = 0.3f;
            audio.PlayOneShot(DeathSounds[index]);
        }
	}
	//void Update()
	//{
	//    //frameCount++;
	//    //if (frameCount > 2)
	//    //{
	//    //    Debug.Log("Dead zombie still here.");
	//    //}
	//    //if (frameCount > 1)
	//    //{
	//    //    GameObject.Destroy(gameObject);
	//    //}
	//}
	
}
