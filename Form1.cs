using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        interface ILibrary
        {
            string Name { set; get; }
            string Family { set; get; }
            string Address { set; get; }
            string Phone { set; get; }
        }
        class Library : ILibrary, IComparable<Library>, ICloneable
        {
            string name;
            string family;
            public int[] birthday = new int[3];
            string address;
            string phone;
            public string Name
            {
                set { name = value; }
                get { return name; }
            }
            public string Family
            {
                set { family = value; }
                get { return family; }
            }
            public string Address
            {
                set { address = value; }
                get { return address; }
            }
            public string Phone
            {
                set { phone = value; }
                get { return phone; }
            }
            public Library(string name, string family, int d, int m, int y, string address, string phone)
            {
                Name = name;
                Family = family;
                this.birthday[0] = d;
                this.birthday[1] = m;
                this.birthday[2] = y;
                Address = address;
                Phone = phone;
            }
            public override string ToString()
            {
                return string.Format("Имя: {0}{1}Фамилия: {2}{3}Дата рождения: {4}.{5}.{6}{7}Город: {8}{9}Телефон: {10}{11}",
                    Name, Environment.NewLine, Family, Environment.NewLine, birthday[0], birthday[1], birthday[2], Environment.NewLine, Address, Environment.NewLine, Phone, Environment.NewLine);
            }
            public int CompareTo(Library other)
            {
                if (this.birthday[2] > other.birthday[2]) return 1;
                if (this.birthday[2] < other.birthday[2]) return -1;
                return 0;
            }
            public object Clone()
            {
                return new Library(this.name, this.family, this.birthday[0], this.birthday[1], this.birthday[2], address, phone);
            }
        }
        class Sort : IComparer<Library>
        {
            public int Compare(Library x, Library y)
            {
                if (x.birthday[2] > y.birthday[2]) return 1;
                if (x.birthday[2] < y.birthday[2]) return -1;
                return 0;
            }
        }
        private List<Library> libraryList = new List<Library>();
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string family = textBox2.Text;
            int d = dateTimePicker1.Value.Day;
            int m = dateTimePicker1.Value.Month;
            int y = dateTimePicker1.Value.Year;
            string address = textBox3.Text;
            string phone = textBox4.Text;

            Library newLibrary = new Library(name, family, d, m, y, address, phone);
            libraryList.Add(newLibrary);
            RefreshTextBox5();
        }
        private void RefreshTextBox5()
        {
            textBox5.Text = "";
            int count = 1;
            foreach (Library lib in libraryList)
            {
                textBox5.Text += $"#{count}{Environment.NewLine}" + lib + Environment.NewLine;
                count++;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textBox6.Text) - 1;
            int y = int.Parse(textBox7.Text) - 1;

            if (x < libraryList.Count && y < libraryList.Count)
            {
                Sort birthdaySorter = new Sort();
                int comparisonResult = birthdaySorter.Compare(libraryList[x], libraryList[y]);
                label8.Text = comparisonResult.ToString();
            }
            else
            {
                MessageBox.Show("Неверный номер записи.");
            }
        }
        private void RefreshTextBox9()
        {
            textBox9.Text = "";
            for (int i = 0; i < libraryList.Count; i++)
            {
                textBox9.Text += $"#{i + 1}{Environment.NewLine}" + libraryList[i] + Environment.NewLine;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            libraryList.Sort();
            RefreshTextBox9();
        }      
        private void button4_Click(object sender, EventArgs e)
        {
            int index = int.Parse(textBox8.Text) - 1;
            if (index < libraryList.Count)
            {
                Library clonedLibrary = (Library)libraryList[index].Clone();
                textBox10.Text = clonedLibrary.ToString();
            }
            else
            {
                MessageBox.Show("Неверный номер записи.");
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
