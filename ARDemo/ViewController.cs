using ARKit;
using Foundation;
using SceneKit;
using System;
using UIKit;

namespace ARDemo
{
    public partial class ViewController : UIViewController
    {
        private readonly ARSCNView sceneView;


        public ViewController(IntPtr handle) : base(handle)
        {
            this.sceneView = new ARSCNView
            {
                AutoenablesDefaultLighting = true,
                Delegate = new FaceDetection()
            };

            this.View.AddSubview(this.sceneView);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.sceneView.Frame = this.View.Frame;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var faceTrackingConfiguration = new ARFaceTrackingConfiguration()
            {
                LightEstimationEnabled = true,
                MaximumNumberOfTrackedFaces = 1
            };

            this.sceneView.Session.Run(faceTrackingConfiguration);
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            this.sceneView.Session.Pause();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }
    }

    
}