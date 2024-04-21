using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float jumpSpeed = 10;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private GameObject endPannel;

    private bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!endPannel.activeSelf) {
            float x = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime * playerSpeed);

            if(x < 0)
            {
                sr.flipX = true;
                animator.SetBool("correndo", true);
            } 
            else if(x == 0)
            {
                animator.SetBool("correndo", false);
            }
            else
            {
                sr.flipX = false;
                animator.SetBool("correndo", true);
            }


            jump();
        }
    }

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            // canJump = false;
            rb.AddForce(
                new Vector2(0f, jumpSpeed),
                ForceMode2D.Impulse
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
            animator.SetBool("pulando", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
            animator.SetBool("pulando", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "fruta")
        {
            if(collision.gameObject.GetComponent<Animator>().GetBool("coletando") == true) {
                return;
            }

            collision.gameObject.GetComponent<Animator>().SetBool("coletando", true);
            Destroy(collision.gameObject, 1f);
            GameController.setPontos(1);
        }

        if (collision.gameObject.tag == "inimigo")
        {
            morre();
        }

        if(collision.gameObject.tag == "end") {
            endPannel.SetActive(true);
            animator.SetBool("correndo", false);
            animator.SetBool("pulando", false);
        }
    }

    private void morre()
    {
        GameController.setPerda();
        transform.position = spawn.transform.position;
    }
}
