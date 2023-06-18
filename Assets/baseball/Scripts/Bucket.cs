using System;
using UnityEngine;

using Random = UnityEngine.Random;

public class Bucket : MonoBehaviour
{
    private AudioSource Source { get; set; }
    public static Action OnCollisionEnter { get; set; }
    private static Bucket Instance
    {
        get => FindObjectOfType<Bucket>();
    }

    private void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

    public static void UpdatePositiion()
    {
        var x = Random.Range(-1.58f, 1.58f);
        var y = Random.Range(0, 3.26f);

        Instance.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollisionEnter?.Invoke();
        if(SettingsManager.SoundsEnabled)
        {
            Source.Play();
        }
    }
}
