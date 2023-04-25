using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private float health;
    public GameObject breakPrefab;
    public List<GameObject> drops;
    public int level;

    private GameObject breakSound;


    private void breakObject() {
        Instantiate(breakPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        GameObject.Find("CM vcam1").GetComponent<screenShake>().shake(0.125f, .5f, .5f);

        //Amount of drops
        for (int i = 0; i < Random.Range(2 ,3 * level); i++) {
            //Drop item based on level
            Vector3 offset = new Vector3(Random.Range(-0.0f, 0.50f), 0, 0);
            for(int u = 0; u < level; u++) {
                Instantiate(drops[Random.Range(0, level - 1)], transform.position + offset, Quaternion.identity);    
            }
            offset = new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        }
            
    }

    private void Update() {
        if(health <= 0) {
            breakObject();
        }
    }

    public void damage(float amount) {
        health -= amount;
        if(health <= 0) {
            breakObject();
        }
    }
}
