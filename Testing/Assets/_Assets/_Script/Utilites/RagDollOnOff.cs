using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RagDollOnOff : MonoBehaviour {
    [SerializeField] private Rigidbody orignalRb;
    [SerializeField] private Collider orignalCollider;
    [SerializeField] private Rigidbody[] rbs;
    
    [SerializeField] private Collider[] colliders;
    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }
    private void Start(){
        OnOffRagdoll(false,Vector3.zero);
    }
    
    public void OnOffRagdoll(bool on,Vector3 throwHitForceDirection){
        if(on){
            if(orignalRb != null){
                orignalRb.isKinematic = true;
            }
            if(orignalCollider != null){
                orignalCollider.isTrigger = true;
            }
            animator.enabled = false;
            foreach(Rigidbody rb in rbs){
                rb.isKinematic = false;
                rb.AddForce(throwHitForceDirection * 4,ForceMode.Impulse);
            }
            foreach(Collider coli in colliders){
                coli.isTrigger = false;
            }
        }else{
            if(orignalRb != null){
                orignalRb.isKinematic = false;
            }
            if(orignalCollider != null){
                orignalCollider.isTrigger = false;
            }
            animator.enabled = true;
            foreach(Rigidbody rb in rbs){
                rb.isKinematic = true;
            }
            foreach(Collider coli in colliders){
                coli.isTrigger = true;
            }
        }
    }

    
}
