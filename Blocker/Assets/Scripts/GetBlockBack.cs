using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBlockBack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D blockCollider)
    {
        if (blockCollider != null)
        {
            GameObject block = blockCollider.gameObject;
            if (block.transform.position.x < 0)
            {
                Vector2 newPosition = new Vector2(-11, 6);
                block.transform.position = newPosition;
            }
            else if (block.transform.position.x > 0)
            {
                Vector2 newPosition = new Vector2(11, -6);
                block.transform.position = newPosition;
            }
        }
    }
}
