using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private Camera camera;
	[SerializeField] private float lerpTime = 0.3f;

	private Vector3 velocity = Vector3.zero;
	private Vector3 blockPosition = new Vector3(0, -10, 0);

	public void SetBlockPosition(Vector3 _blockPosition)
	{
		blockPosition = _blockPosition;
	}

	private void LateUpdate()
	{
		Vector3 currentPosition = camera.transform.position;

		if (currentPosition.y - blockPosition.y < 2f)
		{
			Vector3 targetPosition = camera.transform.position;
			targetPosition.y = blockPosition.y + 2f;

			transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocity, lerpTime);
		}
	}

	public void ResetPosition()
	{
		camera.transform.position = new Vector3(0, 0, -10);
		blockPosition = new Vector3(0, -10, 0);
	}
}
