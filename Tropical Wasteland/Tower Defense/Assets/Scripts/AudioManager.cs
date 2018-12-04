using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Handles audio mixer groups and sets their volume levels linearly
/// Because audio mixer volume 
/// </summary>
public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioMixer mixer;

	public string masterVolume = "Master";
	public string musicVolume = "Music";
	public string sfxVolume = "SFX";

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);
	}

	/// <summary>
	/// Takes the value of the slider, which is between 0.01 and 1 and is also linear
	/// Turns a the logarithmic volume which decreases to inaudible levels after -20
	///	into a linear representation so if the slider is at half way, the sound will be half as loud
	///	as opposed to completely inaudible.
	/// </summary>
	/// <param name="channelName">The name of the mixing channel to adjust</param>
	/// <param name="volume">The new volume level to convert and set</param>
	private void SetGroupVolume(string channelName, float volume)
	{
		float linearVolume = Mathf.Log(volume) * 20f;
		mixer.SetFloat(channelName, linearVolume);
	}

	public void SetMasterVolume(float volume)
	{
		SetGroupVolume(masterVolume, volume);
		PlayerPrefs.SetFloat(masterVolume, volume);
	}
	public void SetMusicVolume(float volume)
	{
		SetGroupVolume(musicVolume, volume);
        PlayerPrefs.SetFloat(musicVolume, volume);
    }

	public void SetSFXVolume(float volume)
	{
		SetGroupVolume(sfxVolume, volume);
        PlayerPrefs.SetFloat(sfxVolume, volume);
    }

	private void OnDestroy()
	{
		Save();
	}

	public void Save()
	{
		PlayerPrefs.Save();
	}

	public void Load()
	{
		Debug.Log(PlayerPrefs.GetString(masterVolume));
	}
}
