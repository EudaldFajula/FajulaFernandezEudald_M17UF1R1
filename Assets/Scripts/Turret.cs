using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float timeBetweenBullets = 1f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private Animator animator;

    private float nextFireTime = 0f;
    public Stack<GameObject> BulletStack = new Stack<GameObject>();

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + timeBetweenBullets;
        }
    }

    void Fire()
    {
        animator.SetTrigger("Shoot"); //Activar animación de disparo

        GameObject bull;
        if (BulletStack.Count == 0)
        {
            bull = Instantiate(bullet, initialPosition, Quaternion.identity);
            bull.GetComponent<Bullet>()._spawner = this;
        }
        else
        {
            bull = BulletStack.Pop();
            Debug.Log("Reusing bullet from stack");
            // bull.SetActive(true);
            // bull.transform.position = initialPosition;
        }

        StartCoroutine(ResetToIdle());
    }

    IEnumerator ResetToIdle()
    {
        yield return new WaitForSeconds(nextFireTime); //Esperar la duración de la animación "Shoot"
        animator.Play("Idle");
    }
}
