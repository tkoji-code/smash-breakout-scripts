using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    const int BlockDef = 100;
    
    [SerializeField] 
    GameObject DebriPrefab;
    [SerializeField] 
    GameObject BallPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            BallController ballController=collision.gameObject.GetComponent<BallController>();

            int atackPower = ballController.power;
            if (ballController.isSmash) atackPower *= 2;

            //Judge clear condition
            if (atackPower >= BlockDef)
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
            else
            {
                //Generate debris;
                int qty = 10;
                //if (ballController.isSmash)
                //{
                //    qty *= 2;
                //}
                GenerateDebris(qty);
            }

            //Initialize ball
            Rigidbody2D ballRigid2D = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 ballVec = Vector3.Normalize(ballRigid2D.velocity);

            ballRigid2D.velocity = new Vector3(0f, 0f, 0f);
            ballRigid2D.angularVelocity = 0;

            ballRigid2D.AddForce(ballVec * ballController.iniForce);

            ballController.isSmash = false;
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white; //fix Animation
        }
    }

    private void GenerateDebris(int qty)
    {
        for(int i = 1; i <= qty; i++)
        {
            GameObject debri = Instantiate(DebriPrefab) as GameObject;
            debri.transform.position = new Vector3(transform.position.x, transform.position.y-0.5f,0f);
        }
    }
    
}
