namespace SkiServiceWPF.Interfaces
{
    /// <summary>
    /// Represents a service for navigating between views
    /// </summary>
    public interface INavigationService
    {
        void NavigateTo(string viewName);
    }
}
