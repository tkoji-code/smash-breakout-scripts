using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    const float FIELD_WIDTH = 4.8f;
    const float PaddleMin =1f;
    const float shrink =0f;
    const float speed =5f;
    const float CONST_TQ =1f;

    Animator animator;

    float vel_x;
    float x1;

    // Start is called before the first frame update
    void Start()
    {
        x1 = transform.position.x;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float x2 = transform.position.x;
        vel_x = (x2 - x1) / Time.deltaTime;
        x1 = x2;

        Vector3 pos = transform.position + new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, 0f);

        //pos limit
        float max_pos = FIELD_WIDTH / 2f - transform.localScale.x / 2f;
        if (pos.x <=-max_pos)
        {
            transform.position = new Vector3(-max_pos, transform.position.y, transform.position.z);
        }
        else if(pos.x >= max_pos)
        {
            transform.position = new Vector3(max_pos, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = pos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("SmashTrigger");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //transform paddle
        gameObject.transform.localScale -= Vector3.right*shrink;
        if (gameObject.transform.localScale.x < PaddleMin)
        {
            gameObject.transform.localScale = new Vector3(PaddleMin, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }

        //Add torque to collision
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddTorque(-CONST_TQ * vel_x);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Debri"))
        {
            Destroy(collision.gameObject);
        }
    }
}
