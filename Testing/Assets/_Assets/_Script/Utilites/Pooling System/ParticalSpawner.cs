using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Abhishek.Utils {
    public class ParticalSpawner : MonoBehaviour {
        
        private static ParticalSpawner current{get;set;}

        private void Awake(){
            current = this;
        }

        public static void SpawnEffect(string effectName,Vector3 spawnPoint,Quaternion SpawnRotation){
            GameObject EffectObject = ObjectPoolingManager.SpawnFromPool(effectName,spawnPoint,SpawnRotation);
            if(EffectObject.TryGetComponent<ParticleSystem>(out ParticleSystem effect)){
                effect.Play();
            }
        }

    }

}