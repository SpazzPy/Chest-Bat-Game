using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    private int chestCollected = 0;
    public TMP_Text ChestText;
    public AudioSource openChestSound;
    public TMP_Text winText;

    private int currentSceneIndx;

    private void Start() {
        ChestText.text = "Cofres: 0/5";
        currentSceneIndx = SceneManager.GetActiveScene().buildIndex;
        winText.text = "";

    }

    private void FixedUpdate() {

        if (chestCollected >= 5)
        {
            winText.text = "Ganaste !";
            StartCoroutine(changeScene());
        }
        
    }

    private IEnumerator changeScene()
    {
        yield return new WaitForSeconds(3f);
        if (currentSceneIndx == 1)
        {
            SceneManager.LoadScene(2);
        } else {
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chest"))
        {
            Animator chestAnimator = collision.gameObject.GetComponent<Animator>();
            chestAnimator.SetBool("chestopen", true);
            collision.isTrigger = false;
            collision.gameObject.GetComponent<Collider2D>().enabled = false;
            chestCollected++;
            ChestText.text = "Cofres: " + chestCollected + "/5";
            openChestSound.Play();
        }
    }
}
