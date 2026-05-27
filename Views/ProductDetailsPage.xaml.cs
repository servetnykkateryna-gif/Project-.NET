using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class ProductDetailsPage : ContentPage
{
    public ProductDetailsPage(ProductDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
