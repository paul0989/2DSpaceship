using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {

    private GameObject Object;
    private Queue objectContainer = new Queue();
    //先進先出的容器
    public EnemyPool(GameObject iPoolObject)
    {
        Object = iPoolObject;
        Debug.LogWarning("ObjectPool 初始化：" + iPoolObject.name);
    }
    public GameObject GetPoolObject()//從池子中取得物件
    {
        Debug.LogWarning("取得物件：" + Object.name);
        GameObject poolObj;

        if (objectContainer.Count > 0)//如果池子有物件
        {
            //從容器前端取出一個項目，同時將其移除
            poolObj = (GameObject)objectContainer.Dequeue();
        }
        else //如果池子沒有物件
        {
            //if(EnemyManager)
            //生一隻新的怪
            poolObj = GameObject.Instantiate(Object);
        }
        poolObj.name = Object.name + "(Active)";
        return poolObj;
    }

    public void ReturnPoolObject(GameObject iGameObject)
    {
        iGameObject.SetActive(false);
        iGameObject.name = Object.name + "(Sleep)";
        //把一個怪物放入容器尾端
        objectContainer.Enqueue(iGameObject);
    }


}
