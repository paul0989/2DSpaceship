using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Transform player;
    //太空船
    private Animator animator;
    public float speed;
    //隕石速度
    public delegate void ExplodingDelegate(int score);
    //隕石爆炸
    public static ExplodingDelegate ExplodinEvent;

    [SerializeField]
    private int score;

    public int rangeMin;
    public int rangeMax;
    //隕石產生的範圍

    private AudioSource audioSource;
    public AudioClip explosionAudio;
    //音效


    public int GetScore()
    {
        return score;
    }

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //找到player
        animator = GetComponent<Animator>();
        Vector3 target= player.position - transform.position;
        //太空船的位置(V1)-隕石目前位置(V2)
        target.Normalize();
        //只取方向不要長度
        GetComponent<Rigidbody2D>().AddForce(target*speed,ForceMode2D.Impulse);
        audioSource = GetComponent<AudioSource>();

    }

    private void PlayEffects()
    {
        animator.SetTrigger("exploding");
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
    public void ResetEnemy()
    //隕石爆炸後消失
    {
        Destroy(gameObject);
    }
}
