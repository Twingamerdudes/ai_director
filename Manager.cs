using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using OpenAI;
using TMPro;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;

    public Location[] locations;

    public string[] characters;

    public string topic;
    [Serializable] 
    public struct Location
    {
        public string name;
        public Material skybox;
    }
    
    public StageDirection[] stageDirections;
    void Start()
    {
        var names = new List<string>();
        foreach (var t in stageDirections)
        {
            names.Add(t.name);
        }
        SendRequest(names.ToArray());
    }

    private async void SendRequest(string[] stageDirectionNames)
    {
        var openai = new OpenAIApi();
        //Make a character string, ex. Rick, Morty
        string characterString = "";
        foreach(string character in characters)
        {
            characterString += character + ",";
        }
        characterString = characterString.Substring(0, characterString.Length - 1);
        string stageDirectionString = "";
        foreach(string stageDirection in stageDirectionNames)
        {
            stageDirectionString += stageDirection + "\n";
        }
        var response = await openai.CreateCompletion(new CreateCompletionRequest{
            Model="text-davinci-003",
            Prompt=$"Generate a dialogue between the characters {characterString}. It should be formatted like this:\n" +
                "(Character):(thing);(stage direction, look later for more info about it)\n" +
            "(Another Character):(thing);(stage direction)\n" +
            "Stage Directions (format exactly as shown):\n" +
            stageDirectionString +
                "Possible stages:\n" +
            "House\n" +
            "Space\n" +
        $"Topic: {topic}\n. The dialogue should be at least 5 lines long. Make sure characters don't end up at the random position. The max distance a character can move is 5 on both axis's. Don't surround stage direction args with () or []. Keep stage instructions on the same line as dialogue.\nDialogue:",
            MaxTokens = 1000
        });
        StartCoroutine(StepThroughGeneration(response.Choices[0].Text));
    }

    private IEnumerator StepThroughGeneration(string dialogue)
    {
        string[] lines = dialogue.Split("\n");
        foreach(string line in lines)
        {
            if (line == "")
            {
                continue;
            }
            string characterName = line.Split(":")[0];
            string characterDialogue = line.Split(";")[0];
            text.text = characterDialogue;
            string stageDirection = line.Split(";")[1];
            foreach (StageDirection direction in stageDirections)
            {
                string stageDirName = stageDirection.Split(" ")[0];
                if (!direction.name.ToLower().Contains(stageDirName.ToLower())) continue;
                var args = stageDirection.Split(" ").ToList();
                args.RemoveAt(0);
                direction.Execute(args.ToArray(), new object[] { characterName, locations });
            }

            yield return new WaitForSeconds(characterDialogue.Length / 10f);
        }
    }
}
