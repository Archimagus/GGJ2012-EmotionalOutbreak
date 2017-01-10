using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour
{
	public static Map Instance;

	public int startingZombies = 1;
	public int startingCops;
	public int startingCivilians;
	public int civiliansToRespawn;

	public int aliveZombies;
	public int aliveCivilians;
	public int aliveCops;

	public GameObject ZombieTemplate;
	public GameObject CivilianTemplate;
	public GameObject CopTemplate;

	public GameObject Cursor;
	public GameObject myGui;
	public CameraScript cam;

	public AudioClip[] Stingers;
	private static float lastScream = 0;
	private static float lastStinger = 0;

	public AudioClip[] Ambients;

	public bool gameOver { get; set; }
	public bool FullyZoomed { get; set; }
	void Awake()
	{
		Instance = this;
		Texture2D mapTex = renderer.sharedMaterial.mainTexture as Texture2D;
		Texture2D copy = new Texture2D(mapTex.width, mapTex.height, TextureFormat.RGB24, false);
		copy.SetPixels(mapTex.GetPixels());
		copy.Apply();
		renderer.material.mainTexture = copy;
	}
	// Use this for initialization
	void Start()
	{
		aliveZombies = 0;
		aliveCivilians = 0;
		aliveCops = 0;

		for (int i = 0; i < startingZombies; i++)
		{
			Vector3 pos = Random.insideUnitSphere;
			pos.y = 0;
			pos = pos.normalized * Random.value * 20;
			Instantiate(ZombieTemplate, pos, Quaternion.identity);
			Map.Instance.aliveZombies++;
		}

		for (int i = 0; i < startingCivilians; i++)
		{
			Civilian.Spawn();
		}

	}

	// Update is called once per frame
	void Update()
	{
		if (Map.Instance.aliveZombies == 0)
		{
			myGui.GetComponent<Gui>().gameOver = gameOver = true;
		}
		int requiredCops = startingCops - aliveCops;
		for (int i = 0; i < requiredCops; i++)
		{
			Cop.Spawn();
		}

		if (Time.realtimeSinceStartup - lastStinger > 20)
		{
			lastStinger = Time.realtimeSinceStartup;

			int index = Random.Range(0, 14);
			audio.volume = 0.1f;
			audio.PlayOneShot(Ambients[index]);
		}

		if (Time.realtimeSinceStartup - lastScream > 10)
		{
			lastScream = Time.realtimeSinceStartup;

			int index = Random.Range(0, 13);
			//audio.volume = 0.5f;
			audio.PlayOneShot(Stingers[index]);
		}
	}

}
