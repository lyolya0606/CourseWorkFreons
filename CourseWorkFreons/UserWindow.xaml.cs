using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWorkFreons {
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window {

        DatabaseWork _databaseWork;
        bool isFirstEnter;
        public UserWindow() {
            InitializeComponent();
            FirstEntering();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Hide();
            new LoginWindow().Show();
            Close();
        }

        private void FirstEntering() {
            isFirstEnter = true;
            _databaseWork = new DatabaseWork();
            List<string> marks = _databaseWork.GetMarks();

            foreach (string mark in marks) {
                marks_ComboBox.Items.Add(mark);
            }

            marks_ComboBox.SelectedItem = marks[0];

            string name = _databaseWork.GetNameFreon(marks[0]);
            name_label.Content = name;

            string area = _databaseWork.GetArea(marks[0]);
            area_label.Text = area;

            string scheme = _databaseWork.GetSchemeFreon(marks[0]);
            scheme_image.Source = new BitmapImage(new Uri(scheme, UriKind.Relative));

            List<Tuple<string, string>> equipment = _databaseWork.GetEquipment(marks[0]);
            FillTable(equipment);
        }

        private void SetUpColumns() {
            var column = new DataGridTextColumn {
                Header = "Обозначение",
                Binding = new Binding("Designation")
            };
            designation_DataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Оборудование",
                Binding = new Binding("Equipment")
            };
            designation_DataGrid.Columns.Add(column);
        }

        private record DataForTable {
            public required string Designation { get; set; }
            public required string Equipment { get; set; }
        }

        private void marks_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (isFirstEnter) {
                isFirstEnter = false;
                return;
            }
            //designation_DataGrid.Items.Clear();
            designation_DataGrid.ItemsSource = null;
            designation_DataGrid.Columns.Clear();
            //designation_DataGrid.Items.Refresh();

            string mark = (string)marks_ComboBox.SelectedItem;

            string name = _databaseWork.GetNameFreon(mark);
            name_label.Content = name;

            string area = _databaseWork.GetArea(mark);
            area_label.Text = area;

            string scheme = _databaseWork.GetSchemeFreon(mark);
            scheme_image.Source = new BitmapImage(new Uri(scheme, UriKind.Relative));

            List<Tuple<string, string>> equipment = _databaseWork.GetEquipment(mark);
            FillTable(equipment);
        }

        private void FillTable(List<Tuple<string, string>> designations) {
            SetUpColumns();
            List<DataForTable> data = new();
            for (int i = 0; i < designations.Count; i++) {
                data.Add(new DataForTable {
                    Designation = designations[i].Item2,
                    Equipment = designations[i].Item1 
                });
            }

            designation_DataGrid.ItemsSource = data;
        }
    }
}
