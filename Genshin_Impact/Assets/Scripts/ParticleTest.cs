using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    [SerializeField] private GameObject damageText;
    [SerializeField] private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            GameObject newtext = Instantiate(damageText, other.transform.position, Quaternion.identity);
            newtext.transform.position += new Vector3(Random.Range(1f, -1f), Random.Range(1f, -1f), Random.Range(1f, -1f));
            newtext.transform.LookAt(player);
            newtext.GetComponent<Rigidbody>().AddForce(Vector3.up * 10);
            newtext.GetComponent<TextMesh>().characterSize = Random.Range(0.1f, 0.2f);
            newtext.GetComponent<TextMesh>().fontSize = 100;
            newtext.GetComponent<TextMesh>().text = Mathf.Round(other.GetComponent<Arrow>().dmg).ToString();
            Destroy(other.gameObject);
            Destroy(newtext, 1f);
        }
    }
}
