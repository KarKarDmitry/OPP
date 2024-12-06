using System;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using OPP.AppData;

namespace OPP
{
    public static class Autentification
    {

        public const string AppMode = "Editor";
        public static string Password = "1111";
        public static bool isEditable { get; private set; } = true;

        /// <summary>
        /// Метод для аутентификации пользователя с заданным паролем.
        /// </summary>
        /// <param name="password">Пароль для аутентификации.</param>
        /// <returns>Возвращает true, если аутентификация успешна, иначе false.</returns>
        public static bool Autentificate(string password)
        {
            // Обновляем пароль
            Password = password;

            // Создаем строку подключения с новым паролем
            string connectionString = DatabaseConnection.ConnectionString;

            try
            {
                // Пытаемся открыть соединение с базой данных
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Если соединение удачно открыто, аутентификация успешна
                    connection.Close();
                }
                if (AppMode == "Editor")
                    isEditable = true;
                else isEditable = false;
                return true; // Аутентификация успешна
            }
            catch (Exception ex)
            {
                // Логируем ошибку для отладки
                Trace.WriteLine($"Ошибка при аутентификации: {ex.Message}");
                return false; // Аутентификация не удалась
            }
        }
    }
}
