using SkiServiceWPF.Interfaces;
using System.Windows.Controls;

namespace SkiServiceWPF.Services
{
    /// <summary>
    /// Service for managing navigation within the application
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Func<UserControl>> _viewsByKey = new Dictionary<string, Func<UserControl>>();
        private Frame _mainContentFrame;

        /// <summary>
        /// Sets the main frame where pages will be displayed
        /// </summary>
        /// <param name="mainFrame">The frame used for navigation</param>
        #region SetMainFrame
        public void SetMainFrame(Frame mainFrame)
        {
            _mainContentFrame = mainFrame;
        }
        #endregion

        /// <summary>
        /// Registers a view with a unique key for navigation
        /// </summary>
        /// <param name="key">Unique key for the view</param>
        /// <param name="creator">Function to create the view</param>
        /// <exception cref="ArgumentException">Thrown if the key is already registered</exception>
        #region RegisterView
        public void RegisterView(string key, Func<UserControl> creator)
        {
            if (_viewsByKey.ContainsKey(key))
            {
                throw new ArgumentException("Key is already registered", nameof(key));
            }
            _viewsByKey.Add(key, creator);
        }
        #endregion

        /// <summary>
        /// Navigates to a registered view based on its key
        /// </summary>
        /// <param name="pageKey">Key of the page to navigate to</param>
        /// <exception cref="ArgumentException">Thrown if no page is registered with the given key</exception>
        #region NavigateTo
        public void NavigateTo(string pageKey)
        {
            if (!_viewsByKey.TryGetValue(pageKey, out var createFunc))
            {
                throw new ArgumentException("No page registered with this key", nameof(pageKey));
            }

            var newPage = createFunc();
            _mainContentFrame.Navigate(newPage);

            _mainContentFrame.NavigationService.RemoveBackEntry();
        }
        #endregion
    }
}