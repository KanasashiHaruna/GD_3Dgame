using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("’e‚ðŒ‚‚Â")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] Rigidbody rb;


    private Vector3 direction;
    public PlayerManager player;
    // Start is called before the first frame update

    
    void Start()
    {
        
    }

    public void Initialize(PlayerManager pl)
    {
        player = pl;
    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy=collision.GetComponent<EnemyScript>();
            if (enemy != null && enemy.isHit==false)
            {
                //Debug.Log("’e‚Æ“G‚ª“–‚½‚Á‚½‚æ");
                Vector3 enemyPosition = collision.transform.position;
                player.DrawLine(enemyPosition,collision.transform);
                enemy.isHit = true;
            }
            Destroy(gameObject);
        }
    }
}
