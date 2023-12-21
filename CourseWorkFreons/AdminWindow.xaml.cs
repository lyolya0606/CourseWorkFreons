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
            delete_Button.IsEnabled = true;
            int selectedRow = base_DataGrid.SelectedIndex;

            if (selectedRow == -1) { return; }

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

            } else if (table == "Оборудование") {
                TextBlock? id = base_DataGrid.Columns[0].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentID = id?.Text;

                TextBlock? name = base_DataGrid.Columns[1].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentName = name?.Text;

                TextBlock? designation = base_DataGrid.Columns[2].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentDesignation = designation?.Text;

                currentTable = "Оборудование";
            } else {
                TextBlock? id = base_DataGrid.Columns[0].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentID = id?.Text;

                TextBlock? name = base_DataGrid.Columns[1].GetCellContent(base_DataGrid.Items[selectedRow]) as TextBlock;
                currentName = name?.Text;

                currentTable = "Стадия";
            }


        }

        private void edit_Button_Click(object sender, RoutedEventArgs e) {
            List<string> first = new();
            List<string> second = new();
            List<string> third = new();
            if (currentTable == "Готовая продукция") {
                first.Add("Название");
                first.Add(currentName);
                second.Add("Марка");
                second.Add(currentDesignation);
                third.Add("Область применения");
                third.Add(currentArea);
            } else if (currentTable == "Оборудование") {
                first.Add("Название");
                first.Add(currentName);
                second.Add("Обозначение");
                second.Add(currentDesignation);
                third.Add("");
                third.Add("");
            } else {
                first.Add("Название");
                first.Add(currentName);
                second.Add("");
                second.Add("");
                third.Add("");
                third.Add("");
            }

            AddAndEditWindow addAndEditWindow = new AddAndEditWindow(currentTable, true, currentID, first, second, third);
            addAndEditWindow.ShowDialog();
            base_DataGrid.ItemsSource = null;
            base_DataGrid.Columns.Clear();

            if (currentTable == "Готовая продукция") {
                List<List<string>> dt = _databaseWork.GetTableFinalProduct();
                base_DataGrid.SelectedIndex = -1;
                FillFinalProduct(dt);
            } else if (currentTable == "Оборудование") {
                List<List<string>> dt = _databaseWork.GetTableEquipment();
                base_DataGrid.SelectedIndex = -1;
                FillEquipment(dt);
            } else {
                List<List<string>> dt = _databaseWork.GetTableStage();
                base_DataGrid.SelectedIndex = -1;
                FillStage(dt);
            }
            


        }

        private void add_Button_Click(object sender, RoutedEventArgs e) {
            string table = (string)tables_ComboBox.SelectedItem;
            List<string> first = new();
            List<string> second = new();
            List<string> third = new();
            if (table == "Готовая продукция") {
                first.Add("Название");
                first.Add("");
                second.Add("Марка");
                second.Add("");
                third.Add("Область применения");
                third.Add("");
            } else if (table == "Оборудование") {
                first.Add("Название");
                first.Add("");
                second.Add("Обозначение");
                second.Add("");
                third.Add("");
                third.Add("");
            } else {
                first.Add("Название");
                first.Add("");
                second.Add("");
                second.Add("");
                third.Add("");
                third.Add("");
            }

            AddAndEditWindow addAndEditWindow = new AddAndEditWindow(table, false, currentID, first, second, third);
            addAndEditWindow.ShowDialog();
            base_DataGrid.ItemsSource = null;
            base_DataGrid.Columns.Clear();

            if (table == "Готовая продукция") {
                List<List<string>> dt = _databaseWork.GetTableFinalProduct();
                base_DataGrid.SelectedIndex = -1;
                FillFinalProduct(dt);
            } else if (table == "Оборудование") {
                List<List<string>> dt = _databaseWork.GetTableEquipment();
                base_DataGrid.SelectedIndex = -1;
                FillEquipment(dt);
            } else {
                List<List<string>> dt = _databaseWork.GetTableStage();
                base_DataGrid.SelectedIndex = -1;
                FillStage(dt);
            }
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e) {
            if (currentTable == "Готовая продукция") {
                MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить запись из таблицы Готовая продукция:" +
                    $"{currentName} {currentDesignation}?", "Удаление записи", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) {
                    _databaseWork.DeleteFinalProduct(currentID);
                } else {
                    return;
                }

            } else if (currentTable == "Оборудование") {
                MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить запись из таблицы Оборудование:" +
                     $"{currentName} {currentDesignation}?", "Удаление записи", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) {
                    _databaseWork.DeleteEquipment(currentID);
                } else {
                    return;
                }

            } else {
                MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить запись из таблицы Стадия:" +
                     $"{currentName}?", "Удаление записи", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes) {
                    _databaseWork.DeleteStage(currentID);
                } else {
                    return;
                }
            }




            base_DataGrid.ItemsSource = null;
            base_DataGrid.Columns.Clear();

            if (currentTable == "Готовая продукция") {
                List<List<string>> dt = _databaseWork.GetTableFinalProduct();
                base_DataGrid.SelectedIndex = -1;
                FillFinalProduct(dt);
            } else if (currentTable == "Оборудование") {
                List<List<string>> dt = _databaseWork.GetTableEquipment();
                base_DataGrid.SelectedIndex = -1;
                FillEquipment(dt);
            } else {
                List<List<string>> dt = _databaseWork.GetTableStage();
                base_DataGrid.SelectedIndex = -1;
                FillStage(dt);
            }



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
