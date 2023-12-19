using SkiServiceWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls; // Wichtig für den Zugriff auf das Frame-Element

namespace SkiServiceWPF.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Func<UserControl>> _viewsByKey = new Dictionary<string, Func<UserControl>>();
        private Frame _mainContentFrame;

        public void SetMainFrame(Frame mainFrame)
        {
            _mainContentFrame = mainFrame;
        }

        public void RegisterView(string key, Func<UserControl> creator)
        {
            if (_viewsByKey.ContainsKey(key))
            {
                throw new ArgumentException("Key is already registered", nameof(key));
            }
            _viewsByKey.Add(key, creator);
        }

        public void NavigateTo(string pageKey)
        {
            if (!_viewsByKey.TryGetValue(pageKey, out var createFunc))
            {
                throw new ArgumentException("No page registered with this key", nameof(pageKey));
            }

            var newPage = createFunc();
            _mainContentFrame.Navigate(newPage);
        }
    }
}
