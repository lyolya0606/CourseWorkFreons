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
            DatabaseWork databaseWork = new DatabaseWork();
            List<string> marks = databaseWork.GetMarks();

            foreach (string mark in marks) {
                marks_ComboBox.Items.Add(mark);
            }

            marks_ComboBox.SelectedItem = marks[0];

            string name = databaseWork.GetNameFreon(marks[0]);
            name_label.Content = name;

            string area = databaseWork.GetArea(marks[0]);
            area_label.Text = area;

            string scheme = databaseWork.GetSchemeFreon(marks[0]);
            scheme_image.Source = new BitmapImage(new Uri(scheme, UriKind.Relative));

            List<Tuple<string, string>> equipment = databaseWork.GetEquipment(marks[0]);
            FillTable(equipment);

            //List<Tuple<string, string>> tuples = new List<Tuple<string, string>>();
            //tuples.Add(new Tuple<string, string>("1", "jds"));
            //tuples.Add(new Tuple<string, string>("2", "jdddddds"));
            //tuples.Add(new Tuple<string, string>("3", "s"));

            //FillTable(tuples);
        }

        private void SetUpColumns() {
            var column = new DataGridTextColumn {
                Header = "Номер",
                Binding = new Binding("Number")
            };
            designation_DataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
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
            public int Number { get; set; }
            public required string Designation { get; set; }
            public required string Equipment { get; set; }
        }

        private void FillTable(List<Tuple<string, string>> designations) {
            SetUpColumns();
            List<DataForTable> data = new();
            for (int i = 0; i < designations.Count; i++) {
                data.Add(new DataForTable {
                    Number = i + 1,
                    Designation = designations[i].Item2,
                    Equipment = designations[i].Item1 
                });
            }

            designation_DataGrid.ItemsSource = data;
        }
    }
}
