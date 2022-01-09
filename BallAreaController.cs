using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAreaController : MonoBehaviour
{
    [SerializeField]
    GameObject ball;

    BallController ballController;

    // Start is called before the first frame update
    void Start()
    {
        ballController = ball.GetComponent<BallController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Debri"))
        {
            Destroy(collision.gameObject);
            ballController.power++;
        }
    }
}
