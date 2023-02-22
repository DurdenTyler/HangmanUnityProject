using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class HangManController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _wordToGuessText;

        [SerializeField] private TextMeshProUGUI _hpText;

        [SerializeField] private TextMeshProUGUI _wrongLetersText;

        [SerializeField] private TextMeshProUGUI _cluesText;

        [SerializeField] private int hp = 7;

        private readonly List<char> _guessedLetters = new();

        private readonly List<char> _wrongTriedLetters = new();

        private readonly string[] _words =
        {
            "Cat",
            "Dog",
            "Car",
            "Weather",
            "Flat",
            "Building"
        };

        private readonly string[] _clues =
        {
            "a pet who says meow",
            "a pet who says whoam",
            "four wheels",
            "degree what is it?",
            "you live in ...",
            "workers build it"
        };

        private string _wordToGuess = "";

        private KeyCode _lastKeyPressed;

        private string _clue = "";

        private void Start()
        {
            var randomIndex = Random.Range(0, _words.Length);
            _wordToGuess = _words[randomIndex];
            _clue = _clues[randomIndex];
            _cluesText.text = _clue;
        }

        private void OnGUI()
        {
            var e = Event.current;
            if (!e.isKey) return;
            if (e.keyCode == KeyCode.None || _lastKeyPressed == e.keyCode) return;
            _lastKeyPressed = e.keyCode;
            ProcessKey(e.keyCode);
        }

        private void ProcessKey(KeyCode key)
        {
            _wrongLetersText.text = string.Join(", ", _wrongTriedLetters);
            _hpText.text = $"Your life: {hp.ToString()}";
            var wordUppercase = _wordToGuess.ToUpper();
            char pressedKey = key.ToString()[0];
            string stringToPrint = "";

            if (!wordUppercase.Contains(pressedKey) && !_wrongTriedLetters.Contains(pressedKey))
            {
                _wrongTriedLetters.Add(pressedKey);
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

            if (wordUppercase.Contains(pressedKey) && !_guessedLetters.Contains(pressedKey))
            {
                _guessedLetters.Add(pressedKey);
            }

            foreach (var letterInWord in wordUppercase)
            {
                if (_guessedLetters.Contains(letterInWord))
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
                _wordToGuessText.text = stringToPrint;
                print(stringToPrint);
            }
        }
    }
}