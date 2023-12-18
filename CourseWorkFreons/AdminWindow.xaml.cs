using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window {
        DatabaseWork _databaseWork;
        bool isFirstEnter;
        public AdminWindow() {
            InitializeComponent();
            FirstEnter();
        }

        private void back_Button_Click(object sender, RoutedEventArgs e) {
            Hide();
            new LoginWindow().Show();
            Close();
        }

        private void SetUpColumnsFinalProduct() {
            var column = new DataGridTextColumn {
                Header = "Номер",
                Binding = new Binding("ID")
            };
            base_DataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Название",
                Binding = new Binding("Name")
            };
            base_DataGrid.Columns.Add(column);

            column = new DataGridTextColumn {
                Header = "Марка",
                Binding = new Binding("Designation")
            };
            base_DataGrid.Columns.Add(column);

            column = new DataGridTextColumn {
                Header = "Область применения",
                Binding = new Binding("Area")
            };
            base_DataGrid.Columns.Add(column);
        }

        private record DataForTable {
            public required string Designation { get; set; }
            public required string Equipment { get; set; }
        }

        private void FirstEnter() {
            isFirstEnter = true;
            _databaseWork = new DatabaseWork();

            tables_ComboBox.Items.Add("Готовая продукция");
            tables_ComboBox.Items.Add("Оборудование");
            tables_ComboBox.Items.Add("Стадия");

            tables_ComboBox.SelectedIndex = 0;

            SetUpColumnsFinalProduct();
            DataTable dt = _databaseWork.GetTableFinalProduct();

            foreach (var row in  dt.Rows) {
                base_DataGrid.Items.Add(row);
            }
        }

        private void tables_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
    }
}
