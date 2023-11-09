using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SharpDX.MediaFoundation;
using SharpDX.Direct2D1.Effects;

namespace HandTracker
{
    public class BasicModel
    {
        Matrix world;
        Model model;
        Matrix[] boneTransforms;

        //Blender has weird scaling, halved the scale of everything.
        float blenderToXNAScale = 0.5f;

        public BasicModel(Vector3 startingPosition)
        {
            world = Matrix.Identity * Matrix.CreateScale(blenderToXNAScale) * Matrix.CreateTranslation(startingPosition);
        }

        // If using the hand model, flip the scale for the right hand
        public BasicModel(Vector3 startingPosition, bool shouldFlip)
        {
            float xscale = shouldFlip ? -blenderToXNAScale : blenderToXNAScale;
            world = Matrix.Identity * Matrix.CreateScale(xscale, blenderToXNAScale, blenderToXNAScale) * Matrix.CreateTranslation(startingPosition);
        }

    public void Initialize(ContentManager contentManager)
        {
            model = contentManager.Load<Model>("SM_Sphere");
            //model = contentManager.Load<Model>("SM_Hand");

            boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);
        }

        public void SetPosition(Vector3 position) 
        {
            world *= Matrix.CreateTranslation(position);
        }

        public void Draw(Camera camera)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                    effect.World = boneTransforms[mesh.ParentBone.Index] * world;

                    effect.PreferPerPixelLighting = true;
                    effect.EnableDefaultLighting();
                }

                mesh.Draw();
            }
        }
    }
}
