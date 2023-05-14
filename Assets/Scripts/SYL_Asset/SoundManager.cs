using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : Singleton<SoundManager>
{
	private Dictionary<string, AsyncOperationHandle<AudioClip>> loadedClips = new Dictionary<string, AsyncOperationHandle<AudioClip>>();

	public void PlayBGM(string address)
	{
		if (loadedClips.TryGetValue(address, out AsyncOperationHandle<AudioClip> handle))
		{
			AudioManager.it.PlayBGM(handle.Result);
		}
		else
		{
			Addressables.LoadAssetAsync<AudioClip>(address).Completed += handle =>
			{
				loadedClips[address] = handle;
				AudioManager.it.PlayBGM(handle.Result);
			};
		}
	}

	public void StopBGM()
	{
		AudioManager.it.StopBGM();
	}

	public void PlaySFX(string address)
	{
		if (loadedClips.TryGetValue(address, out AsyncOperationHandle<AudioClip> handle))
		{
			AudioManager.it.PlaySFX(handle.Result);
		}
		else
		{
			Addressables.LoadAssetAsync<AudioClip>(address).Completed += handle =>
			{
				loadedClips[address] = handle;
				AudioManager.it.PlaySFX(handle.Result);
			};
		}
	}

	public override void Dispose()
	{
		AudioManager.it.StopAllSounds();
		foreach (var handle in loadedClips.Values)
		{
			if (handle.IsValid())
			{
				Addressables.Release(handle);
			}
		}
		loadedClips.Clear();
	}
}