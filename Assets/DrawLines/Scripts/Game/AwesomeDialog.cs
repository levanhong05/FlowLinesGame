using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class AwesomeDialog : MonoBehaviour
{
		/// <summary>
		/// Number of stars for the AwesomeDialog.
		/// </summary>
		private StarsNumber starsNumber;

		/// <summary>
		/// Star sound effect.
		/// </summary>
		public AudioClip starSoundEffect;

		/// <summary>
		/// Awesome dialog animator.
		/// </summary>
		public Animator awesomeDialogAnimator;

		/// <summary>
		/// First star fading animator.
		/// </summary>
		public Animator firstStarFading;

		/// <summary>
		/// Second star fading animator.
		/// </summary>
		public Animator secondStarFading;

		/// <summary>
		/// Third star fading animator.
		/// </summary>
		public Animator thirdStarFading;

		public Image nextButtonImage;

		public Image awesomeDialogImage;

		public Sprite awesomeSprite;

		public Sprite gameoverSprite;

		public Image firstStar;
		public Image secondStar;
		public Image thirdStar;

		// Use this for initialization
		void Awake ()
		{
				///Setting up the references
				if (awesomeDialogAnimator == null) {
						awesomeDialogAnimator = GetComponent<Animator> ();
				}

				if (firstStarFading == null) {
						firstStarFading = GameObject.Find ("FirstStarFading").GetComponent<Animator> ();
				}

				if (secondStarFading == null) {
						secondStarFading = GameObject.Find ("SecondStarFading").GetComponent<Animator> ();
				}

				if (thirdStarFading == null) {
						thirdStarFading = GameObject.Find ("ThirdStarFading").GetComponent<Animator> ();
				}

				if (nextButtonImage == null) {
						nextButtonImage = GameObject.Find ("Next").GetComponent<Image> ();
				}

				if (awesomeDialogImage == null) {
					awesomeDialogImage = GameObject.Find ("AwesomeDialog").GetComponent<Image> ();
				}
		}

		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				///Hide the Awesome Dialog
				Hide ();
		}

		/// <summary>
		/// Show the Awesome Dialog.
		/// </summary>
		public void Show ()
		{
				if (awesomeDialogAnimator == null) {
						return;
				}
				awesomeDialogAnimator.SetTrigger ("Running");
		}

		/// <summary>
		/// Hide the Awesome Dialog.
		/// </summary>
		public void Hide ()
		{
				StopAllCoroutines ();
				awesomeDialogAnimator.SetBool ("Running", false);
				firstStarFading.SetBool ("Running", false);
				secondStarFading.SetBool ("Running", false);
				thirdStarFading.SetBool ("Running", false);
		}

		public void GameOver()
		{
				nextButtonImage.GetComponent<Button> ().gameObject.SetActive (false);
				awesomeDialogImage.sprite = gameoverSprite;

				//Hide star
				if (firstStar == null) {
					firstStar = GameObject.Find ("FirstStar").GetComponent<Image> ();
				}

				if (secondStar == null) {
					secondStar = GameObject.Find ("SecondStar").GetComponent<Image> ();
				}

				if (thirdStar == null) {
					thirdStar = GameObject.Find ("ThirdStar").GetComponent<Image> ();
				}

				firstStar.gameObject.SetActive (false);
				secondStar.gameObject.SetActive (false);
				thirdStar.gameObject.SetActive (false);
		}
		
		/// <summary>
		/// Fade stars Coroutine.
		/// </summary>
		/// <returns>The stars.</returns>
		public IEnumerator FadeStars ()
		{
				starsNumber = StarsRating.GetAwesomeDialogStarsRating (Timer.timeInSeconds, GameManager.movements, Mission.wantedMission.rowsNumber * Mission.wantedMission.colsNumber);

				float delayBetweenStars = 0.5f;
				if (starsNumber == StarsNumber.ZERO) {//Fade with Zero Star
						//nextButtonImage.GetComponent<Button> ().interactable = false;
				} else if (starsNumber == StarsNumber.ONE) {//Fade with One Star
						AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
						firstStarFading.SetTrigger ("Running");
				} else if (starsNumber == StarsNumber.TWO) {//Fade with Two Star
						AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
						firstStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
						secondStarFading.SetTrigger ("Running");
				} else if (starsNumber == StarsNumber.THREE) {//Fade with Three Star
						AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
						firstStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
						secondStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						AudioSource.PlayClipAtPoint (starSoundEffect, Vector3.zero);
						thirdStarFading.SetTrigger ("Running");
				}
				yield return 0;
		}

		public enum StarsNumber
		{
				ZERO,
				ONE,
				TWO,
				THREE
		}
}