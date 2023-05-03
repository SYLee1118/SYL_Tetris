using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum BLOCK
{
	Block_L = 0,
	Block_L_R,
	Block_Box,
	Block_T,
	Block_Long,
	Block_Snake,
	Block_Snake_R
}

public class BlockFactory
{
	private Dictionary<BLOCK, AssetReference> blockPrefabs;

	public BlockFactory()
	{
		blockPrefabs = new Dictionary<BLOCK, AssetReference>();

		foreach (BLOCK blockType in System.Enum.GetValues(typeof(BLOCK)))
		{
			blockPrefabs.Add(blockType, new AssetReference(blockType.ToString()));
		}
		LoadBlocks();
	}

	public void LoadBlocks()
	{
		foreach (var blockPrefab in blockPrefabs.Values)
		{
			blockPrefab.LoadAssetAsync<GameObject>();
		}
	}

	public void UnloadBlocks()
	{
		foreach (var blockPrefab in blockPrefabs.Values)
		{
			blockPrefab.ReleaseAsset();
		}
	}

	public void CreateBlock(BLOCK blockType, Vector3 position, Quaternion rotation, System.Action<Block> onComplete)
	{
		// �ε�� ��� �������� �ν��Ͻ�ȭ�Ͽ� onComplete �ݹ� �Լ��� �����մϴ�.
		blockPrefabs[blockType].InstantiateAsync(position, rotation).Completed += (operation) =>
		{
			if (operation.Status == AsyncOperationStatus.Succeeded)
			{
				Block block = operation.Result.GetComponent<Block>();
				block.gameObject.SetActive(true);
				onComplete?.Invoke(block);
			}
		};
	}
}