using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.ComponentModel;
using CourseProject.DataBase;
using CourseProject.Models;

namespace CourseProject
{

    /// <summary>
    /// Делегат для обработки событий изменения в коллекции записей.
    /// </summary>
    /// <param name="p">Объект, который был добавлен/удален.</param>
    public delegate void CollectionChanged(ProgrammingCourse p);
    
    /// <summary>
    /// Класс основной формы приложения.
    /// </summary>
    public partial class MainForm : Form
    {

        /// <summary>
        /// Коллекция, хранящая записи о представлениях.
        /// </summary>
        private BindingList<ProgrammingCourse> programmingCourses;

        /// <summary>
        /// Строка для подключения к БД.
        /// </summary>
        private string connection;

        /// <summary>
        /// Событие удаления записи из коллекции.
        /// </summary>
        public event CollectionChanged ItemRemoved;

        /// <summary>
        /// Событие добавления записи в коллекцию.
        /// </summary>
        public event CollectionChanged ItemAdded;

        /// <summary>
        /// Флаг направления сортировки (в прямом/обратном алфавитном порядке).
        /// </summary>
        private bool reverseSort = false;

        /// <summary>
        /// Конструктор формы.
        /// Настраивает таблицу для отображения записей, инициализириует коллекцию, события
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            programmingCourses = new BindingList<ProgrammingCourse>();
            cbFilter.SelectedIndex = 0;
            connection = "";
            bindingSource.DataSource = programmingCourses;
            dgvMainTable.DataSource = bindingSource;
            dgvMainTable.Columns["ProgrammingCourseName"].HeaderText = "Название курса";
            dgvMainTable.Columns["AuthorName"].HeaderText = "Имя автора";
            dgvMainTable.Columns["TypeCourse"].HeaderText = "Тип";
            dgvMainTable.Columns["PublishDate"].HeaderText = "Дата создания";
            dgvMainTable.Columns["Duration"].HeaderText = "Продолжительность";
            dgvMainTable.Columns["CourseCost"].HeaderText = "Стоимость курса";
            dgvMainTable.Columns["Id"].Visible = false;
            dgvMainTable.Columns["PublishDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dgvMainTable.Columns["CourseCost"].DefaultCellStyle.Format = "F2";
            dgvMainTable.RowHeadersWidth = 25;
            ItemAdded += (p => { });
            ItemRemoved += (p => { });
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для создания новой БД.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text = "ИС «Курсы по программированию»";
            connection = "";
            bindingSource.DataSource = programmingCourses;
            programmingCourses.Clear();
            labelCount.Text = "Отображено 0 из 0 записей";
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для загрузки созданной ранее БД.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "SQLite Database Files (*.db)|*.db",
                Title = "Выберите файл базы данных",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindingSource.DataSource = programmingCourses;
                connection = $"Data Source={openFileDialog.FileName}";
                Text = $"ИС «Курсы по программированию» - {connection.Replace("Data Source=", "")}";
                using (DatabaseContext context = new DatabaseContext(connection))
                {
                    programmingCourses.Clear();
                    foreach (ProgrammingCourse p in context.ProgrammingCourses.ToList())
                    {
                        programmingCourses.Add(p);
                    }
                }
                labelCount.Text = $"Отображено {programmingCourses.Count} из {programmingCourses.Count} записей";
            }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для сохранения записей в файл БД.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connection == "")
            {
                SaveDatabaseAs();
            }
            else
            {
                using (DatabaseContext context = new DatabaseContext(connection))
                {
                    context.RemoveRange(context.ProgrammingCourses);
                    context.SaveChanges();
                    context.AddRange(programmingCourses);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для сохранения записей в новый файл БД.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDatabaseAs();
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для добавления новой записи.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void AddRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PerformanceForm performanceForm = new PerformanceForm(programmingCourses))
            {
                if (performanceForm.ShowDialog() == DialogResult.OK)
                {
                    ProgrammingCourse newPerformance = new ProgrammingCourse
                    {
                        ProgrammingCourseName = performanceForm.ProgrammingCourse.ProgrammingCourseName,
                        AuthorName = performanceForm.ProgrammingCourse.AuthorName,
                        TypeCourse = performanceForm.ProgrammingCourse.TypeCourse,
                        PublishDate = performanceForm.ProgrammingCourse.PublishDate,
                        Duration = performanceForm.ProgrammingCourse.Duration,
                        CourseCost = performanceForm.ProgrammingCourse.CourseCost
                    };
                    programmingCourses.Add(newPerformance);
                    ItemAdded.Invoke(newPerformance);
                    dgvMainTable.Refresh();
                    labelCount.Text = $"Отображено {(bindingSource.DataSource as BindingList<ProgrammingCourse>).Count} из {programmingCourses.Count} записей";
                }
            }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для изменения существующей записи.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void ChangeRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMainTable.SelectedRows.Count == 1)
            {
                ProgrammingCourse programmingCourse = bindingSource.Current as ProgrammingCourse;
                using (PerformanceForm performanceForm = new PerformanceForm(programmingCourse, programmingCourses))
                {
                    if (performanceForm.ShowDialog() == DialogResult.OK)
                    {
                        programmingCourse.ProgrammingCourseName = performanceForm.ProgrammingCourse.ProgrammingCourseName;
                        programmingCourse.AuthorName = performanceForm.ProgrammingCourse.AuthorName;
                        programmingCourse.TypeCourse = performanceForm.ProgrammingCourse.TypeCourse;
                        programmingCourse.PublishDate = performanceForm.ProgrammingCourse.PublishDate;
                        programmingCourse.Duration = performanceForm.ProgrammingCourse.Duration;
                        programmingCourse.CourseCost = performanceForm.ProgrammingCourse.CourseCost;
                        dgvMainTable.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выделите одну запись для изменения", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для удаления существующей записи.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void RemoveRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMainTable.SelectedRows.Count != 1)
            {
                MessageBox.Show("Сначала выберите запись", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ProgrammingCourse programmingCourse = bindingSource.Current as ProgrammingCourse;
            ItemRemoved.Invoke(programmingCourse);
            programmingCourses.Remove(programmingCourse);
            labelCount.Text = $"Отображено {(bindingSource.DataSource as BindingList<ProgrammingCourse>).Count} из {programmingCourses.Count} записей";
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для удаления всех записей.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (programmingCourses.Count == 0)
            {
                MessageBox.Show("База данных пуста", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bindingSource.DataSource = programmingCourses;
            programmingCourses.Clear();
            labelCount.Text = "Отображено 0 из 0 записей";
        }

        /// <summary>
        /// Метод, сохраняющий записи в новый файл БД.
        /// </summary>
        private void SaveDatabaseAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "SQLite Database Files (*.db)|*.db",
                Title = "Сохранить как",
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                connection = $"Data Source={saveFileDialog.FileName}";
                Text = $"ИС «Курсы по программированию» - {connection.Replace("Data Source=", "")}";
                using (DatabaseContext context = new DatabaseContext(connection))
                {
                    context.Database.EnsureCreated();
                    context.ProgrammingCourses.AddRange(programmingCourses);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Метод, обрабатывающий изменение текста для фильтрации записей.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void tbFilterValue_TextChanged(object sender, EventArgs e)
        {
            if (tbFilterValue.Text == "")
            {
                labelCount.Text = $"Отображено {programmingCourses.Count} из {programmingCourses.Count} записей";
                bindingSource.DataSource = programmingCourses;
                return;
            }
            dgvMainTable.Refresh();
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    Filter(p => p.ProgrammingCourseName);
                    break;
                case 1:
                    Filter(p => p.AuthorName);
                    break;
                case 2:
                    Filter(p => p.TypeCourse);
                    break;
                case 3:
                    Filter(p => p.PublishDate.ToString());
                    break;
                case 4:
                    Filter(p => p.Duration.ToString());
                    break;
                case 5:
                    Filter(p => p.CourseCost.ToString());
                    break;
                default:
                    bindingSource.DataSource = programmingCourses;
                    break;
            }
        }

        /// <summary>
        /// Метод, фильтрующий записи по заданному критерию.
        /// </summary>
        /// <param name="selector">Делегат, возвращающий строковое значение из объекта ProgrammingCourse</param>
        private void Filter(Func<ProgrammingCourse, string> selector)
        {
            ItemAdded -= (p => { });
            ItemRemoved -= (p => { });
            BindingList<ProgrammingCourse> tmp = new BindingList<ProgrammingCourse>(
                programmingCourses
                .Where(s => selector(s)
                    .ToLower()
                    .Trim()
                    .Contains(tbFilterValue.Text.ToLower().Trim())
                ).
                ToList());
            ItemRemoved += (p => tmp.Remove(p));
            ItemAdded += (p =>
            {
                if (selector(p).ToLower().Contains(tbFilterValue.Text.ToLower()))
                {
                    tmp.Add(p);
                }
            });
            bindingSource.DataSource = tmp;
            labelCount.Text = $"Отображено {tmp.Count} из {programmingCourses.Count} записей";
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для сортировки записей.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void btnSort_Click(object sender, EventArgs e)
        {
            if (programmingCourses.Count == 0)
            {
                return;
            }
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    SortList(p => p.ProgrammingCourseName);
                    break;
                case 1:
                    SortList(p => p.AuthorName);
                    break;
                case 2:
                    SortList(p => p.TypeCourse);
                    break;
                case 3:
                    SortList(p => p.PublishDate);
                    break;
                case 4:
                    SortList(p => p.Duration);
                    break;
                case 5:
                    SortList(p => p.CourseCost);
                    break;
                default:
                    bindingSource.DataSource = programmingCourses;
                    break;
            }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки для сохранения таблицы с записями в PDF-файл.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void CreatePdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF файлы (*.pdf)|*.pdf",
                Title = "Сохранить отчет по текущим данным",
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                Table table = new Table(dgvMainTable.Columns.Count - 1);

                PdfFont font = PdfFontFactory.CreateFont(@"C:\Windows\Fonts\arial.ttf", PdfEncodings.IDENTITY_H);
                Paragraph header = new Paragraph("Список представлений")
                    .SetFont(font)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(16);
                foreach (DataGridViewColumn i in dgvMainTable.Columns)
                {
                    if (i.Visible && i.Index != 0)
                    {
                        table.AddCell(new Paragraph(i.HeaderText).SetFont(font));
                    }
                }
                foreach (DataGridViewRow i in dgvMainTable.Rows)
                {
                    foreach (DataGridViewCell j in i.Cells)
                    {
                        if (dgvMainTable.Columns[j.ColumnIndex].Visible && j.ColumnIndex != 0)
                        {
                            if (j.Value is DateTime date)
                            {
                                table.AddCell(new Paragraph(date.ToString("dd.MM.yyyy")).SetFont(font));
                            }
                            else
                            {
                                table.AddCell(new Paragraph(j.Value.ToString()).SetFont(font));
                            }

                        }

                    }
                }
                using (PdfWriter pdfWriter = new PdfWriter(saveFileDialog.FileName))
                using (PdfDocument pdf = new PdfDocument(pdfWriter))
                {
                    Document document = new Document(pdf);
                    document.Add(header);
                    document.Add(table);
                    document.Close();
                }
            }
        }

        /// <summary>
        /// Метод, обрабатывающий изменение текста для поиска записей.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dgvMainTable.Rows)
            {
                i.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                i.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            }
            if (tbSearch.Text == "")
            {
                return;
            }
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    Search(p => p.ProgrammingCourseName);
                    break;
                case 1:
                    Search(p => p.AuthorName);
                    break;
                case 2:
                    Search(p => p.TypeCourse);
                    break;
                case 3:
                    Search(p => p.PublishDate.ToString());
                    break;
                case 4:
                    Search(p => p.Duration.ToString());
                    break;
                case 5:
                    Search(p => p.CourseCost.ToString());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Метод, сортирующий записи по алфавиту/возрастанию (убыванию) (в зависимости от критерия).
        /// </summary>
        /// <typeparam name="T">Тип значения, по которому производится сортировка.</typeparam>
        /// <param name="selector">Делегат, определяющий поле объекта, по которому производится сортировка.</param>
        private void SortList<T>(Func<ProgrammingCourse, T> selector)
        {
            ItemAdded -= (p => { });
            ItemRemoved -= (p => { });
            BindingList<ProgrammingCourse> tmp = bindingSource.DataSource as BindingList<ProgrammingCourse>;
            tmp = reverseSort
                ? new BindingList<ProgrammingCourse>(tmp.OrderByDescending(selector).ToList())
                : new BindingList<ProgrammingCourse>(tmp.OrderBy(selector).ToList());
            ItemRemoved += (p => tmp.Remove(p));
            ItemAdded += (p => tmp.Add(p));
            bindingSource.DataSource = tmp;
            reverseSort = !reverseSort;
        }

        /// <summary>
        /// Метод, выполняющий поиск записей по критерию.
        /// </summary>
        /// <param name="selector">Делегат, возвращающий значение, по которому производится поиск.</param>
        private void Search(Func<ProgrammingCourse, string> selector)
        {
            foreach(DataGridViewRow i in dgvMainTable.Rows)
            {
                ProgrammingCourse item = i.DataBoundItem as ProgrammingCourse;
                if (selector(item).ToLower().Trim().Contains(tbSearch.Text.Trim().ToLower()))
                {
                    i.DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        /// <summary>
        /// Метод, обрабатывающий сброс выбора строки в таблице по клику на любое место формы, кроме самой таблицы.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (!dgvMainTable.Bounds.Contains(PointToClient(Cursor.Position)))
            {
                dgvMainTable.ClearSelection();
            }
        }
    }
}
    
