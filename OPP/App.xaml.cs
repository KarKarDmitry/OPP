using OPP.Views.Pages.Guides;
using Accounting = OPP.Views.Pages.Accounting;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using OPP.Views.Pages.Accounting;
using OPP.Environment;
using System.Windows.Input;
using System.Collections;
using OPP.AppData.Guides;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using OPP.AppData.GroupClasses;

namespace OPP
{
    public partial class App : Application
    {
        static App()
        {

            ThemeColorPicker.LoadAccentColorFromSettings();

            RegisterPage("GuidesPackagesPage", () => new Packages());
            RegisterPage("GuidesAssembliesPage", () => new Assemblies());
            RegisterPage("GuidesBlanksPage", () => new Blanks());
            RegisterPage("GuidesBoxesPage", () => new Boxes());
            RegisterPage("GuidesCoatingsPage", () => new Coatings());
            RegisterPage("GlobalGuideViewPage", () => new GlobalGuidesView());
            RegisterPage("GuidesKitsPage", () => new Kits());
            RegisterPage("GuidesLabelsPage", () => new Labels());
            RegisterPage("GuidesLocksPage", () => new Locks());
            RegisterPage("GuidesManualsPage", () => new Manuals());
            RegisterPage("GuidesMaterialsPage", () => new Materials());
            RegisterPage("GuidesPackagingsPage", () => new Packagings());
            RegisterPage("GuidesPartsPage", () => new Parts());
            RegisterPage("GuidesSectorsPage", () => new Sectors());
            RegisterPage("AccoutingMaterialPricesPage", () => new MaterialPrices());
            RegisterPage("GuidesRemainingsPage", () => new Remainings());
            RegisterPage("GuidesMovementsPage", () => new Movements());
            RegisterPage("GuidesPlansPage", () => new Plans());

        }

        private static Dictionary<string, Func<Page>> PageConstructors = new Dictionary<string, Func<Page>>();
        private static Dictionary<string, Page> NavigationPages = new Dictionary<string, Page>();

        public static Page GetNavigationPage<NavigationPageType>(string name) where NavigationPageType : Page, new()
        {
            if (NavigationPages.ContainsKey(name))
            {
                return NavigationPages[name];
            }
            else
            {
                var newPage = new NavigationPageType();
                NavigationPages[name] = newPage;
                return newPage;
            }
        }

        public static void RegisterPage(string name, Func<Page> constructor)
        {
            PageConstructors[name] = constructor;
        }

        public static Page GetPage(string name)
        {
            if (PageConstructors.ContainsKey(name))
            {
                return PageConstructors[name]();
            }
            throw new InvalidOperationException($"Page '{name}' not found in PageFactory.");
        }

        public static void DeletePage(string name)
        {
            if (PageConstructors.ContainsKey(name))
            {
                PageConstructors.Remove(name);
            }
        }

        #region Логика работы ComboBox
        private static string? comboBoxSearchText { get; set; }
        private void ComboBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (sender is ComboBox comboBox)
                {
                    var itemType = comboBox.ItemsSource.Cast<object>().FirstOrDefault()?.GetType();

                    if (comboBox.Text == itemType?.FullName) comboBoxSearchText = string.Empty;

                    comboBoxSearchText += GetCharFromKey(e.Key, e.KeyboardDevice.Modifiers);
                    comboBox.Text = comboBoxSearchText;

                    if (itemType != null)
                    {
                        var baseType = itemType.BaseType;

                        if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(AbstractGuides<>))
                        {
                            var method = baseType.GetMethod("GetAll", BindingFlags.Public | BindingFlags.Static);

                            if (method != null)
                            {
                                if (string.IsNullOrEmpty(comboBoxSearchText))
                                {
                                    var allItems = method.Invoke(null, null) as IEnumerable<object>;
                                    if (allItems != null)
                                    {
                                        comboBox.ItemsSource = allItems.ToList(); 
                                    }
                                }
                                else
                                {
                                    var allItems = method.Invoke(null, null) as IEnumerable<object>;

                                    var filteredItems = allItems
                                        .Where(item => FilterByAllProperties(item, comboBoxSearchText))
                                        .ToList();

                                    if (filteredItems.Any())
                                    {
                                        comboBox.ItemsSource = filteredItems;
                                    }
                                    else
                                    {
                                        comboBox.ItemsSource = allItems.ToList(); 
                                    }
                                }

                                comboBox.IsDropDownOpen = comboBox.ItemsSource.Cast<object>().Any();
                            }
                        }
                    }
                }
            }
            catch { };
        }

        [DllImport("user32.dll")]
        private static extern int ToUnicode(
            uint virtualKeyCode,
            uint scanCode,
            byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder receivingBuffer,
            int bufferSize,
            uint flags);

        [DllImport("user32.dll")]
        private static extern bool GetKeyboardState(byte[] keyboardState);

        [DllImport("user32.dll")]
        private static extern uint MapVirtualKey(uint code, uint mapType);

        public static string GetCharFromKey(Key key, ModifierKeys modifiers)
        {
            uint virtualKeyCode = (uint)KeyInterop.VirtualKeyFromKey(key);
            uint scanCode = MapVirtualKey(virtualKeyCode, 0);

            byte[] keyboardState = new byte[256];
            if (!GetKeyboardState(keyboardState))
            {
                return string.Empty;
            }

            if ((modifiers & ModifierKeys.Shift) != 0)
            {
                keyboardState[0x10] = 0x80; 
            }

            StringBuilder receivingBuffer = new StringBuilder(2);

            int result = ToUnicode(virtualKeyCode, scanCode, keyboardState, receivingBuffer, receivingBuffer.Capacity, 0);

            return result > 0 ? receivingBuffer.ToString() : string.Empty;
        }

        private bool FilterByAllProperties(object item, string searchText)
        {
            var properties = item.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (!property.CanRead)
                    continue;

                var value = property.GetValue(item)?.ToString();
                if (!string.IsNullOrEmpty(value) && value.ToLower().Contains(searchText))
                {
                    return true; 
                }
            }

            return false; 
        }

        #endregion

    }
}
