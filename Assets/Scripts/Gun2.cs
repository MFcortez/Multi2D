using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun2 : MonoBehaviour
{
    public GameObject bullet;
    public float force;

    PhotonView view;
    SpriteRenderer render;

    private void Start()
    {
        view = GetComponentInParent<PhotonView>();
        render = GetComponentInParent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!view.IsMine)
        {
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            int dir = (render.flipX) ? -1 : 1;
            Vector2 pos = (render.flipX) ? transform.position - new Vector3(1,0,0) : transform.position;

            GameObject tmpBullet = PhotonNetwork.Instantiate(bullet.name, pos, transform.rotation);
            tmpBullet.GetComponent<Rigidbody2D>().AddForce(dir * transform.right * force);
        }
    }
}
