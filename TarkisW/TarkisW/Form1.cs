using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TarkisW
{
    public partial class Form1 : Form
    {
        private Panel[,] _board;
        private const int tileSize = 80;
        private const int gridSize = 5;

        TextBox selTB = null;
        Panel selPN = null;

        public Form1()
        {
            InitializeComponent();
            InitializeComboboxData();
            Panel1_Load();
            RegisterTextBoxEnterEvents();
        }

        private void InitializeComboboxData()
        {
            comboBoxShape.DataSource = Enum.GetValues(typeof(ButtonShape));
            comboBoxSize.DataSource = Enum.GetValues(typeof(ButtonSize));
            comboBoxShape.SelectedItem = ButtonShape.Square;
            comboBoxSize.SelectedItem = ButtonSize.Small;
        }

        private void RegisterTextBoxEnterEvents()
        {
            textBox1.Enter += tb_Enter;
            textBox2.Enter += tb_Enter;
            textBox3.Enter += tb_Enter;
            textBox4.Enter += tb_Enter;
            textBox5.Enter += tb_Enter;
        }

        private void tb_Enter(object sender, EventArgs e)
        {
            selTB = (TextBox)sender;
        }

        //Create board
        private void Panel1_Load()
        {
            _board = new Panel[gridSize, gridSize];

            //double for loops to handle all rows and columns
            for (var row = 0; row < gridSize; row++)
            {
                for (var col = 0; col < gridSize; col++)
                {
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(tileSize * row, tileSize * col),
                        Name = $"{row}{col}",
                        AllowDrop = true,
                        BackColor = (row + col) % 2 == 0 ? Color.White : Color.DarkGray
                    };

                    newPanel.DragEnter += (sender, e) => e.Effect = DragDropEffects.Move;
                    newPanel.DragDrop += (sender, e) => ((MyButton)e.Data.GetData(typeof(MyButton))).Parent = (Panel)sender;
                    newPanel.MouseClick += (sender, e) => selPN = (Panel)sender;

                    panel1.Controls.Add(newPanel);
                    _board[row, col] = newPanel;
                }
            }
        }
        
        private void button_MouseClicked(object sender, MouseEventArgs e)
        {
            var btn_c = sender as MyButton;
            selPN = (Panel)btn_c.Parent;
        }

        //create random object
        private void Btn_newo_Click(object sender, EventArgs e)
        {
            var shape = (ButtonShape)comboBoxShape.SelectedItem;
            var size = (ButtonSize)comboBoxSize.SelectedItem;
            var name = comboBoxName.GetItemText(comboBoxName.SelectedItem);
           
            if (selPN == null)
            {
                MessageBox.Show("Please choose a spawn position");
            }
            else
            {
                var btn = new MyButton();
                btn.Name = name;
                btn.ButtonShape = shape;
                btn.ButtonSize = size;
                btn.SetImage();
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;

                if (selPN.Controls.Count == 0)
                {
                    selPN.Controls.Add(btn);
                    btn.Position = Function.NameToPosition(btn.Parent.Name);
                    btn.MouseDown += new MouseEventHandler(btn_MouseDown);
                    btn.MouseDown += new MouseEventHandler(button_MouseClicked);
                    btn.Location = new Point((80 - btn.Width) / 2, (80 - btn.Height) / 2);
                    btn.Text = name;
                    comboBoxName.Items.Remove(name);
                }
                else
                {
                    MessageBox.Show("This position already contains an object");
                }
            }
        }

        //Random
        private void btnRnd_Click(object sender, EventArgs e)
        {
            string name;
            string[] names = new string[comboBoxName.Items.Count];

            for (int i = 0; i < comboBoxName.Items.Count; i++)
            {
                names[i] = comboBoxName.Items[i].ToString();
            }

            Random r = new Random();

            var shapes = Enum.GetValues(typeof(ButtonShape));
            var shape = (ButtonShape)shapes.GetValue(r.Next(shapes.Length));

            var sizes = Enum.GetValues(typeof(ButtonSize));
            var size = (ButtonSize)sizes.GetValue(r.Next(shapes.Length));

            int randomNa = r.Next(names.Length);
            name = names[randomNa];

            var spawn = new List<Panel>();
            
            foreach (var panel in _board)
            {
                if (panel.Controls.Count == 0)
                {
                    spawn.Add(panel);
                }
            }

            if (spawn.Any())
            {
                int num = r.Next(0, spawn.Count);

                var btn = new MyButton();
                spawn[num].Controls.Add(btn);

                btn.Name = name;
                btn.Text = name;
                btn.ButtonShape = shape;
                btn.ButtonSize = size;
                btn.SetImage();
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Position = Function.NameToPosition(btn.Parent.Name);
                btn.MouseDown += new MouseEventHandler(btn_MouseDown);
                btn.MouseDown += new MouseEventHandler(button_MouseClicked);
                btn.Location = new Point((80 - btn.Width) / 2, (80 - btn.Height) / 2);
                
                comboBoxName.Items.Remove(name);
            }
            else
            {
                MessageBox.Show("The board is full");
            }
        }
        //

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (var panel in _board)
            {
                panel.AllowDrop = panel.Controls.Count == 0;
            }
            
            var btn_c = sender as MyButton;
            btn_c.DoDragDrop(btn_c, DragDropEffects.Move);
            btn_c.Position = Function.NameToPosition(btn_c.Parent.Name);
            MessageBox.Show(btn_c.Position[0].ToString() + btn_c.Position[1].ToString());
        }

        //buttons 
        private void btnAnd_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnAnd.Text;
                selTB.Focus();
            }
        }

        private void btnOr_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnOr.Text;
                selTB.Focus();
            }
        }

        private void btnImply_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnImply.Text;
                selTB.Focus();
            }
        }

        private void btnIff_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnIff.Text;
                selTB.Focus();
            }
        }

        private void btnFAll_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnFAll.Text;
                selTB.Focus();
            }
        }

        private void btnTExists_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnTExists.Text;
                selTB.Focus();
            }
        }

        private void btnLb_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnLb.Text;
                selTB.Focus();
            }
        }

        private void btnRb_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnRb.Text;
                selTB.Focus();
            }
        }

        private void btnNot_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += btnNot.Text;
                selTB.Focus();
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            selTB.Clear();
        }

        //Clear all textbox
        public void ClearTextBoxes(Control.ControlCollection ctrlCollection)
        {
            foreach (Control ctrl in ctrlCollection)
            {
                if (ctrl is TextBoxBase)
                {
                    ctrl.Text = string.Empty;
                }
                else
                {
                    ClearTextBoxes(ctrl.Controls);
                }
            }
        }

        //Delete buttons
        private void btnDeleteO_Click(object sender, EventArgs e)
        {
            if (selPN == null)
            {
                MessageBox.Show("Please select an object you want to delete");
            }
            else
            {
                selPN.Controls.OfType<Button>().ToList().ForEach(button =>
                {
                    selPN.Controls.Remove(button);
                    button.Dispose();
                });
            }
        }

        private void btnDeleteOA_Click(object sender, EventArgs e)
        {
            panel1.Controls.OfType<Panel>().ToList().ForEach(panel =>
            {
                var button = panel.Controls.OfType<Button>().SingleOrDefault();

                if (button != null)
                {
                    panel.Controls.Remove(button);
                    button.Dispose();
                }
            });
        }

        private void btnSo1_Click(object sender, EventArgs e)
        {
            //label1.Text = Function.Square().ToString();
            //label2.Text = Function.Circle().ToString();
            //label3.Text = Function.Triangle().ToString();
            //label4.Text = Function.RightOf("a", "b").ToString();
            List<string> Arr = new List<string>();
            String s = "Square(a) ∧ Small(b) ∨ LeftOf(a,b)";
            Regex re = new Regex(@"\b(Square|Circle|Triangle|Small|Medium|Large|LeftOf|RightOf|AboveOf|BelowOf)\b\(+(?<btn1>[a-w])+(,?)+((?<btn2>[a-w])?)+\)");
            //MatchCollection items = re1.Matches(s);
            foreach(Match item in re.Matches(s))
            {
                string res;
                if (item.Groups["btn2"].ToString() == "")
                {
                    res = typeof(Function)
                        .GetMethod(item.Groups[1].ToString(), new Type[] { typeof(string) })
                        .Invoke(null, new object[] { item.Groups["btn1"].ToString() }).ToString();
                    MessageBox.Show(res);
                }
                else
                {
                    res = typeof(Function)
                        .GetMethod(item.Groups[1].ToString(), new Type[] { typeof(string) , typeof(string) })
                        .Invoke(null, new object[] { item.Groups["btn1"].ToString() , item.Groups["btn2"].ToString() }).ToString();
                    MessageBox.Show(res);
                }
                s = s.Replace(item.ToString(), res);
                //s.Replace(item.ToString(), res);
                MessageBox.Show(s);
                //MessageBox.Show(res);
            }
            //while (s != null) {
            //    if (Match item == re1.Matches(s))
            //        { 
            //        Arr.Add(item.ToString());
            //        s = s.Replace(item.ToString(), "");
            //        MessageBox.Show(s);
            //    }
            //}
            //MessageBox.Show(s);
            //MessageBox.Show(strAry[1].ToString());
        }

        private void LeftOfbtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "LeftOf(,)";
                selTB.Focus();
            }
        }

        private void RightOfbtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "RightOf(,)";
                selTB.Focus();
            }
        }

        private void AboveOfbtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "AboveOf(,)";
                selTB.Focus();
            }
        }

        private void BelowOfbtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "BelowOf(,)";
                selTB.Focus();
            }
        }

        private void Smallbtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "Small()";
                selTB.Focus();
            }
        }

        private void Mediumbtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "Medium()";
                selTB.Focus();
            }
        }

        private void Largebtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "Large()";
                selTB.Focus();
            }
        }

        private void Squarebtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "Square()";
                selTB.Focus();
            }
        }

        private void Circlebtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "Circle()";
                selTB.Focus();
            }
        }

        private void Trianglebtn_Click(object sender, EventArgs e)
        {
            if (selTB != null)
            {
                selTB.SelectedText += "Triangle()";
                selTB.Focus();
            }
        }
    }
}
