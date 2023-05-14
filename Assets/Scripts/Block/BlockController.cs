using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private float blockMoveSpeed;
	[SerializeField] private float blockFallingSpeed;

	private BlockFactory factory;
	private Block currentBlock = null;

	public Block CurrnetBlock => currentBlock;

	private List<Block> blocks = new List<Block>();

	private void Start()
	{
		factory = new BlockFactory();
	}

	public void CreateRandomBlock(System.Action<Block> _action)
	{
		currentBlock = null;

		Vector3 topCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
		topCenter.y += 1f;
		topCenter.z = 0;

		BLOCK randomBlockType = (BLOCK)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BLOCK)).Length);

		factory.CreateBlock(randomBlockType, topCenter, Quaternion.identity, (block) =>
		{
			block.SetCollidingAction(() =>
			{
				currentBlock = null;
				_action?.Invoke(block);
				SoundManager.it.PlaySFX("BlockDrop");
			});
			currentBlock = block;
			blocks.Add(block);
		});
	}

	public void ResetBlocks()
	{
		for (int i = 0; i <  blocks.Count; i++) 
		{
			var block = blocks[i];
			Destroy(block.gameObject);
		}

		blocks.Clear();
	}

	public void SetBlockSpeed(float _speed)
	{
		blockFallingSpeed = _speed;
	}

	private void Update()
	{
		if (currentBlock == null)
		{
			return;
		}

		Vector3 speed = Vector3.zero;

		// 왼쪽 방향키 입력 감지
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			speed.x -= blockMoveSpeed * Time.deltaTime;
		}

		// 오른쪽 방향키 입력 감지
		if (Input.GetKey(KeyCode.RightArrow))
		{
			speed.x += blockMoveSpeed * Time.deltaTime;
		}

		// 스페이스바 입력 감지
		if (Input.GetKeyDown(KeyCode.S))
		{
			currentBlock.Rotate();
		}

		if (Input.GetKey(KeyCode.A))
		{
			speed.y -= blockFallingSpeed * 3 * Time.deltaTime;
		}
		else
		{
			speed.y -= blockFallingSpeed * Time.deltaTime;
		}

		currentBlock.Move(speed);
	}

	private void OnDestroy()
	{
		factory.UnloadBlocks();
	}
}
