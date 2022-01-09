using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float iniForce =200.0f;
    public bool isSmash = false;
    public int power = 0;

    const float curvRatio = 0.003f;

    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();

        float launchAng = (Random.Range(0, 1) * 2 - 1) * 30;
        rigid2D.AddForce(new Vector3(Mathf.Sin(Mathf.Deg2Rad * launchAng), -Mathf.Cos(Mathf.Deg2Rad * launchAng), 0f) * iniForce);
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.AddForce(Vector3.Cross(rigid2D.velocity.normalized, rigid2D.angularVelocity * Vector3.forward) * curvRatio);

        if (transform.position.y < -5f) Destroy(gameObject);

        //動かなくなった時対策
        if (rigid2D.velocity.y == 0f && rigid2D.angularVelocity == 0f)
        {
            rigid2D.AddForce(new Vector2(0f, iniForce * 0.01f));
        }
    }

}
