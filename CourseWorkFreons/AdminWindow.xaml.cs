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

        private record DataForFinalProduct {
            public required string ID { get; set; }
            public required string Name { get; set; }
            public required string Designation { get; set; }
            public required string Area { get; set; }
        }

        private void SetUpColumnsEquipment() {
            var column = new DataGridTextColumn {
                Header = "Номер",
                Binding = new Binding("ID")
            };
            base_DataGrid.Columns.Add(column);
            column = new DataGridTextColumn {
                Header = "Оборудование",
                Binding = new Binding("Name")
            };
            base_DataGrid.Columns.Add(column);

            column = new DataGridTextColumn {
                Header = "Обозначение",
                Binding = new Binding("Designation")
            };
            base_DataGrid.Columns.Add(column);
        }

        private record DataForEquipment {
            public required string ID { get; set; }
            public required string Name { get; set; }
            public required string Designation { get; set; }
        }

        private void SetUpColumnsStage() {
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
        }

        private record DataForStage {
            public required string ID { get; set; }
            public required string Name { get; set; }
        }

        private void FirstEnter() {
            isFirstEnter = true;
            _databaseWork = new DatabaseWork();

            tables_ComboBox.Items.Add("Готовая продукция");
            tables_ComboBox.Items.Add("Оборудование");
            tables_ComboBox.Items.Add("Стадия");

            tables_ComboBox.SelectedIndex = 0;

            //SetUpColumnsFinalProduct();
            List<List<string>> dt = _databaseWork.GetTableFinalProduct();

            FillFinalProduct(dt);



        }

        private void FillFinalProduct(List<List<string>> dt) {
            SetUpColumnsFinalProduct();
            List<DataForFinalProduct> data = new();
            for (int i = 0; i < dt.Count; i++) {
                data.Add(new DataForFinalProduct {
                    ID = dt[i][0],
                    Name = dt[i][1],
                    Designation = dt[i][2],
                    Area = dt[i][3]
                }); 
            }

            base_DataGrid.ItemsSource = data;
        }

        private void base_DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) {

            //var selectedCell = e.AddedCells[0];
            //var selectedRow = base_DataGrid.SelectedItem as DataRowView;

            //var firstCellValue = selectedRow.Row.ItemArray[0];
            //string value = firstCellValue.ToString();
            //int selectedRow = base_DataGrid.SelectedIndex;
            //var firstCellValue = selectedRow.Row.ItemArray[0].ToString();
            //string x = base_DataGrid.ItemsSource[(IEnumerable<selectedRow>)];
            //string id = (base_DataGrid.ItemsSource[selectedRow].Row[0].ToString();
            //add_Button.Content = value;
        }
        string? currentID;
        string? currentName;
        string? currentDesignation;
        string? currentArea;
        string currentTable;

        private void base_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            edit_Button.IsEnabled = true;
            int selectedRow = base_DataGrid.SelectedIndex;

            string table = (string)tables_ComboBox.SelectedItem;

            if (table == "Готовая продукция") {
                TextBlock? id = base_DataGrid.Columns[0].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentID = id?.Text;

                TextBlock? name = base_DataGrid.Columns[1].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentName = name?.Text;
               
                
                TextBlock? designation = base_DataGrid.Columns[2].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentDesignation = designation?.Text;
                
                TextBlock? area = base_DataGrid.Columns[3].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentArea = area?.Text;
                currentTable = "Готовая продукция";
            }


        }

        private void edit_Button_Click(object sender, RoutedEventArgs e) {
            List<string> first = new() { "Название", currentName };
            List<string> second = new() { "Марка", currentDesignation };
            List<string> third = new() { "Область применения", currentArea };

            AddAndEditWindow addAndEditWindow = new AddAndEditWindow(currentTable, true, currentID, first, second, third);
            addAndEditWindow.ShowDialog();

        }

        private void FillEquipment(List<List<string>> dt) {
            SetUpColumnsEquipment();
            List<DataForEquipment> data = new();
            for (int i = 0; i < dt.Count; i++) {
                data.Add(new DataForEquipment {
                    ID = dt[i][0],
                    Name = dt[i][1],
                    Designation = dt[i][2]
                });
            }

            base_DataGrid.ItemsSource = data;
        }

        private void FillStage(List<List<string>> dt) {
            SetUpColumnsStage();
            List<DataForStage> data = new();
            for (int i = 0; i < dt.Count; i++) {
                data.Add(new DataForStage {
                    ID = dt[i][0],
                    Name = dt[i][1]
                });
            }

            base_DataGrid.ItemsSource = data;
        }

        private void tables_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (isFirstEnter) {
                isFirstEnter = false;
                return;
            }

            base_DataGrid.ItemsSource = null;
            base_DataGrid.Columns.Clear();

            string table = (string)tables_ComboBox.SelectedItem;

            if (table == "Готовая продукция") {
                List<List<string>> dt = _databaseWork.GetTableFinalProduct();

                FillFinalProduct(dt);
            } else if (table == "Оборудование") {
                List<List<string>> dt = _databaseWork.GetTableEquipment();

                FillEquipment(dt);
            } else {
                List<List<string>> dt = _databaseWork.GetTableStage();

                FillStage(dt);
            }
        }
    }
}
