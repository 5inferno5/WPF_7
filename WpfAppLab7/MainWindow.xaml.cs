using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppLab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //	Коллекция для привязки к списку

        ObservableCollection<Table> Tables;
           
        
        
        public MainWindow()

        {
            Tables = new ObservableCollection<Table>();
            InitializeComponent();
            lBox.DataContext = Tables;
        }


        //	Заполнение коллекции данными

        void FillData()
        {
            Tables.Clear();
            foreach (var item in Table.GetAllTables())
            {
                Tables.Add(item);
            }
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var p = new Table();
            Detales dialog = new Detales();
            dialog.grid.DataContext = p;
            dialog.ShowDialog();
            p.Insert();
            FillData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            var p = (Table)lBox.SelectedItem;
            Detales dialog = new Detales();
            dialog.grid.DataContext = p;
            dialog.ShowDialog();

            p.Update();
            FillData();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Table)lBox.SelectedItem).Id;
            Table.Delete(id);
            FillData();
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            Tables.Clear();
            foreach (var A in Table.Search(search.Text))
                Tables.Add(A);
        }
        
    }
}
