using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject go = GameObject.Find("Singleton");
				if (go == null)
				{
					go = new GameObject();
					GameObject.DontDestroyOnLoad(go);
					go.name = "Singleton";
				}
				GameObject child = new GameObject();
				child.name = typeof(T).Name;
				child.transform.SetParent(go.transform);
				instance = child.AddComponent<T>();
			}
			return instance;
		}
	}
	public static T it => instance;

	public abstract void Dispose();
}
