using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    static class GameObjectExtensions
    {
        public static GameObject Instantiate(this GameObject gameObject)
        {
            GameObjectManager.AddGameOBJ?.Invoke(gameObject);
            return gameObject;
        }

        public static RigidBody2D AddRigidBody2D(this GameObject gameObject)
        {
            RigidBody2D rigidBody = new RigidBody2D(0,0);
            GameObjectManager.ADDRigidBody?.Invoke(rigidBody, gameObject);
            return rigidBody;
        }

        public static List<RigidBody2D> GetAttachedRigidBodies(this GameObject gameObject)
            => GameObjectManager.GetRigidBODY?.Invoke(gameObject);
        
        public static void Destroy(this RigidBody2D rigidBody)
            =>  GameObjectManager.RemoveRigidBody2?.Invoke(rigidBody);

        public static void AddCollider(this RigidBody2D rigidBody, ISolid solid)
        { GameObjectManager.AddCollider?.Invoke(solid, rigidBody); } 
        
        public static List<GameObject> GetParent(this RigidBody2D rigidBody)
        {
            List<GameObject> gameObjects = GameObjectManager.GetGameObjects?.Invoke();
            return gameObjects.FindAll((x) => x.ID == rigidBody.ID);
        }

        public static List<RigidBody2D> GetParent(this ISolid solid)
        {
            List<RigidBody2D> rigidBodies = GameObjectManager.GetRigidBodies?.Invoke();
            return rigidBodies.FindAll((x) => x.ID == solid.ID);
        }
        
        public static void Destroy(this ISolid solid)
            => GameObjectManager.RemoveCollider?.Invoke(solid);    
    }
}
