using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace _2DGameEngine
{
    delegate void testDelegate();
    interface IGameObject { }
    interface IComponent { }
    interface IRigidBody : IComponent { }
    interface ILineRenderer : IComponent { }
    interface IShape  { }
    interface IPoint { }
    interface I2DPoint : IPoint {  }
    interface I3DPoint : IPoint {  }

    interface I4DPoint : IPoint { }
    

    interface ISolid : Iid{ };
    
    

    class GameObject : Iid
    {
        IPoint point;
        string id;

        public GameObject(IPoint point)
        {
            this.point = point;
            id = "";
        }

        private void GameLoop_onUpdate()
        {

        }

        public void Subscribe_Update() 
            => GameLoop.onUpdate += GameLoop_onUpdate;
        
        public void Unsubscribe_Update()
            => GameLoop.onUpdate -= GameLoop_onUpdate;

        

        public string ID { get => id; set => id = value; }

        public void setPoint<P>(P point) where P : IPoint
        { 
            this.point = point; 
        }
    }

    class RigidBody2D : Iid
    {
        Vector2
            linear_velocity,
            rotational_velocity,
            rotational_acceleration,
            rotational_angle,
            acceleration,
            position;
        double friction;
        string id;

        public RigidBody2D(double x, double y)
        {
            ChangeAll(x , y);
            GameLoop.onUpdate += GameLoop_onUpdate;
            friction = 0;
            id = "";
        }

        private void GameLoop_onUpdate()
        {
            rotational_velocity += rotational_acceleration;
            rotational_angle += rotational_velocity;

            linear_velocity += acceleration;
            position += linear_velocity;
        }

        public void ChangeAll(double x, double y)
        {   
            linear_velocity = new Vector2(x,y);
            rotational_velocity = new Vector2(x,y);
            rotational_acceleration = new Vector2(x, y);
            rotational_angle = new Vector2(x, y);
            acceleration = new Vector2(x,y);
            position = new Vector2(x,y);
        }
        
        public Vector2 Linear_Velocity { get => linear_velocity; set => linear_velocity = value; }
        public Vector2 Rotational_Acceleration { get => rotational_acceleration; set => rotational_acceleration = value; }
        public Vector2 Rotational_Velocity { get => rotational_velocity; set => rotational_velocity = value; }
        public Vector2 Acceleration { get => acceleration; set => acceleration = value; }
        public Vector2 Position { get => position; set => position = value; }
        public double Friction { get => friction; set => friction = value; }
        public string ID { get => id; set => id = value; }
    }
}