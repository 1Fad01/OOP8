using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab8
{
    public partial class Form1 : Form
    {
        private Drawing drawing = new Drawing();
        public Form1()
        {
            InitializeComponent();
        }

        // Обработчик события Paint для полотна 
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            drawing.Draw(e.Graphics);
        }

        // Обработчик события Click для кнопки "Линия" 
        private void LineButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var x1 = random.Next(0, canvas.Width);
            var y1 = random.Next(0, canvas.Height);
            var x2 = random.Next(0, canvas.Width);
            var y2 = random.Next(0, canvas.Height);
            var line = new Line(x1, y1, x2, y2);
            drawing.AddShape(line);
            drawing.Draw(canvas);
            canvas.Invalidate();
        }

        // Обработчик события Click для кнопки "Окружность" 
        private void CircleButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var x = random.Next(0, canvas.Width);
            var y = random.Next(0, canvas.Height);
            var r = random.Next(10, 50);
            var circle = new Circle(x, y, r);
            drawing.AddShape(circle);
            canvas.Invalidate();
        }

        // Обработчик события Click для кнопки "Прямоугольник" 
        private void RectangleButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var x = random.Next(0, canvas.Width);
            var y = random.Next(0, canvas.Height);
            var width = random.Next(10, 100);
            var height = random.Next(10, 100);
            var rectangle = new Rectangle(x, y, width, height);
            drawing.AddShape(rectangle);
            canvas.Invalidate();
        }

        // Обработчик события Click для кнопки "Сохранить"  
        private void SaveButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "JSON files|*.json";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                drawing.SaveToFile(dialog.FileName);
            }
        }

        // Обработчик события Click для кнопки "Загрузить" 
        private void LoadButton_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "JSON files|*.json";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                drawing.LoadFromFile(dialog.FileName);
                canvas.Invalidate();
            }
        }
    }
}
