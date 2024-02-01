using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public GameObject enemy;
    public List<ItemInstance> dropTable = new();
    public float chanceToDrop;

    public static LayerMask PlayerLayer { get; private set; }

    public void damage(int dmg)
    {
        enemyHealth -= dmg;
    }

    public void Update()
    {
        if(enemyHealth <= 0)
        {
            dropItem();
            Destroy(enemy);
        }
    }

    public void dropItem()
    {
        float randomNumber = Random.Range(0, 10);
        if(randomNumber <= chanceToDrop)
        {
            int itemPlace = Random.Range(0, dropTable.Count);
            GameObject itemDropped = new GameObject();
            itemDropped.AddComponent<Rigidbody2D>();
            itemDropped.AddComponent<BoxCollider2D>().isTrigger = true;
            itemDropped.AddComponent<BoxCollider2D>().excludeLayers = PlayerLayer;
            itemDropped.AddComponent<ItemInstanceHolder>().item = dropTable[itemPlace];
            itemDropped.tag = "Item";
            itemDropped.AddComponent<SpriteRenderer>().sprite = dropTable[itemPlace].itemType.icon;
            itemDropped.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
            itemDropped.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);
            itemDropped.transform.position = this.transform.position;
            itemDropped.transform.position = this.transform.position;
            itemDropped.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }
}
