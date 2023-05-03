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
			AudioManager.Instance.PlayBGM(handle.Result);
		}
		else
		{
			Addressables.LoadAssetAsync<AudioClip>(address).Completed += handle =>
			{
				loadedClips[address] = handle;
				AudioManager.Instance.PlayBGM(handle.Result);
			};
		}
	}

	public void PlaySFX(string address)
	{
		if (loadedClips.TryGetValue(address, out AsyncOperationHandle<AudioClip> handle))
		{
			AudioManager.Instance.PlaySFX(handle.Result);
		}
		else
		{
			Addressables.LoadAssetAsync<AudioClip>(address).Completed += handle =>
			{
				loadedClips[address] = handle;
				AudioManager.Instance.PlaySFX(handle.Result);
			};
		}
	}

	public override void Dispose()
	{
		AudioManager.Instance.StopAllSounds();
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