using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAreaController : MonoBehaviour
{
    const float force = 500f;

    [SerializeField]
    GameObject ball;

    BallController ballController;

    public bool isSmash;

    // Start is called before the first frame update
    void Start()
    {
        isSmash = false;
        ballController = ball.GetComponent<BallController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                Rigidbody2D rb_col = collision.gameObject.GetComponent<Rigidbody2D>();

                if (rb_col.velocity.y > 0)
                {
                    rb_col.AddForce(Vector3.up * force);
                }
                else
                {
                    rb_col.AddForce(Vector3.down * force);
                }

                ballController.isSmash = true;
                collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red; //fix Animation
            }
        }       
    }

}
