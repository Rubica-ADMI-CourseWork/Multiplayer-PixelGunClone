using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] Camera fpsCam;
    [SerializeField] float fireRate = 0.1f;
    float fireTimer = 0f;

    private void Update()
    {
        if(fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        if(Input.GetMouseButtonDown(0) && fireTimer > fireRate)
        {
            //reset the timer
            fireTimer = 0;

            //create ray
            Ray ray = fpsCam.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100f))
            {
                //have we hit the player
                if (hit.collider.gameObject.CompareTag("Player") &&
                    !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    //call RPC to deal damage
                    var photonview = hit.collider.gameObject.GetComponent<PhotonView>();
                    photonview.RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
                }
            }
        }
    }
}
