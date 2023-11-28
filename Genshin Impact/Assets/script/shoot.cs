using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public float damage;
    public GameObject prefab;
    public Camera myCamera;

    float x = Screen.width / 2;
    float y = Screen.height / 2;


    // Start is called before the first frame update
    void Start()
    {
        //var ray = myCamera.ScreenPointToRay(new Vector3(x, y, 0));
        //clone.velocity = ray.direction * 80;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            damage += 1 * Time.deltaTime;
            
            Debug.Log(damage.ToString());

            /*if(Input.GetMouseButtonUp(0))
            {
                fire();
                Debug.Log("release");
                return;
            }*/
        }
        if (Input.GetMouseButtonUp(0))
        {
            fire();
            Debug.Log("release");
            return;
        }
    }

    void fire()
    {
        Debug.Log("je hebt geschoten");
        GameObject ob = Instantiate(prefab);
        ob.transform.position = transform.position;
        ob.transform.rotation = transform.rotation;
        Destroy(ob, 3f);
    }
    
}