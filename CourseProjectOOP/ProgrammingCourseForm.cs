using System.ComponentModel;
using System.Text.RegularExpressions;
using CourseProject.Models;

namespace CourseProject
{
    /// <summary>
    /// Класс формы для добавления/изменения записей.
    /// </summary>
    public partial class PerformanceForm : Form
    {
        /// <summary>
        /// Объект, который будет добавлен/изменен.
        /// </summary>
        public ProgrammingCourse ProgrammingCourse { get; set; }

        /// <summary>
        /// Коллекция уже добавленных объектов (для проверки на дублирование).
        /// </summary>
        public BindingList<ProgrammingCourse> List { get; set; }

        /// <summary>
        /// Конструктор формы, который вызывается при изменении записей.
        /// </summary>
        /// <param name="_performance">Объект, который будет изменен.</param>
        /// <param name="_list">Коллекция уже существующих объектов.</param>
        public PerformanceForm(ProgrammingCourse _performance, BindingList<ProgrammingCourse> _list)
        {
            InitializeComponent();
            ProgrammingCourse = _performance;
            List = _list;
            Text = "Изменение записи";
            btnAdd.Text = "Изменить";
            tbNameOfPerformance.Text = ProgrammingCourse.ProgrammingCourseName;
            tbDirectorName.Text = ProgrammingCourse.AuthorName;
            cbGenre.Text = ProgrammingCourse.TypeCourse;
            dtpPremiereDate.Value = ProgrammingCourse.PublishDate;
            dtpDuration.Value = ProgrammingCourse.Duration;
            nudTicketCost.Value = ProgrammingCourse.CourseCost;
        }

        /// <summary>
        /// Конструктор формы, который вызывается при добавлении записей.
        /// </summary>
        /// <param name="_list">Коллекция уже существующих объектов.</param>
        public PerformanceForm(BindingList<ProgrammingCourse> _list)
        {
            InitializeComponent();
            ProgrammingCourse = new ProgrammingCourse();
            List = _list;
            cbGenre.SelectedIndex = 0;
            dtpPremiereDate.Value = DateTime.Now;
            dtpDuration.Value = 10;
            Text = "Добавление записи";
            btnAdd.Text = "Добавить";
        }


        /// <summary>
        /// Метод, обрабатывающий нажатие кнопки добавить/изменить запись.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(tbDirectorName.Text == "" || tbNameOfPerformance.Text == "")
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!Regex.IsMatch(tbDirectorName.Text, @"^[А-ЯЁа-яёa-zA-Z\s]+$"))
            {
                MessageBox.Show("Имя введено некорректно. Пример правильного ввода: Константин Станиславский, Билл Ирвин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtpDuration.Value <= 0)
            {
                MessageBox.Show("Продолжительность не может быть нулевой или отрицательной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (List.Any(item =>
                    item != ProgrammingCourse &&
                    item.ProgrammingCourseName == tbNameOfPerformance.Text &&
                    item.TypeCourse == cbGenre.Text &&
                    item.PublishDate.Date == dtpPremiereDate.Value.Date &&
                    item.CourseCost == nudTicketCost.Value &&
                    item.Duration == dtpDuration.Value))
            {
                MessageBox.Show("Такая запись уже добавлена в базу данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ProgrammingCourse.ProgrammingCourseName = tbNameOfPerformance.Text;
            ProgrammingCourse.AuthorName = tbDirectorName.Text;
            ProgrammingCourse.TypeCourse = cbGenre.Text;
            ProgrammingCourse.PublishDate = dtpPremiereDate.Value;
            ProgrammingCourse.Duration = (int)dtpDuration.Value;
            ProgrammingCourse.CourseCost = nudTicketCost.Value;
            DialogResult = DialogResult.OK;
        }
    }
}
