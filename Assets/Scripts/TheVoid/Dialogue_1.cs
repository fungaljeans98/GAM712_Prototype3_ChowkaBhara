using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;



public class Dialogue_1 : MonoBehaviour
{

    InputSystem_Actions inputSystem;

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public string sceneManager;


    public GameObject glowingOrbSpawner;
    public GlowingOrb_Spawner glowingOrb_Spawner;

    // Start is called before the first frame update


    private void Awake() {
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();
    }
    private void OnEnable() {
        inputSystem.Player.TouchTap.performed += OnTap;
    }
    private void OnDisable() {
        inputSystem.Player.TouchTap.performed -= OnTap;
    }
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void OnTap(InputAction.CallbackContext context) {
        if (textComponent.text == lines[6]) {
                    NextLine();
                    glowingOrb_Spawner.SpawnGlowingOrb();
            }
            
            else if (textComponent.text == lines[index])
            {
                NextLine();
                
            }
            else if (textComponent.text == lines[0]) {
                Debug.Log("Reached end");
                SceneManager.LoadScene(sceneManager);
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
                // Debug.Log("Reached end");
                // SceneManager.LoadScene(sceneManager);
            }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = null;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(sceneManager);
        }
    }
}
