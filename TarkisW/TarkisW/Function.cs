using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TarkisW
{
    public class Function
    {
        private const string buttonNotExist = "Button {0} does not exist";
        private const string twoButtonsNotExist = "Button {0} or {1} does not exist";

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static int[] NameToPosition(string name)
        {
            int pos = int.Parse(name);
            int a = pos / 10;
            int b = pos % 10;
            int[] result = new int[2];

            result[0] = a;
            result[1] = b;

            return result;
        }

        public static bool AboveOf(string btn1, string btn2)
        {
            try
            {
                var button1 = (MyButton)Form1.ActiveForm.Controls.Find(btn1, true).Single();
                var button2 = (MyButton)Form1.ActiveForm.Controls.Find(btn2, true).Single();
                return button1.Position[1] + 1 == button2.Position[1];
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(twoButtonsNotExist, btn1, btn2));
                return false;
            }
        }

        public static bool BelowOf(string btn1, string btn2)
        {
            try
            {
                var button1 = (MyButton)Form1.ActiveForm.Controls.Find(btn1, true).Single();
                var button2 = (MyButton)Form1.ActiveForm.Controls.Find(btn2, true).Single();
                return button1.Position[1] - 1 == button2.Position[1];
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(twoButtonsNotExist, btn1, btn2));
                return false;
            }
        }

        public static bool LeftOf(string btn1, string btn2)
        {
            try
            {
                var button1 = (MyButton)Form1.ActiveForm.Controls.Find(btn1, true).Single();
                var button2 = (MyButton)Form1.ActiveForm.Controls.Find(btn2, true).Single();
                return button1.Position[0] + 1 == button2.Position[0];
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(twoButtonsNotExist, btn1, btn2));
                return false;
            }
        }

        public static bool RightOf(string btn1, string btn2)
        {
            try
            {
                var button1 = (MyButton)Form1.ActiveForm.Controls.Find(btn1, true).Single();
                var button2 = (MyButton)Form1.ActiveForm.Controls.Find(btn2, true).Single();
                return button1.Position[0] - 1 == button2.Position[0];
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(twoButtonsNotExist, btn1, btn2));
                return false;
            }
        }

        public static bool Small(string btn)
        {
            try
            {
                var button = (MyButton)Form1.ActiveForm.Controls.Find(btn, true).Single();
                return button.ButtonSize == ButtonSize.Small;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(buttonNotExist, btn));
                return false;
            }
        }

        public static bool Small()
        {
            return GetAllButtons().Any(button => button.ButtonSize == ButtonSize.Small);
        }

        public static bool Medium(string btn)
        {
            try
            {
                var button = (MyButton)Form1.ActiveForm.Controls.Find(btn, true).Single();
                return button.ButtonSize == ButtonSize.Medium;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(buttonNotExist, btn));
                return false;
            }
        }

        public static bool Medium()
        {
            return GetAllButtons().Any(button => button.ButtonSize == ButtonSize.Medium);
        }

        public static bool Large(string btn)
        {
            try
            {
                var button = (MyButton)Form1.ActiveForm.Controls.Find(btn, true).Single();
                return button.ButtonSize == ButtonSize.Large;
            }
            catch (InvalidOperationException) 
            {
                MessageBox.Show(string.Format(buttonNotExist, btn));
                return false;
            }
        }

        public static bool Large()
        {
            return GetAllButtons().Any(button => button.ButtonSize == ButtonSize.Large);
        }

        public static bool Square(string btn)
        {
            try
            {
                var button = (MyButton)Form1.ActiveForm.Controls.Find(btn, true).Single();
                return button.ButtonShape == ButtonShape.Square;
                   
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(buttonNotExist, btn));
                return false;
            }
        }

        public static bool Square()
        {
            return GetAllButtons().Any(button => button.ButtonShape == ButtonShape.Square);
        }

        public static bool Circle(string btn)
        {
            try
            {
                var button = (MyButton)Form1.ActiveForm.Controls.Find(btn, true).Single();
                return button.ButtonShape == ButtonShape.Circle;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(buttonNotExist, btn));
                return false;
            }
        }
        
        public static bool Circle()
        {
            return GetAllButtons().Any(button => button.ButtonShape == ButtonShape.Circle);
        }

        public static bool Triangle(string btn)
        {
            try
            {
                var button = (MyButton)Form1.ActiveForm.Controls.Find(btn, true).Single();
                return button.ButtonShape == ButtonShape.Triangle;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show(string.Format(buttonNotExist, btn));
                return false;
            }
        }

        public static bool Triangle()
        {
            return GetAllButtons().Any(button => button.ButtonShape == ButtonShape.Triangle);
        }

        private static IEnumerable<MyButton> GetAllButtons()
        {
            var panel = (Panel)Form1.ActiveForm.Controls.Find("panel1", true).Single();
            return panel.Controls
                .OfType<Panel>()
                .Select(p => p.Controls.OfType<MyButton>().SingleOrDefault())
                .Where(b => b != null);
        }
    }
}
