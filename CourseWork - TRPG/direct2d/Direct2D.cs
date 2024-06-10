using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using Device = SharpDX.Direct3D11.Device;
using Factory = SharpDX.Direct2D1.Factory;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using System.Drawing.Imaging;
using CourseWork___TRPG.game;

namespace CourseWork___TRPG.direct2d
{
    /*
     * Class that extends the RenderTarget class to provide additional functionality
     */
    public class EnhancedRenderTarget : RenderTarget
    {
        // Field to store the DirectWrite factory
        private SharpDX.DirectWrite.Factory writeFactory;

        public SharpDX.DirectWrite.Factory WriteFactory
        { get { return writeFactory; } }

		public EnhancedRenderTarget(
            Factory factory, Surface dxgiSurface, RenderTargetProperties properties
        ) : base(factory, dxgiSurface, properties)
        {
            writeFactory = new SharpDX.DirectWrite.Factory();
        }

        // Drawing functions
        /*
         * DrawText function that takes in a text string, text format, color, and position
         *  and draws the text at the specified position
         */
        public void DrawText(string text, TextFormat textFormat, RawColor4 color, PointF position)
        {
            using (var brush = new SolidColorBrush(this, color))
            {
                var textLayout = new TextLayout(writeFactory, text, textFormat, float.MaxValue, float.MaxValue);
                var textSize = textLayout.Metrics;

                DrawText(
                    text, textFormat,
                    new RawRectangleF(
                        position.X, position.Y,
                        position.X + textSize.Width, position.Y + textSize.Height
                    ),
                    brush
                );
            }
        }

        // Utility functions
        /*
         * MeasureText function that takes in a text string and text format
         *  and returns the metrics of the text
         */
        public TextMetrics MeasureText(string text, TextFormat textFormat)
        {
            TextLayout textLayout = new TextLayout(
                writeFactory, text, textFormat,
                float.MaxValue, float.MaxValue
            );
            return textLayout.Metrics;
        }

        /*
         * ConvertImageToSharpDXBitmap function that takes in a System.Drawing.Image
         *  and converts it to a SharpDX.Direct2D1.Bitmap
         */
        public Bitmap ConvertImageToSharpDXBitmap(System.Drawing.Image image)
        {
            Bitmap bmp = null;

            using (var bitmap = new System.Drawing.Bitmap(image))
            {
                var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    bitmap.PixelFormat
                );
                var dataStream = new DataStream(
                    bitmapData.Scan0, bitmapData.Stride * bitmapData.Height,
                    true, false
                );
                var properties = new BitmapProperties(
                    new SharpDX.Direct2D1.PixelFormat(
                        Format.B8G8R8A8_UNorm,
                        SharpDX.Direct2D1.AlphaMode.Premultiplied
                    )
                );

                bmp = new Bitmap(
                    this,
                    new Size2(
                        bitmap.Width, bitmap.Height
                    ),
                    dataStream, bitmapData.Stride, properties
                );

                bitmap.UnlockBits(bitmapData);
            }
            return bmp;
        }

        /*
         * ConvertSharpDXBitmapToImage function that takes in a SharpDX.Direct2D1.Bitmap
         *  and converts it to a System.Drawing.Image
         */
        public System.Drawing.Image ConvertSharpDXBitmapToImage(Bitmap bitmap)
        {
            var width = bitmap.PixelSize.Width;
            var height = bitmap.PixelSize.Height;
            var pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppPArgb;
            var stride = width * 4;

            var pixelData = new byte[width * height * 4];

            bitmap.CopyFromMemory(pixelData, stride);

            var gdiBitmap = new System.Drawing.Bitmap(width, height, pixelFormat);

            var bitmapData = gdiBitmap.LockBits(
                new Rectangle(0, 0, width, height),
                ImageLockMode.WriteOnly, pixelFormat
            );

            try
            {
                System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, bitmapData.Scan0, pixelData.Length);
            }
            finally
            {
                gdiBitmap.UnlockBits(bitmapData);
            }

