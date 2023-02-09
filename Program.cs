using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.Data;

using System.Drawing;

using System.Linq;

using System.Text;

using System.Windows.Forms;

namespace Exam_Project
{

    public partial class AddNewBook : Form
    {

        public AddNewBook()
        {

            InitializeComponent();

        }

        /// <summary>

        /// Добавление нового элемента в коллекцию ComboBox

        /// </summary>

        private void addItemToComboBox(ComboBox tempComboBox, string item)
        {

            if (!tempComboBox.Items.Contains(item))
            {

                tempComboBox.Items.Add(item);

            }

        }

        /// <summary>

        /// Проверка на вхождение элемента в коллекцию ComboBox и возврат введенного в ComboBox текста

        /// </summary>

        private string comboItemChek(ComboBox tempComboBox, string messagePart)
        {

            string str = tempComboBox.Text;

            if (str == "")
            {

                return "-";

            }

            if (!tempComboBox.Items.Contains(str))
            {

                if (!messageComboBox(messagePart, tempComboBox, str))
                {

                    return "";

                }

            }

            return str;

        }

        /// <summary>

        /// Сообщение, выдаваемое, если элемент не содержится в ComboBox

        /// </summary>

        private bool messageComboBox(string messagePart, ComboBox tempComboBox, string item)
        {

            var dialogResult = MessageBox.Show(messagePart + "\nВы уверены, что хотите добавить?", "Добавление книги", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {

                tempComboBox.Items.Add(item);

                return true;

            }

            return false;

        }

        /// <summary>

        /// Проверка на вхождение в веденный текст разделителя

        /// </summary>

        private bool isContainSeparator(string str)
        {

            return str.Contains(separator[0]);

        }

        public bool isAdded = false;

        /// <summary>

        /// Событие по клику на кнопку "Добавить новую книгу"

        /// </summary>

        private void addBookButton_Click(object sender, EventArgs e)
        {

            isAdded = false;

            errorLabel.Text = "";

            string title = titleTextBox.Text;

            if ((title == "") || (yearTextBox.Text == ""))
            {

                errorLabel.Text = "Заполнены не все обязательные поля (*)";

                return;

            }

            int year = 0;

            if (!Int32.TryParse(yearTextBox.Text, out year) || (year > 2011) || (year < 1900))
            {

                errorLabel.Text = "Год издания введен некорректно";

                return;

            }

            string author = comboItemChek(authorComboBox, "Данного автора нет в списке");

            string publisher = comboItemChek(publisherComboBox, "Данного издательства нет в списке");

            string type = comboItemChek(typeComboBox, "Данного жанра нет в списке");

            if ((author == "") || (publisher == "") || (type == ""))
            {

                return;

            }

            string extraInfo = infoTextBox.Text;

            if (extraInfo == "")
            {

                extraInfo = "-";

            }

            if (isContainSeparator(title) || isContainSeparator(author) || isContainSeparator(publisher) || isContainSeparator(type) || isContainSeparator(extraInfo))
            {

                errorLabel.Text = "Одно из полей содержит недопустимый символ - \"" + BookInfo.separator + "\"";

                return;

            }

            BookCatalog.catalog.addNewBook(author, title, year, publisher, type, extraInfo);

            MessageBox.Show("Книга добавлена");

            clearFieldButton_Click(sender, e);

            isAdded = true;

        }

        /// <summary>

        /// Событие по клику на кнопку "Перейти к каталогу"

        /// </summary>

        private void toCatalButton_Click(object sender, EventArgs e)
        {

            BookCatalog.catalogViewForm.Activate();

            clearFieldButton_Click(sender, e);

        }

        /// <summary>

        /// Событие по клику на кнопку "Сброс"

        /// </summary>

        private void clearFieldButton_Click(object sender, EventArgs e)
        {

            authorComboBox.Text = "";

            titleTextBox.Text = "";

            publisherComboBox.Text = "";

            yearTextBox.Text = "";

            typeComboBox.Text = "";

            infoTextBox.Text = "";

        }

        /// <summary>

        /// Событие по активации формы

        /// </summary>

        private void AddNewBook_Enter(object sender, EventArgs e)
        {

            authorComboBox.Text = CatalogView.author;

            titleTextBox.Text = CatalogView.title;

            yearTextBox.Text = CatalogView.year;

            publisherComboBox.Text = CatalogView.publisher;

            typeComboBox.Text = CatalogView.type;

            infoTextBox.Text = CatalogView.extraInfo;

            var list = BookCatalog.catalog.books;

            for (int i = 0; i < list.Count; i++)
            {

                addItemToComboBox(authorComboBox, list[i].author);

                addItemToComboBox(publisherComboBox, list[i].publisher);

                addItemToComboBox(typeComboBox, list[i].type);

            }

        }

    }

}