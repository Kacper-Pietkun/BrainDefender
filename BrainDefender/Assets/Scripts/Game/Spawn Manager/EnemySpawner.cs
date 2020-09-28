﻿using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public void SpawnEnemy(float movementSpeed, int healthPoints, int attackDamage, int weight, float minimalDistance)
    {
        GameObject enemy = Instantiate(EnemyPrefab, GetRandomPosition(), EnemyPrefab.transform.rotation);
        RotateEnemy(enemy);
        EnemyStatistics enemyStatistics = enemy.GetComponent<EnemyStatistics>();
        enemyStatistics.MovementSpeed = movementSpeed;
        enemyStatistics.HealtPoints = healthPoints;
        enemyStatistics.MaxHealtPoints = healthPoints;
        enemyStatistics.AttackDamage = attackDamage;
        enemyStatistics.Weight = weight;
        enemyStatistics.MinimalDistance = minimalDistance;
    }


    private GameObject brain;
    private float maxPosY = -1.25f;
    private float minPosY = -5.5f;
    private int minlengthFromBrainX = 15;
    private int maxlengthFromBrainX = 20;


    private void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        brain = GameObject.FindGameObjectWithTag(Tags.Brain);
    }

    // Position which is not in camera's sight
    private Vector3 GetRandomPosition()
    {
        bool isLeft = Random.Range(0, 2) == 0 ? true : false;
        float posY = Random.Range(minPosY, maxPosY);
        float posX = Random.Range(minlengthFromBrainX, maxlengthFromBrainX);
        if (isLeft)
            posX = brain.transform.position.x - posX;
        else
            posX = brain.transform.position.x + posX;

        return new Vector3(posX, posY, 0);
    }

    // We have to check if an enemy is going to the brain from the left or the right side and set its rotation properly
    // Default rotation fits the situation when the enemy comes from the left side
    private void RotateEnemy(GameObject enemy)
    {
        Vector3 result = enemy.transform.position - brain.transform.position;
        if (result.x > 0)
            enemy.transform.rotation = new Quaternion(transform.rotation.x, 180, transform.rotation.y, 0);
    }
}