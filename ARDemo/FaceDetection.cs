using System;
using ARKit;
using SceneKit;
using UIKit;

namespace ARDemo
{
    public class FaceDetection : ARSCNViewDelegate
    {
        public override void DidAddNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARFaceAnchor)
            {
                var faceGeometry = ARSCNFaceGeometry.Create(renderer.GetDevice());
                node.Geometry = faceGeometry;
                node.Geometry.FirstMaterial.FillMode = SCNFillMode.Fill;
                node.Opacity = 0.8f;
            }
        }

        public override void DidUpdateNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARFaceAnchor)
            {
                var faceAnchor = anchor as ARFaceAnchor;
                var faceGeometry = node.Geometry as ARSCNFaceGeometry;
                var expressionThreshold = 0.5f;

                faceGeometry.Update(faceAnchor.Geometry);

                if (faceAnchor.BlendShapes.EyeWideLeft > expressionThreshold
                    || faceAnchor.BlendShapes.EyeWideRight > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Green);
                    return;
                }

                if (faceAnchor.BlendShapes.EyeBlinkLeft > expressionThreshold
                    || faceAnchor.BlendShapes.EyeBlinkRight > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Blue);
                    return;
                }

                if (faceAnchor.BlendShapes.MouthFrownLeft > expressionThreshold
                    || faceAnchor.BlendShapes.MouthFrownRight > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Black);
                    return;
                }

                if (faceAnchor.BlendShapes.MouthSmileLeft > expressionThreshold
                    || faceAnchor.BlendShapes.MouthSmileRight > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.SystemPinkColor);
                    return;
                }

                if (faceAnchor.BlendShapes.BrowOuterUpLeft > expressionThreshold
                    || faceAnchor.BlendShapes.BrowOuterUpRight > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Cyan);
                    return;
                }

                if (faceAnchor.BlendShapes.EyeLookOutLeft > expressionThreshold
                    || faceAnchor.BlendShapes.EyeLookOutRight > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Magenta);
                    return;
                }

                if (faceAnchor.BlendShapes.TongueOut > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Red);
                    return;
                }

                if (faceAnchor.BlendShapes.MouthFunnel > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Yellow);
                    return;
                }

                if (faceAnchor.BlendShapes.CheekPuff > expressionThreshold)
                {
                    ChangeFaceColour(node, UIColor.Orange);
                    return;
                }

                ChangeFaceColour(node, UIColor.White);
            }
        }

        private void ChangeFaceColour(SCNNode faceGeometry, UIColor colour)
        {
            var material = new SCNMaterial();
            var lips = UIImage.FromBundle("lipsTemplate");
            material.Diffuse.Contents = lips;
            material.Ambient.Contents = UIColor.Red;
            //material.FillMode = SCNFillMode.Fill;

            faceGeometry.Geometry.FirstMaterial = material;
        }
        public override void DidRemoveNode(ISCNSceneRenderer renderer, SCNNode node, ARAnchor anchor)
        {
            if (anchor is ARFaceAnchor)
            {
            }
        }
    }
}
