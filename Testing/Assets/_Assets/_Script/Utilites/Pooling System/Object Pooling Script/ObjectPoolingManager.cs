using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
namespace Abhishek.Utils{
    [DefaultExecutionOrder(-20)]
    public class ObjectPoolingManager : Singleton<ObjectPoolingManager>{

        #region Singelton.

        protected override void Awake(){
            base.Awake();
            DontDestroyOnLoad(this.gameObject);
            Init();
        }

        #endregion
        [SerializeField] private List<PoolSO> pools = new List<PoolSO>();
        private Dictionary<string , Queue<GameObject>> poolDictionary;
        private List<IPooledObject> pooledObjectsList = new List<IPooledObject>();
        private GameObject parentObj;

        private void Init(){
            poolDictionary = new Dictionary<string, Queue<GameObject>>();
            pooledObjectsList = new List<IPooledObject>();
            CreatePool();
        }

        public void CreatePool(){
            foreach(PoolSO pool in pools){
                parentObj = new GameObject(pool.name + " Pooled Object Parent");
                parentObj.transform.SetParent(transform);
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for(int i = 0; i < pool.size; i++){
                    if(pool.prefabs != null){
                        GameObject obj = Instantiate(pool.prefabs) as GameObject;
                        obj.SetActive(false);
                        obj.transform.SetParent(parentObj.transform);
                        if(obj.TryGetComponent<IPooledObject>(out IPooledObject pooledObject)){
                            pooledObject.SetOrignalParent(parentObj.transform);
                        }
                        obj.name = string.Concat(pool.name ," ",obj.transform.GetSiblingIndex().ToString());
                        objectPool.Enqueue(obj);
                    }
                }
                poolDictionary.Add(pool.name,objectPool);
            }
        }
        private string GetRandomTag(){
            int randomNum = Random.Range(0,pools.Count);
            return pools[randomNum].name;
        }

        public GameObject SpawnRandomFromPool(Vector3 _spawnPoint,Quaternion _rotations){
            return SpawnFromPool(GetRandomTag(),_spawnPoint,_rotations);
        }
        public static GameObject SpawnFromPool(string tag){
            return SpawnFromPool(tag,Vector3.zero,Quaternion.identity);
        }
        public static GameObject SpawnFromPool(string tag,Vector3 _spawnPosition,Quaternion _rotation,Transform parent){
            GameObject newObject = SpawnFromPool(tag,_spawnPosition,_rotation);
            newObject.transform.SetParent(parent);
            return newObject;
        }
        
        public static GameObject SpawnFromPool(string tag,Vector3 _spawnPosition,Quaternion _rotation){
            if(!Instance.poolDictionary.ContainsKey(tag)){
                Debug.Log("Pool With the " + tag + " is not Found");
                return null;
            }
            GameObject objectToSpawn = Instance.poolDictionary[tag].Dequeue();
            objectToSpawn.transform.position = _spawnPosition;
            objectToSpawn.transform.rotation = _rotation;
            objectToSpawn.SetActive(true);

            if(objectToSpawn.TryGetComponent<IPooledObject>(out IPooledObject pooledObject)){
                pooledObject.OnObjectReuse();
                if(!Instance.pooledObjectsList.Contains(pooledObject)){
                    Instance.pooledObjectsList.Add(pooledObject);
                }
            }
            Instance.poolDictionary[tag].Enqueue(objectToSpawn);
            return objectToSpawn;
        }
        public static void ResetPools(Action OnPoolReset){
            for (int i = 0; i < Instance.pooledObjectsList.Count; i++) {
                Instance.pooledObjectsList[i].DestroyNow();
            }
            Instance.pooledObjectsList = new List<IPooledObject>();
            OnPoolReset?.Invoke();
        }
    }

}