            return gdiBitmap;
        }
    }

    /*
     * Class that provides a singleton instance of the Direct2D class
     */
    public class Direct2D
    {
        // Field to store the singleton instance
        public static Direct2D Instance = new Direct2D();

        // Field to store the device
        private Device device;
        // Field to store the swap chain
        private SwapChain swapChain;
        // Field to store the render target
        private EnhancedRenderTarget renderTarget;
        // Field to store the Direct2D factory
        private Factory factory;
        // Field to store the DirectWrite factory
        private SharpDX.DirectWrite.Factory writeFactory;

        public Device Device => device;
        public SwapChain SwapChain => swapChain;
        public EnhancedRenderTarget RenderTarget => renderTarget;
        public Factory Factory => factory;
        public SharpDX.DirectWrite.Factory WriteFactory => writeFactory;

		public void Initialize(Control output)
        {
            // Create the swap chain description
			var description = new SwapChainDescription()
            {
                BufferCount = 1, 
                ModeDescription = new ModeDescription(
                    output.ClientSize.Width, output.ClientSize.Height, 
                    new Rational(Config.Game.FRAME_RATE, 1), Format.R8G8B8A8_UNorm
                ), 
                Usage = Usage.RenderTargetOutput, 
                OutputHandle = output.Handle, 
                SampleDescription = new SampleDescription(1, 0), 
                SwapEffect = SwapEffect.Discard, 
				Flags = SwapChainFlags.AllowModeSwitch, 
                IsWindowed = true, 
			};

            // Create the device and swap chain
            Device.CreateWithSwapChain(
                DriverType.Hardware, DeviceCreationFlags.BgraSupport,
                description, out this.device, out this.swapChain
            );

            // Create the render target
            using (var backBuffer = this.swapChain.GetBackBuffer<Texture2D>(0))
            {
                using (var surface = backBuffer.QueryInterface<Surface>())
                {
                    this.factory = new Factory();
                    this.renderTarget = new EnhancedRenderTarget(
                        this.factory, surface,
                        new RenderTargetProperties(
                            new SharpDX.Direct2D1.PixelFormat(
                                Format.Unknown,
                                SharpDX.Direct2D1.AlphaMode.Premultiplied
                            )
                        )
                    );
                }
            }

            // Set the DirectWrite factorys
            this.writeFactory = renderTarget.WriteFactory;
        }

        public void Dispose()
        {
            renderTarget.Dispose();
            swapChain.Dispose();
            device.Dispose();
            factory.Dispose();
            writeFactory.Dispose();
        }

        /*
         * Method to resize the swap chain and render target
         */
        public void Resize(int width, int height)
        {
            if (this.renderTarget == null)
                return;

            this.renderTarget.Dispose();
            this.swapChain.ResizeBuffers(
                1, width, height,
                Format.R8G8B8A8_UNorm, SwapChainFlags.None
            );

            using (var backBuffer = swapChain.GetBackBuffer<Texture2D>(0))
            {
                using (var surface = backBuffer.QueryInterface<Surface>())
                {
                    this.renderTarget = new EnhancedRenderTarget(
                        this.factory, surface,
                        new RenderTargetProperties(
                            new SharpDX.Direct2D1.PixelFormat(
                                Format.Unknown,
                                SharpDX.Direct2D1.AlphaMode.Premultiplied
                            )
                        )
                    );
                }
            }
        }

        // Utility functions
        public TextMetrics MeasureText(string text, TextFormat textFormat)
        {
            return renderTarget.MeasureText(text, textFormat);
        }

        public Bitmap ConvertImageToSharpDXBitmap(System.Drawing.Image image)
        {
            return renderTarget.ConvertImageToSharpDXBitmap(image);
        }

        public System.Drawing.Image ConvertSharpDXBitmapToImage(Bitmap bitmap)
        {
            return renderTarget.ConvertSharpDXBitmapToImage(bitmap);
        }
    }
}
