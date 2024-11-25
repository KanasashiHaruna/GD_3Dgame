using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("プレイヤーの移動関連")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Vector3 distance;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float angleSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //方向------------------------
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100.0f))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 direction=(targetPosition - transform.position).normalized;
            Quaternion lookRotation=Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angleSpeed);
        }
        //移動------------------------
        Move();
    }

    private void Move()
    {
        distance = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            distance = Vector3.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            distance = Vector3.back;
        }

        if (Input.GetKey(KeyCode.D))
        {
            distance = Vector3.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            distance = Vector3.left;
        }

        rb.velocity = distance * moveSpeed;
    }
}
