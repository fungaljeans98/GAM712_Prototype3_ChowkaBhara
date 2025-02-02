using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{

    InputSystem_Actions inputSystem;
    InputAction tapScreen;


    [Header("INK JSON file")]
    [SerializeField] private TextAsset inkJson;

    [Header("UI Stuff")]
    [SerializeField] private Transform canvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private float buttonSpacing;

    [Header("Next Scene")]
    [SerializeField] private string nextScene;

    [Header("Fade In (Link to exposure global volume)")]
    public GameObject globalVolume;
    public ExposureFadeIn globalVolumeScript;

    private Story story;
    private bool hasReachedEnd;


    private void Awake() {
        //Setting Defaults
        dialogueText.text = "";
        dialogueBox.SetActive(false);
        hasReachedEnd = false;

        //Initialising input system
        inputSystem = new InputSystem_Actions();
    }

    private void OnEnable() {
        tapScreen = inputSystem.Player.TouchTap;
        tapScreen.Enable();
        tapScreen.performed += OnTap;
    }

    private void OnDisable() {
        tapScreen.performed -= OnTap;
        tapScreen.Disable();
    }



    private void Start() {
        //Load the ink story
        story = new Story(inkJson.text);
    }

    void OnTap(InputAction.CallbackContext context) {
        //enabling dialogue box
        dialogueBox.SetActive(true);

        //Display the first line of the dialoge
        DisplayNextLine();

        //SceneManager
        if (hasReachedEnd) {
            Debug.Log("loading Next Scene");
            SceneManager.LoadScene(nextScene);
        }
    }


    public void DisplayNextLine() {
        Debug.Log("Display next line was called");
        //Display the next portion of the story
        if (story.canContinue) {
            dialogueText.text = story.Continue();
            DisplayChoices();
        }

        DestroyAllButtons();
        DisplayChoices();        
    }

    void DisplayChoices() {
        //Display any choices (If Any)
        Debug.Log("Display Choices was called");
        if (story.currentChoices.Count > 0) {
            Debug.Log("displaying choices");
            foreach (var choice in story.currentChoices) {
                //Instantiate the button
                UnityEngine.UI.Button button = Instantiate (buttonPrefab,canvas.transform);

                //Calculating button position
                Vector3 buttonPosition = new Vector3(choice.index * buttonSpacing, -250f, 0);

                //Setting new button position
                button.GetComponent<RectTransform>().localPosition = buttonPosition;

                //Set the button text to choice text
                button.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                button.onClick.AddListener(() => OnChoiceSelected(choice.index));
            }

            //Check for tags
            List<string> tags = story.currentTags;
            foreach (string tag in tags) {
                if (tag.StartsWith("Exposure")) {
                    globalVolumeScript.FadeInFunction();
                }
                if (tag.StartsWith("End")) {
                    globalVolumeScript.FadeOutFunction();
                    hasReachedEnd = true;
                }
            }
        }
         else {
            DestroyAllButtons();
        }
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

    void OnChoiceSelected(int choiceIndex) {
        //Make the selected choice in the story
        story.ChooseChoiceIndex(choiceIndex);

        //Continue the story
        DisplayNextLine();
    }

}
