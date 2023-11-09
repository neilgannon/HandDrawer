using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandTracker
{
    public class Camera
    {
        public Matrix View { get; set; }
        public Matrix Projection { get; set; }
        float nearPlane = 0.5f;
        float farPlane = 1000f;
        Vector3 position = Vector3.Zero;
        Vector3 direction = Vector3.Forward;
        Vector3 upDirection = Vector3.Up;

        public Camera(Vector3 position, Vector3 direction)
        {
            this.position = position;
            this.direction = direction;
        }

        public void Initialize()
        {
            direction.Normalize();

            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.AspectRatio,
                nearPlane,
                farPlane);

            UpdateView();
        }

        public void UpdateView()
        {
            View = Matrix.CreateLookAt(
            position,
            position + direction,
            upDirection);
        }
    }
}
