using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        // Зберігаємо об'єкти на рівні класу, щоб пам'ятати їх стан
        Rectangle rect1;
        Rectangle rect2;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private double ReadVal(TextBox tb)
        {
            if (double.TryParse(tb.Text, out double result))
                return result;
            return 0.0;
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Створюємо об'єкти (зчитуємо дані з полів)
                rect1 = new Rectangle(
                    (int)ReadVal(txtX1), (int)ReadVal(txtY1),
                    (int)ReadVal(txtW1), (int)ReadVal(txtH1)
                );

                rect2 = new Rectangle(
                    (int)ReadVal(txtX2), (int)ReadVal(txtY2),
                    (int)ReadVal(txtW2), (int)ReadVal(txtH2)
                );

                // 2. Виводимо Прямокутник 1
                lblResultA.Text = $"Прямокутник 1:\n{rect1}";

                // 3. Обчислюємо та виводимо Прямокутник 2
                lblResultB.Text = $"Прямокутник 2:\n{rect2}";

                // 4. Найменший описуючий(Union)
                Rectangle union = Rectangle.GetBoundingBox(rect1, rect2);
                lblMin.Text = $"Найменший описуючий:\n{union}";

                // 5. Обчислюємо та виводимо Перетин окремо в lblInt
                Rectangle intersect = Rectangle.GetIntersection(rect1, rect2);

                if (intersect != null)
                {
                    lblInt.Text = $"Перетин:\n{intersect}";
                    // Щоб виділити успішний перетин
                    lblInt.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblInt.Text = "Перетин: Немає спільних точок";
                    lblInt.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка даних: " + ex.Message);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            // 1. Очищення полів вводу для першого прямокутника
            txtX1.Text = "";
            txtY1.Text = "";
            txtW1.Text = "";
            txtH1.Text = "";

            // 2. Очищення полів вводу для другого прямокутника
            txtX2.Text = "";
            txtY2.Text = "";
            txtW2.Text = "";
            txtH2.Text = "";

            // 3. Очищення полів вводу обох прямокутників (для переміщення)
            txtMX1.Text = "";
            txtMY1.Text = "";
            txtMX2.Text = "";
            txtMY2.Text = "";


            // 4. Очищення лейблів з результатами
            lblResultA.Text = "Результат А: -";
            lblResultB.Text = "Результат В: -";
            lblInt.Text = "Перетин: -";
            lblMin.Text = "Найменший описуючий: -";

            // 5. Встановити фокус (курсор) у найперше поле для зручності
            txtX1.Focus();
        }
        private void btnMove_Click_1(object sender, EventArgs e)
        {
            // Перевірка наявності об'єктів
            if (rect1 == null || rect2 == null)
            {
                MessageBox.Show("Спочатку створіть прямокутники!");
                return;
            }

            try
            {
                // Переміщення Прямокутника 1
                double dx1 = ReadVal(txtMX1);
                double dy1 = ReadVal(txtMY1);
                rect1.Move(dx1, dy1);

                // Оновлення полів вводу для 1
                txtX1.Text = rect1.X.ToString();
                txtY1.Text = rect1.Y.ToString();

                // Переміщення Прямокутника 2
                double dx2 = ReadVal(txtMX2);
                double dy2 = ReadVal(txtMY2);
                rect2.Move(dx2, dy2);

                // Оновлення полів вводу для 2
                txtX2.Text = rect2.X.ToString();
                txtY2.Text = rect2.Y.ToString();

                // Label A
                lblResultA.Text = $"Прямокутник 1:\n{rect1}";
                // Label B
                lblResultB.Text = $"Прямокутник 2:\n{rect2}";

                // Найменший описуючий(Union)
                Rectangle union = Rectangle.GetBoundingBox(rect1, rect2);
                lblMin.Text = $"Найменший описуючий:\n{union}";

                // Label Int (Intersection)
                Rectangle intersect = Rectangle.GetIntersection(rect1, rect2);

                if (intersect != null)
                {
                    lblInt.Text = $"Перетин:\n{intersect}";
                    lblInt.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblInt.Text = "Перетин: Немає спільних точок";
                    lblInt.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при переміщенні: " + ex.Message);
            }
        }

    }
}
