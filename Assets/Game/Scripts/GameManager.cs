using System;
using System.Text;
using Game.Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

namespace Game.Scripts
{
	public class GameManager : MonoBehaviour
	{
		public Maze maze;
		public MazeGenerator mazeGenerator;
		public Text seedText;
		public Text scoreText;
		public string seed;
		public int score;
		public int highScore;

		public void Start()
		{
			seed = string.IsNullOrWhiteSpace(Statics.Seed) ? new Random().Next(0, 999999999).ToString("000000000") : Statics.Seed;

			mazeGenerator.seed = seed;
			mazeGenerator.GenerateMaze();

			if (seed == "EXTRAZOEY")
				highScore = 7500;

			SetSeedText();
			SetScoreText();
		}

		private void SetSeedText()
		{
			seedText.text = $"Seed{Environment.NewLine}========={Environment.NewLine}{seed}";
		}

		private void SetScoreText()
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Highscore");
			stringBuilder.AppendLine("=========");
			stringBuilder.AppendLine(highScore.ToString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Score");
			stringBuilder.AppendLine("=========");
			stringBuilder.AppendLine(score > 0 ? score.ToString() : "INVALID");
			scoreText.text = stringBuilder.ToString();
		}

		public void AnalyzeMaze()
		{
			var result = MazeAnalzyer.AnalyzeMaze(maze);

			score = result.Rating;

			if (score > highScore)
				highScore = score;

			SetScoreText();
		}

		public void BackToMenu()
		{
			SceneManager.LoadScene("Menu");
		}
	}
}