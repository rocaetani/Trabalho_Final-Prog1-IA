using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform Followed;
    public float Speed;
    public int squareDistance;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Speed * Time.deltaTime;
        if (Followed.position.x - transform.position.x > squareDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position+ Vector3.right, distance);
        }
        if (Followed.position.x - transform.position.x < -1 * squareDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position+ Vector3.left, distance);
        }
        if (Followed.position.y - transform.position.y > squareDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position+ Vector3.up, distance);
        }
        if (Followed.position.y - transform.position.y < -1 * squareDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position+ Vector3.down, distance);
        }
    }
}
