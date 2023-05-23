using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    public abstract class Shape
    {
        // Метод для отрисовки примитива на полотне
        public abstract void Draw(Graphics g);

        // Метод для сериализации примитива в JSON
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        // Метод для десериализации примитива из JSON
        public static Shape FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Shape>(json);
        }
    }

    // Класс для линии
    public class Line : Shape
    {
        // Координаты начала и конца линии
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        // Конструктор с параметрами
        public Line(int x1, int y1, int x2, int y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        // Переопределение метода Draw для линии
        public override void Draw(Graphics g)
        {
            g.DrawLine(Pens.Black, X1, Y1, X2, Y2);
        }
    }

    // Класс для окружности
    public class Circle : Shape
    {
        // Координаты центра и радиус окружности
        public int X { get; set; }
        public int Y { get; set; }
        public int R { get; set; }

        // Конструктор с параметрами
        public Circle(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }

        // Переопределение метода Draw для окружности
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.Black, X - R, Y - R, 2 * R, 2 * R);
        }
    }

    // Класс для прямоугольника
    public class Rectangle : Shape
    {
        // Координаты верхнего левого угла и размеры прямоугольника
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // Конструктор с параметрами
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // Переопределение метода Draw для прямоугольника
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, X, Y, Width, Height);
        }
    }

    // Класс для рисунка из нескольких примитивов
    public class Drawing
    {
        // Список примитивов в рисунке
        private List<Shape> shapes;

        // Конструктор без параметров
        public Drawing()
        {
            shapes = new List<Shape>();
        }

        // Метод для добавления примитива в рисунок
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        // Метод для отрисовки рисунка на полотне
        public void Draw(Graphics g)
        {
            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }
        }

        // Метод для сериализации рисунка в JSON файл
        public void SaveToFile(string fileName)
        {
            using (var writer = new StreamWriter(fileName))
            {
                foreach (var shape in shapes)
                {
                    writer.WriteLine(shape.ToJson());
                }
            }
        }

        // Метод для десериализации рисунка из JSON файла
        public void LoadFromFile(string fileName)
        {
            shapes.Clear();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    var json = reader.ReadLine();
                    var shape = Shape.FromJson(json);
                    shapes.Add(shape);
                }
            }
        }
    }
}
