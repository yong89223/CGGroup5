using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int basePlayerDamage = 20;
    public int damage = 10;
    private int playerDamage;

    private void Start()
    {
        var equippedItem = DataManager.instance.nowPlayer.inventory.items.Find(item => item.id == DataManager.instance.nowPlayer.item);
        int itemBonusDamage = equippedItem != null ? equippedItem.damage : 0;
        playerDamage = basePlayerDamage + itemBonusDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Wall":
                break;

            case "Player":
                HandlePlayerHit(other);
                break;

            case "Enemy":
                HandleEnemyHit(other);
                break;
        }

        Destroy(gameObject); // 충돌 후 삭제
    }

    private void HandlePlayerHit(Collider playerCollider)
    {
        Health playerHealth = playerCollider.GetComponent<Health>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void HandleEnemyHit(Collider enemyCollider)
    {
        Health enemyHealth = enemyCollider.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(playerDamage);
        }
    }


}
