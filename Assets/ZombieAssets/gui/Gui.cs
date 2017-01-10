using UnityEngine;
using System.Collections;
using System.Timers;

public class Gui : MonoBehaviour
{
	public Texture endGame;
	public Texture greenCheck;
	public Texture redX;
	public GameObject playAgainButton;
	public GameObject mainMenuButton;

	public bool gameOver = false;
	private bool soundOn = true;
	public Rect SoundButtonRect { get; set; }

	private GUIStyle guiTextStyle;
	void Start()
	{
		guiTextStyle = new GUIStyle();
		guiTextStyle.fontSize = 32;

		var sound = PlayerPrefs.GetString("Sound", "On");
		soundOn = sound == "On";
		if (!soundOn)
		{
			SetSound();
		}
	}

	void OnGUI()
	{
	
		SoundButtonRect = new Rect(40, 0, 20, 20);

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("MainMenu");
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			Application.LoadLevel("map");
		}
		if (!Map.Instance.gameOver)
		{
			Rect labelRect = new Rect(10, 10, Screen.width * 0.3f, Screen.height * 0.3f);
			labelRect.x = labelRect.xMax + 10;
			GUI.Label(labelRect, "Remaining Civilians: " + (Map.Instance.aliveCivilians + Map.Instance.civiliansToRespawn), guiTextStyle);
		}
		if (Map.Instance.FullyZoomed)
		{		
			Texture tex = endGame;
			
			var aspect = tex.width / (float)tex.height;
			var height = (int)Mathf.Min(Screen.height * 0.85f, tex.height);
			var width = (int)(height * aspect);
			Rect labelRect = new Rect(Screen.width / 2 - width / 2, 30, width, height);
			//GUI.Label(labelRect, tex);
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(labelRect.center.x, Screen.height-labelRect.yMax-20, 10f));
			playAgainButton.transform.position = pos - Vector3.right*4;
			mainMenuButton.transform.position = pos + Vector3.right*4;
		}
		if (soundOn)
		{
			if (GUI.Button(SoundButtonRect, greenCheck))
			{
				PlayerPrefs.SetString("Sound", "Off");
				soundOn = false;
				SetSound();
			}
		}
		else
		{
			if (GUI.Button(SoundButtonRect, redX))
			{
				PlayerPrefs.SetString("Sound", "On");
				soundOn = true;
				SetSound();
			}
		}
		GUI.Label(new Rect(0, 0, 50, 20), "Sound");
	}

	private void SetSound()
	{
		var audioSources = FindObjectsOfType<AudioSource>();
		foreach (var source in audioSources)
		{
			source.mute = !soundOn;
		}
	}

}
