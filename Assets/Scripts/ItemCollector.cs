using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _platanos = 0;

    [SerializeField] private TextMeshProUGUI _textMesh;

    [SerializeField] private AudioSource _collectSound;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Platano")) return;
        Destroy(other.gameObject);
        _platanos++;
        _textMesh.text = $"Platanos: {_platanos}";
        _collectSound.Play();
    }
}
