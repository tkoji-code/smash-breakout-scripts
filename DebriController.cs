using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebriController : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float iniForce = 20f;
    float FIELD_WIDTH = 4.8f; //FIX

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        float ang = -Random.Range(45, 135) * Mathf.Deg2Rad;
        rigid2D.AddForce(new Vector3(Mathf.Cos(ang), Mathf.Sin(ang), 0) * iniForce);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) > FIELD_WIDTH/2f)
        {
            rigid2D.velocity = new Vector3(-rigid2D.velocity.x, rigid2D.velocity.y, 0);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
