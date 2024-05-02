using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using PapplyR.Model; // Importez l'espace de noms du modèle

namespace PapplyR.View
{
    public partial class Promotion : UserControl
    {
        // Collection de promotions
        public ObservableCollection<PapplyR.Model.Promotion> Promotions { get; set; } // Utilisation explicite de la classe Promotion du modèle

        public Promotion()
        {
            InitializeComponent();
            Promotions = new ObservableCollection<PapplyR.Model.Promotion>(); // Utilisation explicite de la classe Promotion du modèle

            // Récupérez toutes les promotions depuis la base de données
            List<PapplyR.Model.Promotion> allPromotions = PapplyR.Model.Promotion.GetAllPromotions();

            // Ajoutez chaque promotion récupérée à votre collection de promotions
            foreach (var promotion in allPromotions)
            {
                Promotions.Add(promotion);
            }

            // Liez la source de données de la ListBox à votre collection de promotions
            lb_promotion.ItemsSource = Promotions;
        }
    }
}
