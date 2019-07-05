using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwan : MonoBehaviour {
    public Enemy enemy;
    public float speed;
	// Use this for initialization
	void Start () {
        InvokeRepeating("GenerateEnemy", 0.1f,speed);
        //首次延遲0.1，每隔speed執行一次
	}
    private void GenerateEnemy()
    {
        Instantiate(enemy.gameObject,enemy.SpwanPosition(),Quaternion.identity);
        
    }
}
