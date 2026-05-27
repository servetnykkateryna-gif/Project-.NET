using CommunityToolkit.Mvvm.ComponentModel;

namespace VelvetRelics.ViewModels;

/// <summary>
/// Представляє одну категорію у горизонтальному списку фільтрів каталогу.
/// Реактивна властивість IsSelected дозволяє автоматично оновлювати підсвітку
/// активної категорії без перезавантаження всього списку.
/// </summary>
public partial class CategoryItemViewModel : ObservableObject
{
    public string Name { get; init; } = string.Empty;

    [ObservableProperty]
    private bool isSelected;
}
