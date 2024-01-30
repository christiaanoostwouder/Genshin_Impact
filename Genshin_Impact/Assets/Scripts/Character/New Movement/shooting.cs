using UnityEngine;

public class shooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform bow;
    [SerializeField] private Transform cam;
    public AudioSource bowSource;
    public AudioClip chargestart, chargeloop, chargerelease;

    [Header("Damage")]
    [SerializeField] private float baseDmg;
    [SerializeField] private float maxDmgMult;
    private float dmgMult;

    [Header("Reload")]
    [SerializeField] private float coolDown;
    private bool canFire = true;
    private bool chargeStartPlayed = true;
    private bool chargeReleasePlayed = false;

    // Update is called once per frame
    void Update()
    {
        if (CameraSwitch.shootingMode == true)
        {
            if (chargeStartPlayed == true)
            {
                chargeStartPlayed = false;
                ChargeStart();
            }
            
            if (chargeStartPlayed == false)
            {
                if (bowSource.isPlaying == false) { ChargeLoop(); }
            }

            if (chargeReleasePlayed == true)
            {
                if (bowSource.isPlaying == false) { chargeStartPlayed = true; }
            }

            if (dmgMult <= maxDmgMult)
            {
                dmgMult += 1 * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0) && canFire)
            {
                Fire(baseDmg * dmgMult);
                ChargeRelease();
            }
        }
    }

    private void ChargeStart()
    {
        Debug.Log("gedaan");
        bowSource.PlayOneShot(chargestart);
    }

    private void ChargeLoop()
    {
        bowSource.clip = chargeloop;
        bowSource.Play();
    }

    private void ChargeRelease()
    {
        bowSource.Stop();
        bowSource.PlayOneShot(chargerelease);
        chargeReleasePlayed = true;
    }

    void Fire(float damage)
    {
        Debug.Log("je hebt geschoten");
        Vector3 shootDirection = cam.forward;
        GameObject ob = Instantiate(arrow, bow.position, Quaternion.LookRotation(shootDirection));

        Debug.Log(damage);
        ob.GetComponent<Arrow>().dmg = damage;
        
        dmgMult = 0;
        canFire = false;
        Invoke("ChangeFire", coolDown);
    }

    private bool ChangeFire()
    {
        return canFire = !canFire;
    }
}