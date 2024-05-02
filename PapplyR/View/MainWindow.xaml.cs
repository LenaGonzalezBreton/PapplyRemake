using PapplyR.View;

namespace PapplyR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void btn_dashboard_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Dashboard dashboard = new Dashboard();

            frame_template.Content = dashboard;
        }

        private void btn_eleve_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Student student = new Student();
            
            frame_template.Content = student;
        }

        private void btn_promotion_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Promotion promotion = new Promotion();
            frame_template.Content = promotion;
        }

        private void btn_tp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
        }
    }
}