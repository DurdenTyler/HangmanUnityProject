using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class HangManGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textFieldGuessedWord;
        
        [SerializeField] private TextMeshProUGUI _textFieldHP;
        
        [SerializeField] private int hp = 7;
        
        private readonly List<char> guessedLetters = new();
        
        private readonly List<char> wrongTriedLetters = new();
        
        private string[] words =
        {
            "Cat",
            "Dog",
            "Car",
            "Weather",
            "Flat",
            "Building"
        };

        private string wordToGuess = "";

        private KeyCode lastKeyPressed;

        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);
            wordToGuess = words[randomIndex];
        }

        private void OnGUI()
        {
            var e = Event.current;

            if (e.isKey)
            {
                if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
                {
                    lastKeyPressed = e.keyCode;
                    ProcessKey(e.keyCode);
                }
            }
        }

        private void ProcessKey(KeyCode key)
        {
            var wordUppercase = wordToGuess.ToUpper();
            char pressedKey = key.ToString()[0];
            string stringToPrint = "";

            if (!wordUppercase.Contains(pressedKey) && !wrongTriedLetters.Contains(pressedKey))
            {
                wrongTriedLetters.Add(pressedKey);
                hp -= 1;
                _textFieldHP.text = $"Your life: {hp.ToString()}";

                if (hp <= 0)
                {
                    print("You lost");
                }
                else
                {
                    print($"This letter isn't in word, hp left {hp}");
                }
            }

            if (wordUppercase.Contains(pressedKey) && !guessedLetters.Contains(pressedKey))
            {
                guessedLetters.Add(pressedKey);
            }
            
            for (int i = 0; i < wordUppercase.Length; i++)
            {
                char letterInWord = wordUppercase[i];

                if (guessedLetters.Contains(letterInWord))
                {
                    stringToPrint += letterInWord;
                }
                else
                {
                    stringToPrint += "_";
                } 
            }

            if (wordUppercase == stringToPrint)
            {
                print($"Congratulations, you won^) the guess word is {wordUppercase}");
                _textFieldGuessedWord.text = wordUppercase;
            }
            else
            {
                print(stringToPrint);
                _textFieldGuessedWord.text = stringToPrint;
            }
        }
    }
}