using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {
    public GameObject bullet;
    public int buffetsize =100;
    //陣列大小
    private float bulletPositionOffset = 0.5f;
    //讓子彈分散開

    private Bullet[] bullets;
    
    private void OnEnable()
    {
        ShipController.ShootEvent += genBullets;
    }
    private void OnDisable()
    {
        ShipController.ShootEvent -= genBullets;
    }

    private void initBulletPool()
    //初始化子彈池
    {
        bullets = new Bullet[buffetsize];
        for(int i=0; i<buffetsize ;i++)
        {
            bullets[i] = Instantiate(bullet,transform.position,Quaternion.identity).GetComponent<Bullet>();
            //陣列[i]=創造放入(子彈.子彈池位置,旋轉角度)
            //Instantiate取出來的是GameObject所以要加後面的.GetComponent
            bullets[i].transform.SetParent(gameObject.transform);
            //把子彈放群組下做管理
        }
    }
    public void genBullets(Vector3 position)
        //讓子彈池裡的子彈進畫面(太空船現在的位置)
    {
        int count = 0;
        //0顆子彈
        foreach (Bullet bullet in bullets)
            //把子彈池裡的子彈取出
        {
            if (bullet.isActivity == false)
                //目前在子彈池裡裡的子彈
            {
                
                bullet.transform.position = position;
                //子彈設定成太空船的位置
                position.x += bulletPositionOffset;
                //讓子彈位移不重疊
                bullet.isActivity = true;
                //bullet已經在畫面上
                count++;
            }
            if (count == 3)
                //一次產生三顆子彈
            {
                break;
            }
        }
    }

	// Use this for initialization
	void Start () {
        initBulletPool();

    }
	
}
