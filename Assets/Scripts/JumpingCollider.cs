using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCollider : MonoBehaviour
{
    [SerializeField] GameObject player;

    private bool collding = false;

    private void Update()
    {
        GetComponent<BoxCollider2D>().transform.position = new Vector2(transform.position.x, player.transform.position.y - (player.transform.localScale.y));
        Debug.Log(collding);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.name.Equals("Player"))
            collding = true;
    }

    public bool isColliding() { return collding; }
}
