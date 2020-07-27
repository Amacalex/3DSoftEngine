using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    delegate void change2GameObjectPool(bool change);
    delegate void addGameObj(GameObject g);
    delegate void addGameObj2(int index, GameObject gameObject);
    delegate void removeGameObj(GameObject g);
    delegate void removeGameObj2(int index);
    delegate List<GameObject> getGameObjectPool();
    delegate List<RigidBody2D> getRigidBody(GameObject parent);
    delegate void addRigidBody(RigidBody2D rigidBody, GameObject parent);
    delegate void addRigidBody2(int index,RigidBody2D rigidBody, GameObject parent);
    delegate void removeRigidBody(GameObject parent);
    delegate void removeRigidBody2(RigidBody2D rigidBody);
    delegate void addCollider(ISolid collider, RigidBody2D rigidBody);
    delegate void removeCollider(ISolid collider);
    delegate List<RigidBody2D> getRigidBodies();
    

    static class World
    {
        public static void Initialize()
        {
            ID_System.Init();
            GameObjectManager.Init();
            GameLoop.Init();
        }
    }

    static class GameObjectManager
    {
        static event addGameObj AddGameObject;
        static event removeGameObj RemoveGameObject;
        static event addGameObj2 AddGameObject2;
        static event getGameObjectPool GetGameObjectPool;
        static event change2GameObjectPool Changed;
        static event addRigidBody AddRigidBody;
        static event addRigidBody2 AddRigidBody2;
        static event removeRigidBody RemoveRigidBody;
        static event removeRigidBody2 OnRemoveRigidBody2;
        static event getRigidBody GetRigidBody;
        static event addCollider OnAddCollider;
        static event removeCollider OnRemoveCollider;
        static event getRigidBodies OnGetRigidBodies;
        

        static List<GameObject> gameObjects;
        static List<RigidBody2D> rigidBodies;
        static List<ISolid> colliders;
        
        public static void Init()
        {
            gameObjects = new List<GameObject>();
            rigidBodies = new List<RigidBody2D>();
            colliders = new List<ISolid>();
            AddGameObject += GameObjectManager_AddGameObj;
            RemoveGameObject += GameObjectManager_RemoveGameObject;
            AddGameObject2 += GameObjectManager_AddGameObject2;
            GetGameObjectPool += GameObjectManager_GetGameObjectPool;
            AddRigidBody += GameObjectManager_AddRigidBody;
            AddRigidBody2 += GameObjectManager_AddRigidBody2;
            RemoveRigidBody += GameObjectManager_RemoveRigidBody;
            GetRigidBody += GameObjectManager_GetRigidBody;
            OnAddCollider += GameObjectManager_OnAddCollider;
            OnRemoveCollider += GameObjectManager_OnRemoveCollider;
            OnGetRigidBodies += GameObjectManager_OnGetRigidBodies;
            OnRemoveRigidBody2 += GameObjectManager_OnRemoveRigidBody2;
        }

        private static void GameObjectManager_OnRemoveRigidBody2(RigidBody2D rigidBody)
            =>  rigidBodies.Remove(rigidBody);
        public static removeRigidBody2 RemoveRigidBody2 => OnRemoveRigidBody2;
        

        private static List<RigidBody2D> GameObjectManager_OnGetRigidBodies()
            => rigidBodies;
        
        private static void GameObjectManager_OnRemoveCollider(ISolid collider)
            => colliders.Remove(collider);
        
        private static void GameObjectManager_OnAddCollider(ISolid collider, RigidBody2D rigidBody)
        {
            ID_System.GetSetChildID(collider, rigidBody);
            colliders.Add(collider);
            
        }

        private static List<RigidBody2D> GameObjectManager_GetRigidBody(GameObject parent)
            => rigidBodies.FindAll((x) => x.ID == parent.ID);

        public static getRigidBodies GetRigidBodies => OnGetRigidBodies;

        public static addCollider AddCollider => OnAddCollider;
        public static removeCollider RemoveCollider => OnRemoveCollider;

        public static getRigidBody GetRigidBODY => GetRigidBody;
        public static addGameObj AddGameOBJ => AddGameObject;
        public static addGameObj2 AddGameOBJ2 => AddGameObject2;
        public static removeGameObj RemoveGameOBJ => RemoveGameObject;
        
        public static addRigidBody ADDRigidBody => AddRigidBody;
        public static addRigidBody2 ADDRigidBody2 => AddRigidBody2;
        public static removeRigidBody RemoveRigidBODY => RemoveRigidBody;
        public static getGameObjectPool GetGameObjects => GetGameObjectPool;
        

        private static void GameObjectManager_AddGameObj(GameObject g)
        {
            gameObjects.Add(g);
            ID_System.GetnewID(g, "GameObject");
        }

        private static void GameObjectManager_RemoveRigidBody(GameObject parent)
            => rigidBodies.Remove(rigidBodies.Find((x) => x.ID == parent.ID));
        
        private static void GameObjectManager_AddRigidBody2(int index, RigidBody2D child, GameObject parent)
        {
            if (index >=0 && index < rigidBodies.Count) { rigidBodies[index] = child; }
            else { rigidBodies.Add(child); }
            ID_System.GetSetChildID?.Invoke(child, parent);
        }

        private static void GameObjectManager_AddRigidBody(RigidBody2D child, GameObject parent)
        { 
            rigidBodies.Add(child);
            ID_System.GetSetChildID?.Invoke(child, parent);   
        }

        private static List<GameObject> GameObjectManager_GetGameObjectPool()
            => gameObjects;
        
        private static void GameObjectManager_AddGameObject2(int index, GameObject gameObject)
        {
            
            ID_System.GetnewID?.Invoke(gameObject, "GameObject");
            if (index >= 0 && index < gameObjects.Count)
            {
                Changed?.Invoke(true);
                gameObjects[index] = gameObject;
            }
            else { Changed?.Invoke(false); }
        }

        private static void GameObjectManager_RemoveGameObject(GameObject g)
        {
            Changed?.Invoke(true);
            RemoveRigidBody?.Invoke(g);
            gameObjects.Remove(g);
        }
    }
}
