using UnityEditor;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [System.Serializable]
    public class AttackInfo
    {
        public GameObject attackPrefab;
        public float attackProbability;
    }

    public AttackInfo[] attacks;

    public float attackCooldown = 2f;
    public float attackRange = 3f;
    private float nextAttackTime = 0f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;

        
        attacks = new AttackInfo[3];

        string pathToPrefab1 = "Assets/Prefabs/enemy/enemy attacks/BulletRain.prefab";
        GameObject bulletRainPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(pathToPrefab1);

        string pathToPrefab2 = "Assets/Prefabs/enemy/enemy attacks/RocketAttack.prefab";
        GameObject RocketAttackPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(pathToPrefab2);

        string pathToPrefab3 = "Assets/Prefabs/enemy/enemy attacks/SkyBombs.prefab";
        GameObject skyBombPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(pathToPrefab3);

        attacks[0] = new AttackInfo
        {
            attackPrefab = bulletRainPrefab,
            attackProbability = 2
        };

        attacks[1] = new AttackInfo
        {
            attackPrefab = RocketAttackPrefab,
            attackProbability = 2
        };

        attacks[2] = new AttackInfo
        {
            attackPrefab = skyBombPrefab,
            attackProbability = 2
        };
    }

    void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                ChooseAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    private void ChooseAttack()
    {
        if (attacks == null || attacks.Length == 0)
        {
            Debug.LogError("Attacks array is null or empty!");
            return;
        }

        float totalProbability = 0f;

        foreach (AttackInfo attack in attacks)
        {
            totalProbability += attack.attackProbability;
        }

        float randomValue = Random.value * totalProbability;

        foreach (AttackInfo attack in attacks)
        {
            if (randomValue < attack.attackProbability)
            {
                PerformAttack(attack.attackPrefab);
                return;
            }

            randomValue -= attack.attackProbability;
        }
    }

    private void PerformAttack(GameObject attackPrefab)
    {
        Instantiate(attackPrefab, transform.position, Quaternion.identity);
    }
}
