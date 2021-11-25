using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public GameObject espadaPlayer;
    public GameObject escudoPlayer;

    public GameObject espadaChao;
    public GameObject escudoChao;
    public bool canAttack;

    private void Start()
    {
        canAttack = false;
        espadaPlayer.SetActive(false);
        escudoPlayer.SetActive(false);
        espadaChao.SetActive(true);
        escudoChao.SetActive(true);
    }

    public void CanAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.5f);
        canAttack = true;
        espadaPlayer.SetActive(true);
        escudoPlayer.SetActive(true);
        escudoChao.SetActive(false);
        espadaChao.SetActive(false);
    }
}
