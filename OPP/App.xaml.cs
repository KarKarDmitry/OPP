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
        }

        private static Dictionary<string, Func<Page>> PageConstructors = new Dictionary<string, Func<Page>>();
        private static Dictionary<string, Page> NavigationPages = new Dictionary<string, Page>();

        public static Page GetNavigationPage<NavigationPageType>(string name) where NavigationPageType : Page, new()
        {
            // Если страница с таким именем уже существует, возвращаем её
            if (NavigationPages.ContainsKey(name))
            {
                return NavigationPages[name];
            }
            else
            {
                // Если страницы нет, создаём новую и добавляем в словарь
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

                    // Получаем тип элементов в ItemsSource
                    var itemType = comboBox.ItemsSource.Cast<object>().FirstOrDefault()?.GetType();

                    if (comboBox.Text == itemType?.FullName) comboBoxSearchText = string.Empty;

                    comboBoxSearchText += GetCharFromKey(e.Key, e.KeyboardDevice.Modifiers);
                    comboBox.Text = comboBoxSearchText;

                    // Если элемент в ItemsSource найден, получаем тип
                    if (itemType != null)
                    {
                        // Получаем тип базового класса AbstractGuides<T>
                        var baseType = itemType.BaseType;

                        // Проверяем, что базовый тип это AbstractGuides<T>
                        if (baseType != null && baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(AbstractGuides<>))
                        {
                            // Получаем метод GetAll через рефлексию, предполагая что он есть в AbstractGuides<T>
                            var method = baseType.GetMethod("GetAll", BindingFlags.Public | BindingFlags.Static);

                            if (method != null)
                            {
                                // Если строка поиска пуста, возвращаем все элементы через метод GetAll
                                if (string.IsNullOrEmpty(comboBoxSearchText))
                                {
                                    var allItems = method.Invoke(null, null) as IEnumerable<object>;
                                    if (allItems != null)
                                    {
                                        comboBox.ItemsSource = allItems.ToList(); // Обновляем ItemsSource
                                    }
                                }
                                else
                                {
                                    // Фильтруем элементы по всем свойствам
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
                                        comboBox.ItemsSource = allItems.ToList(); // Если ничего не нашли, показываем все
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
            // Получение виртуального кода клавиши
            uint virtualKeyCode = (uint)KeyInterop.VirtualKeyFromKey(key);
            uint scanCode = MapVirtualKey(virtualKeyCode, 0);

            // Состояние клавиш на клавиатуре
            byte[] keyboardState = new byte[256];
            if (!GetKeyboardState(keyboardState))
            {
                return string.Empty;
            }

            // Учет модификатора Shift
            if ((modifiers & ModifierKeys.Shift) != 0)
            {
                keyboardState[0x10] = 0x80; // Включаем Shift
            }

            // Буфер для символа
            StringBuilder receivingBuffer = new StringBuilder(2);

            // Преобразование клавиши в символ с учетом раскладки
            int result = ToUnicode(virtualKeyCode, scanCode, keyboardState, receivingBuffer, receivingBuffer.Capacity, 0);

            // Если символ успешно преобразован, вернуть его
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
                    return true; // Если найдено совпадение
                }
            }

            return false; // Если ни одно свойство не совпадает
        }

        #endregion

    }
}
