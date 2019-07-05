using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour {
    public float speed = 0.5f;
    //移動速度
    public float rangeMax;
    public float rangeMin;
    //當兩張圖在捲動時，碰到下方min點的圖移動到上方max點

    public Transform[] bgImages;

    // Update is called once per frame
    void Update () {
        foreach (Transform tf in bgImages)
        //把bgImage裡兩張圖第一張取出放到tf變數裡面，執行完下方{}再回來
        //取出bgImage裡兩張圖第二張放到tf變數裡面，再執行下方{}
        {
            Vector2 tmp = tf.position;
            //把現在的位置取出放到tmp裡
            tmp.y -= speed * Time.deltaTime;
            tf.position = tmp;

            if (tmp.y <= rangeMin)
            //當bg的移動到底部的點時
            {
                tmp.y = rangeMax;
                tf.position = tmp;
                //把bg移動到頂部的點
            }
        }
	}
}
