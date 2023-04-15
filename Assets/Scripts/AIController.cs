using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI_API;
using OpenAI_API.Chat;
using TMPro;
using UnityEngine.UI;
using System;
using OpenAI_API.Models;

public class AIController : MonoBehaviour
{
    public TMP_Text textField;
    public TMP_Text rightWrongField;
    public TMP_InputField inputField;
    public Button okButton;
    public Button A;
    public TMP_Text Atext;
    public Button B;
    public TMP_Text Btext;
    public Button C;
    public TMP_Text Ctext;
    public Button D;
    public TMP_Text Dtext;

    public String chatText;
    public String ansText;
    public String wrongAnsText;

    public String questionSubject = "Science";
    public String age = "15";
    public String difficulty;
    

    private List<ChatMessage> messages;

    private OpenAIAPI api;


    // Start is called before the first frame update
    void Start()
    {
        // API KEY GOES HERE
        api = new OpenAIAPI("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");
        messages = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "You are about to start generating educational questions for users to solve.")
        };

        StartQuestion();
        A.onClick.AddListener(() => StartQuestion());

    }

    private async void StartQuestion()
    {
        String initialQuestion = string.Format("Generate a different random {0} question that a {1} year old would know", questionSubject, age);

        // Generate a prompt to send
        ChatMessage promtMessage = new ChatMessage();
        promtMessage.Role = ChatMessageRole.User;
        promtMessage.Content = initialQuestion;
        messages.Add(promtMessage);
        Debug.Log(promtMessage.Content);

        // Send the entire chat to OpenAI to get the next message
        var questionResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        // Determine and display the question
        ChatMessage questionMessage = new ChatMessage();
        questionMessage.Role = questionResult.Choices[0].Message.Role;
        questionMessage.Content = questionResult.Choices[0].Message.Content;
        messages.Add(questionMessage);
        textField.text = questionMessage.Content;
        Debug.Log(questionMessage.Content);


        okButton.onClick.AddListener(() => RespondQuestion());

        

        /*
        // Get the response message
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = chatResult.Choices[0].Message.Role;
        responseMessage.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessage.Role, responseMessage.Content));
        String[] promptAndAnswers = responseMessage.Content.Split("|");
        String[] answers = promptAndAnswers[1].Split("\n");
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i] == "")
            {
                for (int j = i; j < answers.Length - 1; j++)
                {
                    answers[j] = answers[j + 1];
                }
            }
        }
        textField.text = promptAndAnswers[0];
        Atext.text = answers[0];
        
        
        
        
        /*Btext.text = answers[1];
        Ctext.text = answers[2];
        Dtext.text = answers[3];
        String answer = answers[4];
        char answerLetter = answer[0];
        Debug.Log(answerLetter);
        // Add the response to the list of messages
        messages.Add(responseMessage);

        // Wait for user input
        switch (answerLetter)
        {
            case 'A':
                A.onClick.AddListener(() => ButtonPressed(true));
                B.onClick.AddListener(() => ButtonPressed(false));
                C.onClick.AddListener(() => ButtonPressed(false));
                D.onClick.AddListener(() => ButtonPressed(false));
                break;
            case 'B':
                A.onClick.AddListener(() => ButtonPressed(false));
                B.onClick.AddListener(() => ButtonPressed(true));
                C.onClick.AddListener(() => ButtonPressed(false));
                D.onClick.AddListener(() => ButtonPressed(false));
                break;
            case 'C':
                A.onClick.AddListener(() => ButtonPressed(false));
                B.onClick.AddListener(() => ButtonPressed(false));
                C.onClick.AddListener(() => ButtonPressed(true));
                D.onClick.AddListener(() => ButtonPressed(false));
                break;
            case 'D':
                A.onClick.AddListener(() => ButtonPressed(false));
                B.onClick.AddListener(() => ButtonPressed(false));
                C.onClick.AddListener(() => ButtonPressed(false));
                D.onClick.AddListener(() => ButtonPressed(true));
                break;
            default:
                print("Question Malfunction... Loading new question");
                break;
        }*/
    }

    private async void RespondQuestion()
    {
        ChatMessage responseMessage = new ChatMessage();
        responseMessage.Role = ChatMessageRole.User;
        String response = string.Format("Is {0} the correct answer to the above question? Reply with just Yes or No", inputField.text);
        responseMessage.Content = response;
        messages.Add(responseMessage);
        Debug.Log(responseMessage.Content);

        // Send the entire chat to OpenAI to get the next message
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        ChatMessage correctMessage = new ChatMessage();
        correctMessage.Role = chatResult.Choices[0].Message.Role;
        correctMessage.Content = chatResult.Choices[0].Message.Content;
        messages.Add(correctMessage);
        Debug.Log(correctMessage.Content);

        if (correctMessage.Content == "Yes.")
        {
            rightWrongField.text = "Good Job!";
        }
        else
        {
            rightWrongField.text = "Incorrect.";
        }
    }

    private void ButtonPressed(bool correct)
    {
        if (correct) Debug.Log("GOOD JOB");
        else Debug.Log("RIP, THATS THE WRONG ANSWER");
    }


    private async void AnswerQuestion()
    {

        // Disable the buttons
        okButton.enabled = false;


        // Submit a response to chat
        ChatMessage userAnswer = new ChatMessage();
        userAnswer.Role = ChatMessageRole.User;
        String answerText = "";
        /*if (inputField.text == "CHEAT")
        {
            answerText = "What is the correct answer?";
        }   
        else
        {
            answerText = string.Format("Is {0} the correct answer? Reply with only a yes or no.", inputField.text);
        }*/
        
        userAnswer.Content = answerText;

        if (userAnswer.Content.Length > 100) userAnswer.Content = userAnswer.Content.Substring(0, 100);

        Debug.Log(string.Format("{0}: {1}", userAnswer.Role, userAnswer.Content));

        messages.Add(userAnswer);


        // Send the entire chat to OpenAI to get the next message
        var chatResultYN = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 50,
            Messages = messages
        });

        // Get the response message
        ChatMessage responseMessageYN = new ChatMessage();
        responseMessageYN.Role = chatResultYN.Choices[0].Message.Role;
        responseMessageYN.Content = chatResultYN.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", responseMessageYN.Role, responseMessageYN.Content));

        // Add the response to the list of messages
        messages.Add(responseMessageYN);

        // Update the text field with the response
        //textField.text = string.Format("You: {0}\n\nGuard: {1}", userMessage.Content, responseMessage.Content);

        //Re-enable the Buttons
        okButton.enabled = true;
    }
}
