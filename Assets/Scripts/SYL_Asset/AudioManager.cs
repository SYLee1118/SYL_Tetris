using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	private AudioSource bgmSource;
	private List<AudioSource> sfxSources = new List<AudioSource>();

	public override void Dispose()
	{
		StopAllSounds();
	}

	public void PlayBGM(AudioClip clip)
	{
		if (bgmSource == null)
		{
			bgmSource = gameObject.AddComponent<AudioSource>();
			bgmSource.loop = true;
		}

		if (bgmSource.clip == clip && bgmSource.isPlaying)
		{
			return;
		}

		bgmSource.clip = clip;
		bgmSource.Play();
	}

	public void StopBGM()
	{
		if (bgmSource != null)
		{
			bgmSource.Stop();
		}
	}

	public void PlaySFX(AudioClip clip)
	{
		AudioSource source = null;
		foreach (var sfxSource in sfxSources)
		{
			if (!sfxSource.isPlaying)
			{
				source = sfxSource;
				break;
			}
		}
		if (source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
			sfxSources.Add(source);
		}
		source.clip = clip;
		source.Play();
	}

	public void StopAllSounds()
	{
		StopBGM();
		foreach (var source in sfxSources)
		{
			source.Stop();
		}
	}
}