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

    [Header("弾を撃つ関連")]
    [SerializeField] BulletScript bullet;
    [SerializeField] private float shotInterval = 1.0f;
    [SerializeField] private float lastShotTime = 0.0f;

    [Header("線を繋げる関連")]
    [SerializeField] LineRenderer lineRenderer;
    private List<LineRenderer> lineRenderers=new List<LineRenderer>();
    private List<Transform> enemyTransforms = new List<Transform>();
    private Vector3 playerPosition;
    private Vector3 enemyposition;
    //[SerializeField] private Transform shotPosition;


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        #region 方向
        Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, 100.0f))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Vector3 direction=(targetPosition - transform.position).normalized;
            Quaternion lookRotation=Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angleSpeed);
        }
        #endregion

        //移動------------------------
        Move();

        //弾撃つ----------------------
        if (Input.GetMouseButton(0))
        {
            Shot();
        }

        playerPosition = transform.position;
        UpdateLineRenderers();
    }


    #region 移動
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
    #endregion

    #region 弾を撃つ
    private void Shot()
    {
        if (Time.time >= lastShotTime + shotInterval)
        {
            BulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);
            obj.SetDirection(transform.forward);
            obj.Initialize(this);
            lastShotTime = Time.time;
        }
    }
    #endregion

    #region lineRendererのstartとendの位置を入れる
    public void DrawLine(Vector3 enemypos, Transform enemyTransform)
    {
        LineRenderer newLineRenderer = Instantiate(lineRenderer, transform.position, Quaternion.identity);
        newLineRenderer.positionCount = 2;
        newLineRenderer.SetPosition(0, playerPosition); //プレイヤーの位置
        newLineRenderer.SetPosition(1, enemypos);       //敵の位置
        newLineRenderer.startWidth = 0.05f;
        newLineRenderer.endWidth = 0.05f;

        lineRenderers.Add(newLineRenderer);             //リストに追加
        enemyTransforms.Add(enemyTransform);
    }
    #endregion

    #region Rendererの位置を更新
    private void UpdateLineRenderers()
    {
        if (lineRenderers.Count == enemyTransforms.Count)
        {
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                lineRenderers[i].SetPosition(0, transform.position);
                lineRenderers[i].SetPosition(1, enemyTransforms[i].position);
            }
        }
        //else
        //{
        //    Debug.LogError("LineRenderersとEnemyTransformsのリストが同期していません！");
        //}
    }
    #endregion
}
