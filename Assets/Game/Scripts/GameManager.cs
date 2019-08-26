using System;
using System.Text;
using Game.Code;
using Game.Code.Enums;
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
		public Image analyzeButton;
		public Text seedText;
		public Text scoreText;
		public AudioSource soundA;
		public AudioSource soundB;
		public AudioSource soundC;
		public Image zoey;
		public string seed;
		public int score;
		public int highScore;
		private bool _colorize;

		public void Awake()
		{
			Statics.GameManager = this;
		}

		public void Start()
		{
			seed = string.IsNullOrWhiteSpace(Statics.Seed) ? new Random().Next(0, 999999999).ToString("000000000") : Statics.Seed;

			mazeGenerator.seed = seed;
			mazeGenerator.GenerateMaze();

			if (seed == "ECZOEY")
				highScore = 7500;

			SetSeedText();
			AnalyzeMaze();
		}

		private void SetSeedText()
		{
			seedText.text = $"Seed{Environment.NewLine}==========={Environment.NewLine}{seed}";
		}

		private void SetScoreText(bool solvable)
		{
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Highscore");
			stringBuilder.AppendLine("===========");
			stringBuilder.AppendLine(highScore.ToString());
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("Score");
			stringBuilder.AppendLine("===========");
			stringBuilder.AppendLine(score.ToString());
			if (!solvable)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("Exit not");
				stringBuilder.AppendLine("reachable.");
			}

			scoreText.text = stringBuilder.ToString();
		}

		public void ToggleColorize()
		{
			_colorize = !_colorize;
			analyzeButton.color = _colorize ? Color.gray : Color.white;
			AnalyzeMaze();
		}

		public void AnalyzeMaze()
		{
			var result = MazeAnalzyer.AnalyzeMaze(maze);

			score = result.Rating;

			if (result.Solvable && score > highScore)
			{
				soundC.Play();
				highScore = score;
				if (seed == "ECZOEY")
					zoey.gameObject.SetActive(true);
			}

			for (int x = 0; x < maze.Width; x++)
			{
				for (int y = 0; y < maze.Height; y++)
				{
					Color targetColor;

					if (_colorize)
					{
						switch (result.MazeTileClassifications[x, y])
						{
							case MazeTileResult.Unreachable:
								targetColor = Color.gray;
								break;
							case MazeTileResult.Reachable:
								targetColor = Color.yellow;
								break;
							case MazeTileResult.ShortestPath:
								targetColor = Color.green;
								break;
							default:
								throw new ArgumentOutOfRangeException();
						}
					}
					else
					{
						targetColor = Color.white;
					}

					maze.MazeTiles[x, y].SpriteRenderer.color = targetColor;
				}
			}

			SetScoreText(result.Solvable);
		}

		public void BackToMenu()
		{
			SceneManager.LoadScene("Menu");
		}
	}
}