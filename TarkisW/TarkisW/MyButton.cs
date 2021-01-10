using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TarkisW
{
    public class MyButton : Button
    {
        private readonly string _pathSquare = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Objects\", "square.png");
        private readonly string _pathCircle = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Objects\", "circle.png");
        private readonly string _pathTriangle = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Objects\", "triangle.png");
        private ButtonSize _buttonSize;

        public int[] Position { get; set; }
        public ButtonShape ButtonShape { get; set; }
        public ButtonSize ButtonSize
        {
            get => _buttonSize;
            set
            {
                _buttonSize = value;

                switch (value)
                {
                    case ButtonSize.Small:
                        Size = new Size(40, 40);
                        break;

                    case ButtonSize.Medium:
                        Size = new Size(60, 60);
                        break;

                    case ButtonSize.Large:
                        Size = new Size(80, 80);
                        break;
                }
            }
        }

        public void SetImage()
        {
            Image = Function.resizeImage(Image.FromFile(GetImagePath()), Size);
        }

        private string GetImagePath()
        {
            switch (ButtonShape)
            {
                case ButtonShape.Square:
                    return _pathSquare;

                case ButtonShape.Triangle:
                    return _pathTriangle;

                case ButtonShape.Circle:
                    return _pathCircle;

                default:
                    throw new InvalidDataException("Button shape must be Square, Triangle or Circle");
            }
        }
    }
}
