using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ScrollerEngineData;

namespace ScrollerEngineGameEditor
{
    /// <summary>
    /// Interaction logic for EnemySelector.xaml
    /// </summary>
    public partial class EnemySelector : Window
    {
        public EnemySelector(GameEntry gameEntry)
        {
            InitializeComponent();
            //Current.AvailableCharacters.Values.First().jumpSpeed 
            _current = gameEntry;
            
            //comboBox1.DataContext = _current.AvailableCharacters.Values;
        }

        private GameEntry _current;
        public GameEntry Current
        {
            get { return _current; }
            set { _current = value; }
        }

        public Dictionary<string, Character> AvailableCharacters
        {
            get { return Current.AvailableCharacters; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            comboBox1.DataContext = _current.AvailableCharacters;
        }
    }
}
