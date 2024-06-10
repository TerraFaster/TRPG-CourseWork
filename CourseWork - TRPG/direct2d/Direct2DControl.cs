using System.Diagnostics;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

namespace CourseWork___TRPG.direct2d
{
    /*
     * Class that represents a Direct2D control.
     */
    public class Direct2DControl : Control
    {
        // Field for stopwatch to calculate FPS
		private Stopwatch stopwatch;
        // Field for current FPS
		private float currentFPS;

        public float CurrentFPS
        { get { return currentFPS; } }

        // Event for rendering
        public event Action<EnhancedRenderTarget> Render;

        public Direct2DControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

			this.stopwatch = new Stopwatch();
		}

        // Control overrides
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RenderInternal();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Direct2D.Instance.Resize(ClientSize.Width, ClientSize.Height);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Direct2D.Instance.Dispose();

            base.Dispose(disposing);
        }

        // Method for rendering
        private void RenderInternal()
        {
            EnhancedRenderTarget renderTarget = Direct2D.Instance.RenderTarget;
            SwapChain swapChain = Direct2D.Instance.SwapChain;

            if (renderTarget == null)
                return;

            renderTarget.BeginDraw();
            renderTarget.Clear(new RawColor4(0, 0, 0, 1));

            // Invoke the Render event
            Render?.Invoke(renderTarget);

            renderTarget.EndDraw();
            swapChain.Present(1, PresentFlags.None);

			// Calculate current FPS
			if (!this.stopwatch.IsRunning)
			{
				this.stopwatch.Start();
				this.currentFPS = 0;
			}
			else
			{
				this.currentFPS = 1.0f / (float)stopwatch.Elapsed.TotalSeconds;
				this.stopwatch.Restart();
			}
		}
    }
}
