using System.Collections;
using Game.Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts
{
	public class MenuScript : MonoBehaviour
	{
		public InputField seedInputField;
		public AudioSource audioSource;

		public void PlaySeed()
		{
			StartCoroutine(PlaySeedInternal());
		}

		private IEnumerator PlaySeedInternal()
		{
			audioSource.Play();
			yield return new WaitUntil(() => !audioSource.isPlaying);
			Statics.Seed = seedInputField.text;
			SceneManager.LoadScene("Ingame");
		}
	
		public void PlayChallenge()
		{
			StartCoroutine(PlayChallengeInternal());
		}

		private IEnumerator PlayChallengeInternal()
		{
			audioSource.Play();
			yield return new WaitUntil(() => !audioSource.isPlaying);
			Statics.Seed = "ECZOEY";
			SceneManager.LoadScene("Ingame");
		}
	
		public void Exit()
		{
			StartCoroutine(ExitInternal());
		}
	
		private IEnumerator ExitInternal()
		{
			audioSource.Play();
			yield return new WaitUntil(() => !audioSource.isPlaying);
			Application.Quit();
		}
	}
}