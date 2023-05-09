using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public int maxHealth = 5;
    public int startingHealth = 3;
    public GameObject heartPrefab;
    public Vector2 heartOffset = new Vector2(40f, 0f);

    private int currentHealth;
    private List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(Loadhealth());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            RemoveHeart();
        }
    }

    public void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, transform);
        heart.transform.localPosition = new Vector2(hearts.Count * heartOffset.x, 0f);
        hearts.Add(heart);
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

    private void OnValidate()
    {
        // Ensure max health is always at least starting health
        if (maxHealth < startingHealth)
        {
            maxHealth = startingHealth;
        }
    }

    private void Reset()
    {
        // Set default values
        maxHealth = 5;
        startingHealth = 3;
        heartPrefab = Resources.Load<GameObject>("Heart");
        heartOffset = new Vector2(40f, 0f);
    }

    public IEnumerator Loadhealth()
    {
        yield return new WaitForSeconds(0.1f);
        startingHealth = GameObject.Find("Player").GetComponent<PlayerStats>().health;
        currentHealth = startingHealth;
        for (int i = 0; i < currentHealth; i++)
        {
            AddHeart();
        }
    }
}