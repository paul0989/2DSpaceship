using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private bool _isActivity; //子彈是否活躍狀態
    private Rigidbody2D rig2d;
    public float AmmoDamage = 2;//子彈傷害
	// Use this for initialization
	void Start () {
        rig2d = GetComponent<Rigidbody2D>();
        rig2d.bodyType = RigidbodyType2D.Static;
        //子彈設定為靜態(放場景外)
    }
    public bool isActivity
    {
        get
        {
            return _isActivity;
        }
        set
        {
            //如果是活躍的就把Rigidbody改為動態，不是則靜態
            _isActivity = value;
            rig2d.bodyType =
                _isActivity ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
            //   isActivity  是不是活躍的,如果是則改為動態,不是則靜態
            if (!_isActivity)//現在不是活躍狀態
            {
                transform.position = transform.parent.position;
                //回歸到原來子彈池父類位置
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="recycling" || collision.tag == "enemy")
        //撞擊到recycling或enemy的tag
        {
            this.isActivity = false;
            //子彈變為不活耀(移動到場景外)
        }
    }

}
