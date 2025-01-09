using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerLife : MonoBehaviour
{
    bool dead = false;
    private void Update()
    {
        if (transform.position.y < -10f && !dead)
        {
            Die();
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), 3.5f);
        dead = true;

    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}