using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class dialog : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogObject testDialogue;
    [SerializeField] private GameObject dialogueBox;

    public bool IsOpen {get; private set; }
    
    private typewriter typewritterEffect;

    public string nextSceneName;

    private void Start()
    {
       // textLabel.text = "Hello!\n This is my new line.";
      // GetComponent<TypewritterEffect>().Run("Hellajhbfiluafiyugflo!\n This is maefahifuhay new line.", textLabel);
        typewritterEffect =GetComponent<typewriter>();
        CloseDialogueBox();
        Debug.Log("Start");
       
        ShowDialogueChange(testDialogue);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)){
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void ShowDialogueChange(DialogObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialougeChange(dialogueObject));
    }

    private IEnumerator StepThroughDialougeChange(DialogObject dialogueObject)
    {
        //yield return new WaitForSeconds(2);

        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewritterEffect.Run(dialogue, textLabel);
            yield return new WaitForSeconds(1f);

        }
        CloseDialogueBox();
        SceneManager.LoadScene(nextSceneName);
    }

    public void ShowDialogue(DialogObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialougeChange(dialogueObject));
    }

    public IEnumerator StepThroughDialouge(DialogObject dialogueObject)
    {
        //yield return new WaitForSeconds(2);
        foreach (string dialogue in dialogueObject.Dialogue)
        {
            yield return typewritterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(()=> Input.GetKeyDown(KeyCode.Space));

        }
        CloseDialogueBox();
        //SceneManager.LoadScene("Scenes/TerrainMap");
    }

    private void CloseDialogueBox(){
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text=string.Empty;
    }
}