using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
	public BTNode root;
	public GameObject alvo;
	public GameObject projetil;

	public IEnumerator Begin()
	{
		while (true) {
			yield return StartCoroutine(root.Run(this));
		}
	}
}
