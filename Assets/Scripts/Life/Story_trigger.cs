using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;

public class Story_trigger : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    [Header("INK JSON file")]
    [SerializeField] private TextAsset inkJSON;    // Ink JSON file output (you'll need to compile Ink to JSON)

    [Header("UI Stuff")]
    [SerializeField] private Transform UiCanvas;  // Parent to place buttons in UI
    [SerializeField] private UnityEngine.UI.Button buttonPrefab;  // Button prefab for choices
    [SerializeField] private float buttonSpacing;
    [SerializeField] private GameObject panelPrefab;        //UI prefab to spawn
    [SerializeField] private TextMeshProUGUI storyText;       // UI Text to display the story
    
    [Header("Link to Player GameObject")]
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterController playerScript;
    [SerializeField] private bool playerMovementAcess;

    private Story currentStory;
    private bool playerIsNear;


    private void Awake() {
        //Set Defaults
        storyText.text = " ";
        panelPrefab.SetActive(false);
        playerIsNear = false;
    }

    void Start()
    {
        //Initialising box collider
        boxCollider = GetComponent<BoxCollider2D>();

        // Load the story from the JSON file
        currentStory = new Story(inkJSON.text);
    }

    private void FixedUpdate() {
        if (playerIsNear){
            StartCoroutine(StartStory());
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Invoke("DisableCollider", 1);
        }

    }
    
    bool isRunning = false;
    private IEnumerator StartStory() {
        
        if (playerIsNear && !isRunning){
            DisplayNextStory();
            isRunning = true;
        }
        yield return new WaitForEndOfFrame();
    }

    void DisplayNextStory() {
        //Display the next portion of the story
        if (currentStory.canContinue) {
            DisableMovement();
            storyText.text = currentStory.Continue();
        }
        else if (currentStory.currentChoices.Count < 1) {
            Debug.Log("story is over");
            EnableMovement();
        }

        //Display any choices (If Any)
        DestroyAllButtons();
        DisplayChoices();
    }

    void DisplayChoices() {
        //Display any choices (If Any)
        if (currentStory.currentChoices.Count > 0) {
            Debug.Log("displaying choices");
            foreach (var choice in currentStory.currentChoices) {
                //Instantiate the button
                UnityEngine.UI.Button button = Instantiate (buttonPrefab, UiCanvas);

                //Calculating button position
                Vector3 buttonPosition = new Vector3(choice.index * buttonSpacing, 0, 0);

                //Setting new button position
                button.GetComponent<RectTransform>().localPosition = buttonPosition;

                //Set the button text to choice text
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                button.onClick.AddListener(() => OnChoiceSelected(choice));
            }

            //CHECK FOR TAGS
            List<string> tags = currentStory.currentTags;
            foreach (string tag in tags) {
                if (tag == "END") {
                    Debug.Log("Reached End");
                    EnableMovement();
                }
            }
        }
        else {
            DestroyAllButtons();
        }
    }

    void OnChoiceSelected(Choice choice) {
        currentStory.ChooseChoiceIndex(choice.index);
        DisplayNextStory();
    }

    void DestroyAllButtons() {
        string tagToDestroy = "ChoicesButtons";

        // Find all GameObjects with the specified tag
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(tagToDestroy);

        // Loop through and destroy each GameObject
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }

        Debug.Log($"Destroyed {objectsToDestroy.Length} objects with tag '{tagToDestroy}'.");
    }

    void DisableMovement() {
        if (playerMovementAcess) {
            playerScript.enableLeft = false;
            playerScript.enableRight = false;
            playerScript.enableJump = false;
        }
    }

    void EnableMovement() {
        if (playerMovementAcess) {
            playerScript.enableLeft = true;
            playerScript.enableRight = true;
            playerScript.enableJump = true;
        }
    }


    void DisableCollider() {
        //Disabling player colliders
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        playerCollider.enabled = false;
    }

    void DisableDialogueBox() {
        panelPrefab.SetActive(false);
        storyText.text = "";
    }

    void EnableDialogueBox() {
        panelPrefab.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerIsNear = true;
            EnableDialogueBox();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            playerIsNear = false;
            DisableDialogueBox();
        }
    }

}
