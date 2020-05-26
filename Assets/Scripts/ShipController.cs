using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    private Animator anim;
    //射擊、太空船爆炸等anim用到
    public float speed = 1;
    //太空船移動速度

    private float timer = 0;
    private float shootingTimer = 0.05f;
    //0.05秒可以發射一次

    public delegate void GameOverDelegate();
    public static GameOverDelegate GameOverEvent;
    //太空船爆炸時GameOver的畫面

    private AudioSource audioSource;
    public AudioClip explosionAudio;
    //音效
    
    private bool isDead;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //fire  = this.gameObject.transform.GetChild(0).GetChild(0).gameObject; 
    }

    public delegate void ShootDelegate(Vector3 position);

    public static ShootDelegate ShootEvent;

    private void moving()
    //移動
    {
        float h = Input.GetAxisRaw("Horizontal");
        //水平移動速度方向
        float v = Input.GetAxisRaw("Vertical");
        //垂直移動速度方向
                
        Vector2 tmp = transform.position;
        //Edit>Project Settings>Input內可查到Name
        //取得太空船現在位置

        tmp.x += h * Time.deltaTime * speed;
        tmp.y += v * Time.deltaTime * speed;
        //計算把位置先算出來(還沒有移動)

        if (isDead == false && tmp.x >= -7.5 && tmp.x <= 7.5 && tmp.y >= -4 && tmp.y <= 4)
        //飛船移動不超過邊界
        {
            transform.position = tmp;
            //移動(將計算好的位置(tmp)放到transform裡面
        }

    }

    private void shooting()
    {
        if (isDead == false && Input.GetButton("Fire1")&& timer >= shootingTimer)
        {
            anim.SetTrigger("shooting");
            audioSource.Play();
            if (ShootEvent != null)
            {
                ShootEvent(transform.position);
                //現在的位置
            }
            timer = 0;
            //發射後計時歸0
        }
        timer += Time.deltaTime;
        //計算時間
    }

    public void hide()
    //飛船爆炸後完全消失
    {
        GetComponent<SpriteRenderer>().enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        moving();
        shooting();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            anim.SetTrigger("explosion");
            GetComponent<Collider2D>().enabled = false;
            //撞到隕石後把碰撞器關掉避免重複爆炸
            isDead = true;

            if (GameOverEvent != null)
            {
                GameOverEvent();
            }

            audioSource.PlayOneShot(explosionAudio);
        }

    }

}
