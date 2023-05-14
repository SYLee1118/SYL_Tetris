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

	public int Score { get; private set; }

	private void Start()
	{
		factory = new BlockFactory();
	}

	public void CreateRandomBlock()
	{
		currentBlock = null;

		Vector3 topCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
		topCenter.y += 1f;
		topCenter.z = 0;

		BLOCK randomBlockType = (BLOCK)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BLOCK)).Length);

		factory.CreateBlock(randomBlockType, topCenter, Quaternion.identity, (block) =>
		{
			block.SetCollidingAction(CreateRandomBlock);
			currentBlock = block;
		});
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
