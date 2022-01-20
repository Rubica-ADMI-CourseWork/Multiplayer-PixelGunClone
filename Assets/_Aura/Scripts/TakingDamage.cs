using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    float health;
    [SerializeField] float starthealth = 100f;
    [SerializeField] Image healthBar;

    private void Start()
    {
        health = starthealth;
        healthBar.fillAmount = health / starthealth;
    }
    float Health
    {
        get { return health; }
        set
        {
            health = value;
            healthBar.fillAmount = health / starthealth;

            if (health < 0)
            {
                //player dies.
                Die();
            }
        }
    }

    private void Die()
    {
        if (photonView.IsMine)
        {

            //alert game manager and re spawn
            PixelGunGameManager.Instance.PlayerDie();
        }
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
