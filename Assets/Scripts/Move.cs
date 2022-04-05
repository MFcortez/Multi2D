using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Move : MonoBehaviourPun, IPunObservable
{
    public float speed;
    
    Rigidbody2D rb;
    Animator animator;
    PhotonView view;
    SpriteRenderer render;

    float hor, ver;
    Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
        render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!view.IsMine)
        {
            return;
        }

        hor = Input.GetAxis("Horizontal");
        direction = new Vector2(hor * speed, rb.velocity.y);

        if (hor > 0)
        {
            render.flipX = false;
        } else if (hor < 0)
        {
            render.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction;
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(render.flipX);
        } else
        {
            render.flipX = (bool)stream.ReceiveNext();
        }
   }
}
