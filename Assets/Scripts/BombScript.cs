using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Managers;

public class BombScript : MonoBehaviour, Interactable, IResetObject {
	[SerializeField]
	private GameObject FuseParticleSystem;
	[SerializeField]
	private ParticleSystem BombExplosion;

	[SerializeField]
	private AudioClip bombExplosion;

	private bool hasInteracted;

	private Vector3 initialPosition;
    private Vector3 initialScale;
    private Quaternion initialRotation;
	private Renderer thisMeshRenderer;
    LTDescr shakeTween;
    int timeTweenId;


    void Start ()
	{
		initialPosition = transform.localPosition;
        initialScale = transform.localScale;
        initialRotation = transform.rotation;

        thisMeshRenderer = gameObject.GetComponent<Renderer>();


	}

	public void Interact () 
	{
		if (!hasInteracted)
		{
            AudioManager.Singleton.PlaySFXLongtDurSound(AudioManager.SFK_LONGISH_DUR_ID.BOOING_CROWD);
            hasInteracted = true;
			QuickShake();
		}
	}

	public void ResetState()
	{
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        transform.localPosition = initialPosition;
        BombExplosion.Clear();
        thisMeshRenderer.enabled = true;
        transform.localScale = initialScale;
        transform.rotation = initialRotation;

        LeanTween.cancel(shakeTween.uniqueId);
        LeanTween.cancel(timeTweenId);
        hasInteracted = false;
    }


	public INTERACTION_TYPE GetInteractionType()
	{
		return INTERACTION_TYPE.BOMBA;
	}

	void QuickShake()
	{

        FuseParticleSystem.SetActive(true);
		float shakeAmt = 1f; // the degrees to shake the camera
		float shakePeriodTime = 0.25f; // The period of each shake
		float dropOffTime = 0.7f; // How long it takes the shaking to settle down to nothing
		shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, shakeAmt, shakePeriodTime)
			.setEase(LeanTweenType.easeShake).setLoopClamp().setRepeat(-1).setDelay(0.1f);
		LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate( 
			(float val)=>{
			shakeTween.setTo(Vector3.right*val);
		}
		).setEase(LeanTweenType.easeOutQuad).setDelay(0.1f);
		LeanTween.scale(gameObject, new Vector3(500f, 500f, 500f), 1f).setEaseInOutBounce().setOnComplete(() => StartCoroutine(CompleteExplosion()));
	}

	IEnumerator CompleteExplosion()
	{
		AudioManager.Singleton.PlaySFXShortDurSound(AudioManager.SFX_SHORT_DUR_ID.EXPLOSION);
        yield return new WaitForSeconds(.4f);
        BombExplosion.Play();
		MainCameraController.ShakeCameraMoreEpic();

		float gameOverAnimationSpeed = 3f;
		GameState.StartGameOverAnimations(gameOverAnimationSpeed);
		timeTweenId = LeanTween.value(1f, 0.02f, gameOverAnimationSpeed).setEaseOutQuad().setOnUpdate((float newValue) => Time.timeScale = newValue).setIgnoreTimeScale(true).setOnComplete(() => GameState.GameOver()).uniqueId;

		yield return new WaitForSecondsRealtime(.5f);
		thisMeshRenderer.enabled = false;
	}

}
