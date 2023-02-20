using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class HangManGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textFieldGuessedWord;
        
        [SerializeField] private TextMeshProUGUI _textFieldHP;
        
        [SerializeField] private TextMeshProUGUI _textFieldWrongLetters;
        
        [SerializeField] private TextMeshProUGUI _textFieldClues;
        
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
        
        private string[] clues =
        {
            "a pet who says meow",
            "a pet who says whoam",
            "four wheels",
            "degree what is it?",
            "you libe in ...",
            "workers build it"
        };

        private string wordToGuess = "";

        private KeyCode lastKeyPressed;
        
        private string clue = "";

        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);
            wordToGuess = words[randomIndex];
            clue = clues[randomIndex];
            _textFieldClues.text = clue;
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
            _textFieldWrongLetters.text = string.Join(", ", wrongTriedLetters);
            _textFieldHP.text = $"Your life: {hp.ToString()}";
            var wordUppercase = wordToGuess.ToUpper();
            char pressedKey = key.ToString()[0];
            string stringToPrint = "";

            if (!wordUppercase.Contains(pressedKey) && !wrongTriedLetters.Contains(pressedKey))
            {
                wrongTriedLetters.Add(pressedKey);
                hp -= 1;

                if (hp <= 0)
                {
                    print("You lost");
                    SceneManager.LoadSceneAsync("YouLost");
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
                SceneManager.LoadSceneAsync("YouWon");
            }
            else
            {
                _textFieldGuessedWord.text = stringToPrint;
                print(stringToPrint);
            }
        }
    }
}