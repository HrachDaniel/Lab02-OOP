using System;

namespace WinFormsApp1
{
    // Клас, що описує сутність "Прямокутник"
    public class Rectangle
    {
        // --- Властивості (Properties) ---
        public int X { get; set; }      // Координата X верхнього лівого кута
        public int Y { get; set; }      // Координата Y верхнього лівого кута
        public int Width { get; set; }  // Ширина прямокутника
        public int Height { get; set; } // Висота прямокутника

        // --- Конструктор (Constructor) ---
        // Створює новий екземпляр класу з початковими значеннями
        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        // --- Метод переміщення (Move) ---
        // Змінює координати X та Y на задані величини dx та dy.
        // Приймає double, але перетворює їх в int (явне приведення типів), бо властивості класу цілочисельні.
        public void Move(double dx, double dy)
        {
            X += (int)dx;
            Y += (int)dy;
        }

        // --- Перевизначення методу ToString ---
        // Потрібно для зручного виводу об'єкта у текстовому вигляді (наприклад, у Label)
        public override string ToString()
        {
            return $"X={X}, Y={Y}, W={Width}, H={Height}";
        }

        // --- Статичний метод: Описуючий прямокутник (Bounding Box) ---
        // Повертає найменший прямокутник, який містить всередині себе r1 та r2
        public static Rectangle GetBoundingBox(Rectangle r1, Rectangle r2)
        {
            // Знаходимо мінімальні ліві та верхні межі (початок нового прямокутника)
            int minX = Math.Min(r1.X, r2.X);
            int minY = Math.Min(r1.Y, r2.Y);

            // Знаходимо максимальні праві та нижні межі (кінець нового прямокутника)
            // r.X + r.Width = координата правого краю
            int maxX = Math.Max(r1.X + r1.Width, r2.X + r2.Width);
            int maxY = Math.Max(r1.Y + r1.Height, r2.Y + r2.Height);

            // Створюємо новий прямокутник.
            // Ширина = права межа мінус ліва межа.
            // Висота = нижня межа мінус верхня межа.
            return new Rectangle(
                minX,
                minY,
                maxX - minX,
                maxY - minY
            );
        }

        // --- Статичний метод: Перетин (Intersection) ---
        public static Rectangle GetIntersection(Rectangle r1, Rectangle r2)
        {
            // Початок перетину: максимум з лівих меж та максимум з верхніх меж
            // (тобто найправіший початок і найнижчий верх)
            int x1 = Math.Max(r1.X, r2.X);
            int y1 = Math.Max(r1.Y, r2.Y);

            // Кінець перетину: мінімум з правих меж та мінімум з нижніх меж
            // (тобто найлівіший кінець і найвищий низ)
            int x2 = Math.Min(r1.X + r1.Width, r2.X + r2.Width);
            int y2 = Math.Min(r1.Y + r1.Height, r2.Y + r2.Height);

            // Перевірка: якщо "кінець" правіше "початку" і нижче "верху" — перетин існує
            if (x2 > x1 && y2 > y1)
            {
                // Повертаємо прямокутник перетину
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            else
            {
                // Якщо умова не виконується — прямокутники не перетинаються
                return null;
            }
        }
    }
}