using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class Orb : MonoBehaviour
{
    [SerializeField] private MMFeedbacks idleOuterFeedbacks;
    [SerializeField] private MMFeedbacks idleInnerFeedbacks;

    [SerializeField] private MMFeedbacks hitOuterFeedbacks;
    [SerializeField] private MMFeedbacks hitInnerFeedbacks;

    [SerializeField] private AudioSource hitAudioSource;

    [SerializeField] private ParticleSystem hitParticleSystem;

    [SerializeField] private float hitToIdleTime = 2f;

    private bool recharging = false;

    // Start is called before the first frame update
    void Start()
    {
        idleOuterFeedbacks.PlayFeedbacks();
        idleInnerFeedbacks.PlayFeedbacks();
    }

    public bool IsRecharging() {
        return recharging;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Paper") && !recharging && !other.GetComponent<Paper>().IsReturning()){
            idleOuterFeedbacks.StopFeedbacks();
            idleInnerFeedbacks.StopFeedbacks();

            hitOuterFeedbacks.PlayFeedbacks();
            hitInnerFeedbacks.PlayFeedbacks();

            hitParticleSystem.Play();
            hitAudioSource.Play();
            StartCoroutine(WaitAndGoToIdle());
        }
    }

    private IEnumerator WaitAndGoToIdle() {
        recharging = true;
        yield return new WaitForSeconds(hitToIdleTime);
        hitOuterFeedbacks.StopFeedbacks();
        hitInnerFeedbacks.StopFeedbacks();
        idleOuterFeedbacks.PlayFeedbacks();
        idleInnerFeedbacks.PlayFeedbacks();
        recharging = false;
    }
}
