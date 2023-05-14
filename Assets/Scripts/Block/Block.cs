using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Block : MonoBehaviour
{
	private Rigidbody2D rigidbody;
	private StateController owner;

	// ���� �浹�� RigidBody type�� Kinematic -> Dynamic���� ��ȯ�ϴ°� üũ�ϱ� ���� �뵵
	bool isCollided = false;

	private Action FirstCollidingAction;

	public void SetOwner(StateController _owner)
	{
		owner = _owner;
	}

	public void SetCollidingAction(Action _firstCollidingAction)
	{
		FirstCollidingAction = _firstCollidingAction;
	}

	public void Move(Vector3 _speed)
	{
		Vector3 position = transform.position;
		position += _speed;
		rigidbody.MovePosition(position);
	}

	public void Rotate()
	{
		rigidbody.MoveRotation(rigidbody.rotation - 90f);
	}

	private void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (isCollided == false)
		{
			rigidbody.bodyType = RigidbodyType2D.Dynamic;
			rigidbody.freezeRotation = false;
			isCollided = true;
			FirstCollidingAction?.Invoke();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "GameOver")
		{
			owner.ChangeState(GameState.GameOver);
		}
	}

	private void OnValidate()
	{
		// Rigidbody2D�� �߰��� ������ m_rigidbody �� ����
		rigidbody = GetComponent<Rigidbody2D>();
		rigidbody.isKinematic = true;
		rigidbody.freezeRotation = false;
	}
}