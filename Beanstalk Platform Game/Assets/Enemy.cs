using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] float health;
=======
    [SerializeField] int health;


    public GameObject healthDisplay;
    private int currentHealth;
    private List<GameObject> hearts = new List<GameObject>();
    public GameObject heartPrefab;
    public Vector2 heartOffset = new Vector2(40f, 0f);


    public void Start()
    {
        if(healthDisplay != null)
            StartCoroutine(Loadhealth());
        healthDisplay.gameObject.SetActive(false);
    }

    private void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            GameObject heart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(heart);
        }
    }

    public void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, healthDisplay.transform);
        heart.transform.localPosition = new Vector2(hearts.Count * heartOffset.x, 0f);
        heart.transform.parent = healthDisplay.transform;
        hearts.Add(heart);
    }

    public IEnumerator Loadhealth()
    {
        yield return new WaitForSeconds(0.1f);;
        for (int i = 0; i < health; i++)
        {
            AddHeart();
        }
    }

>>>>>>> Stashed changes

    private void Death() {
        Destroy(this.gameObject);
    }

    public void Update() {
        if(health <= 0) {
            Death();
        }
    }
    public void damage(float damage) {
        health -= damage;
<<<<<<< Updated upstream
        if(health <= 0) {
=======
        RemoveHeart();
        
        if (health <= 0) {
>>>>>>> Stashed changes
            Death();
        }
    }
}
