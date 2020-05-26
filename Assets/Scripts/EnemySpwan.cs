using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwan : MonoBehaviour {
    public GameObject ienemy;
    public Enemy enemy;
    public float speed;
        
    private EnemyPool enemyPool;
    //物件池

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("GenerateEnemy", 0.1f, speed);
        //"字串方法名稱",首次延遲0.1，每隔speed執行一次        
        enemyPool = new EnemyPool(ienemy);
        //初始化物件池

    }

    public void HandleEnemyDeath(GameObject iGameObject)
    {        
        enemyPool.ReturnPoolObject(iGameObject);
        //把從enemyHeath得到的達成回池條件怪物回池
    }

    private void GenerateEnemy()
    {
        Instantiate(enemy.gameObject,enemy.SpwanPosition(),Quaternion.identity);
        //生怪改使用物件池
        /*GameObject enemyObj = enemyPool.GetPoolObject();

        //初始化Enemy參數
        enemyObj.GetComponent<Enemy>().Alive(this);

        enemyObj.gameObject.SetActive(true);*/
    }

}
