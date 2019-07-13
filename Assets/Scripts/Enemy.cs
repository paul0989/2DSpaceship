using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Transform player;
    //太空船
    private Animator anim;
    public float speed;
    //隕石速度
    public delegate void ExplodingDelegate(int score);
    //隕石爆炸
    public static ExplodingDelegate ExplodinEvent;

    [SerializeField]
    private int score;
    //分數

    public int rangeMin;
    public int rangeMax;
    //隕石產生的範圍

    private AudioSource audioSource;
    public AudioClip explosionAudio;
    //音效

    
    private bool isSinking = false;
    private float sinkingDoneTime = 0f;//手動計算的死亡時間
    private EnemySpwan enemyManager;//物件所屬的管理器
    //物件池

    public int GetScore()
    {
        return score;
    }

	// Use this for initialization
	void Start () {
        /*player = GameObject.FindGameObjectWithTag("Player").transform;
        //找到player
        anim = GetComponent<Animator>();
        Vector3 target= player.position - transform.position;
        //太空船的位置(V1)-隕石目前位置(V2)
        target.Normalize();
        //只取方向不要長度
        GetComponent<Rigidbody2D>().AddForce(target*speed,ForceMode2D.Impulse);
        audioSource = GetComponent<AudioSource>();*/
        FindPlayer();
    }

    private void PlayEffects()
    //隕石爆炸特效
    {
        anim.SetTrigger("exploding");
        audioSource.Play();
    }

    public Vector3 SpwanPosition()
        //隕石隨機產生位置
    {
        float xRange = Random.Range(rangeMin, rangeMax + 1);
        //隨機水平(X)位置
        Vector3 spwanPosition = new Vector3(xRange,7);
        return spwanPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    //隕石被打到會爆炸
    {
        if (collision.tag == "recycling")
        //如果隕石碰到邊界
        {
            ResetEnemy();
        }
        if (collision.tag=="bullet")
        {
            PlayEffects();
            GetComponent<Collider2D>().enabled=false;
            //避免同時多發子彈打到隕石造成分數不對
            if (ExplodinEvent != null)
            {
                ExplodinEvent(GetScore());

            }
            
        }
    }
    //讓死掉的enemy復活
    public void Alive(EnemySpwan iEnemyManager)
    {
        enemyManager = iEnemyManager;

        //重置Enemy狀態
        //isDead = false;
        //isSinking = false;
        GetComponent<Collider2D>().enabled = true;
        //初始化Enemy生怪位置/角度
        transform.position = SpwanPosition();
        transform.rotation = Quaternion.identity;

        FindPlayer();
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //找到player
        anim = GetComponent<Animator>();
        Vector3 target = player.position - transform.position;
        //太空船的位置(V1)-隕石目前位置(V2)
        target.Normalize();
        //只取方向不要長度
        Debug.LogWarning("valo：" + GetComponent<Rigidbody2D>().velocity);
        GetComponent<Rigidbody2D>().AddForce(target * speed, ForceMode2D.Impulse);
        Debug.LogWarning("valo after：" + GetComponent<Rigidbody2D>().velocity);

        audioSource = GetComponent<AudioSource>();

    }

    public void ResetEnemy()
    //隕石爆炸後消失
    {
        //Destroy(gameObject);
        isSinking = true;
        //告知管理器這隻enemy回池條件達成
        enemyManager.HandleEnemyDeath(this.gameObject);

        //sinkingDoneTime = Time.time + 2f;

    }
    private bool Query_IsSinkingDone()
    {
        return Time.time >= sinkingDoneTime;
        //當前時間>=怪物死亡時間+兩秒
    }

}
