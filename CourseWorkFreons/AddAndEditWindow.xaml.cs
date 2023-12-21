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
    /// Interaction logic for AddAndEditWindow.xaml
    /// </summary>
    public partial class AddAndEditWindow : Window {
        private readonly bool _isEdit;
        private readonly string _id;
        private readonly string _table;
        private List<string> _firstLabel = new();
        private List<string> _secondLabel = new();
        private List<string> _thirdLabel = new();

        public AddAndEditWindow(string table, bool isEdit, string id, List<string> firstLabel, List<string> secondLabel, List<string> thirdLabel) {
            InitializeComponent();
            _isEdit = isEdit;
            _id = id;
            _firstLabel = firstLabel;
            _secondLabel = secondLabel;
            _thirdLabel = thirdLabel;
            _table = table;
            ShowLabels();
        }

        private void ShowLabels() {
            first_Label.Content = _firstLabel[0];
            first_TextBox.Text = _firstLabel[1];

            second_Label.Content = _secondLabel[0];
            second_TextBox.Text = _secondLabel[1];

            third_Label.Content = _thirdLabel[0];
            third_TextBox.Text = _thirdLabel[1];
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e) {
            DatabaseWork databaseWork = new DatabaseWork();
            if (_isEdit) {     
                if (_table == "Готовая продукция") {
                    databaseWork.UpdateFinalProduct(_id, first_TextBox.Text, second_TextBox.Text, third_TextBox.Text);

                } else if (_table == "Оборудование") {
                    databaseWork.UpdateEquipment(_id, first_TextBox.Text, second_TextBox.Text);
                } else {
                    databaseWork.UpdateStage(_id, first_TextBox.Text);
                }
            } else {
                if (_table == "Готовая продукция") {
                    databaseWork.InsertFinalProduct(first_TextBox.Text, second_TextBox.Text, third_TextBox.Text);

                } else if (_table == "Оборудование") {
                    databaseWork.InsertEquipment(first_TextBox.Text, second_TextBox.Text);
                } else {
                    databaseWork.InsertStage(first_TextBox.Text);
                }
            }
            this.Close();
        }
    }
}
