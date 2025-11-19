using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float TimeBetweenBullets = 1f;
    private float TimeSpan = 0f;
    [SerializeField] private GameObject bullet;
    private float timeInterval;
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private Animator animator;

    public Stack<GameObject> BulletStack = new Stack<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= TimeSpan)
        {
            if (BulletStack.Count == 0)
            {
                animator.Play("Shoot");
                GameObject bull = Instantiate(bullet, initialPosition, Quaternion.identity);
                bull.GetComponent<Bullet>()._spawner = this;
                animator.Play("Idle");
            }
            else
            {
                GameObject bull = BulletStack.Pop();
                Debug.Log("Reusing enemy from stack");
                //bull.SetActive(true);
                //bUll.transform.position = transform.position;
            }

            TimeSpan = Time.time + TimeBetweenBullets;
        }
    }
}
