using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    public float speed;         // speed of projectile
    public float lifetime;      // lifetime of projectile
    
    // Start is called before the first frame update
    void Start()
    {
        if (lifetime <=0)
        {
            lifetime = 2.0f; // anytime a decimal is used, 'f' must be used to convert to 'float'
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifetime);           // destroying 'projectile' at its lifetime (2 sec by default)
    }

}
