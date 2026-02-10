using System;

namespace RectangleApp
{
    public class Rectangle // Оголошення публічного класу Rectangle. "public" означає, що клас доступний з інших частин програми.
    {

        public double X { get; set; } // Координата X верхнього лівого кута прямокутника. { get; set; } дозволяє читати та змінювати це значення.
        public double Y { get; set; } // Координата Y верхнього лівого кута прямокутника.
        public double Width { get; set; } // Ширина прямокутника.
        public double Height { get; set; } // Висота прямокутника.
        public double Right => X + Width; // Властивість, що повертає координату правого краю (X + Ширина).
        public double Bottom => Y + Height; // Властивість, що повертає координату нижнього краю (Y + Висота).

        public Rectangle(double x, double y, double width, double height) // Приймає початкові координати (x, y) та розміри (width, height).
        {
            if (width < 0 || height < 0) // Перевірка: якщо ширина менша за 0 АБО висота менша за 0.
            {
                throw new ArgumentException("Ширина та висота мають бути невід'ємними.");
            }

            X = x; // Присвоюємо передане значення x властивості X об'єкта і тд.
            Y = y;
            Width = width;
            Height = height;
        }

        public void Move(double dx, double dy) // dx - зміщення по осі X, dy - зміщення по осі Y.
        {
            X += dx; // Додаємо зміщення dx до поточної координати X.
            Y += dy; // Додаємо зміщення dy до поточної координати Y.
        }

        // Метод для зміни розмірів прямокутника.
        public void Resize(double newWidth, double newHeight)
        {
            if (newWidth < 0 || newHeight < 0)             // Не можуть бути від'ємними.
            {
                Console.WriteLine("Помилка: Розміри не можуть бути від'ємними.");
                return; // Перериваємо виконання методу.
            }
            Width = newWidth; // Встановлюємо нову ширину.
            Height = newHeight; // Встановлюємо нову висоту.
        }

        // Метод побудови найменшого прямокутника, що містить два задані (r1 і r2).
        public static Rectangle GetBoundingBox(Rectangle r1, Rectangle r2)
        {
            double minX = Math.Min(r1.X, r2.X); // Знаходимо мінімальну координату X серед двох прямокутників (ліва межа нового прямокутника).
            double minY = Math.Min(r1.Y, r2.Y); // Знаходимо мінімальну координату Y (верхня межа нового прямокутника).
            double maxRight = Math.Max(r1.Right, r2.Right); // Знаходимо максимальну праву межу серед двох прямокутників.
            double maxBottom = Math.Max(r1.Bottom, r2.Bottom); // Знаходимо максимальну нижню межу серед двох прямокутників.
            double newWidth = maxRight - minX; // Обчислюємо ширину нового прямокутника як різницю між правим і лівим краєм.
            double newHeight = maxBottom - minY; // Обчислюємо висоту нового прямокутника як різницю між нижнім і верхнім краєм.

            return new Rectangle(minX, minY, newWidth, newHeight); // Створюємо і повертаємо новий об'єкт Rectangle з обчисленими параметрами.
        }

        // Метод знаходження спільної частини (перетину) двох прямокутників.
        public static Rectangle GetIntersection(Rectangle r1, Rectangle r2)
        {
            double intersectX = Math.Max(r1.X, r2.X); // Ліва межа перетину - це найбільше значення з лівих координат.
            double intersectY = Math.Max(r1.Y, r2.Y); // Верхня межа перетину - це найбільше значення з верхніх координат.
            double intersectRight = Math.Min(r1.Right, r2.Right); // Права межа перетину - це найменше значення з правих меж.
            double intersectBottom = Math.Min(r1.Bottom, r2.Bottom); // Нижня межа перетину - це найменше значення з нижніх меж.
            double newWidth = intersectRight - intersectX; // Обчислюємо ширину перетину (права межа мінус ліва).
            double newHeight = intersectBottom - intersectY; // Обчислюємо висоту перетину (нижня межа мінус верхня).

            if (newWidth <= 0 || newHeight <= 0)             // якщо ширина або висота менші або дорівнюють 0, то геометричного перетину немає.
            {
                return null; // Повертаємо null, щоб показати відсутність перетину.
            }

            return new Rectangle(intersectX, intersectY, newWidth, newHeight); // Якщо перетин є, створюємо і повертаємо новий прямокутник.
        }
        public override string ToString()
        {
            return $"[X: {X}, Y: {Y}, Ширина: {Width}, Висота: {Height}]"; // Повертаємо відформатований рядок із даними прямокутника.
        }
    }
}