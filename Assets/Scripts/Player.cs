using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bulletPrefab;
    
    private const float maxLeft = -8.5f;
    private const float maxRight = 8.5f;

    private float speed = 3f;
    private bool isShooting = false;
    private float coolDown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var h = Input.GetAxis("Horizontal");

        var newPlayerPos = Vector2.right * Time.deltaTime * speed * h;

        if (transform.position.x + newPlayerPos.x >= maxLeft && transform.position.x + newPlayerPos.x <= maxRight)
            transform.Translate(newPlayerPos);

        if (Input.GetKeyDown(KeyCode.Space) && !isShooting)
            StartCoroutine(Shoot());

        Debug.Log(newPlayerPos.x);
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(coolDown);
        isShooting = false;
    }
}
