using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Posrtgre;

class Program
{

    static void Main()
    {
        while (true)
        {
            string connString = "Host=localhost;Port=5432;Username=postgres;Password=1;Database=postgres";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Выбор тыблицы");
            Console.ResetColor();
            Console.WriteLine("1. Бренд");
            Console.WriteLine("2. Модель Бренда");
            Console.WriteLine("3. Фулл таблы");
            Console.WriteLine("4. В обратном порядке бренд");
            Console.WriteLine("5. В обратном порядке модель");


            int Table = Convert.ToInt16(Console.ReadLine());
            switch (Table)
            {
                case 1:
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        using (var context = new PostgresContext())
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Таблица: Бренд\n");
                            Console.ResetColor();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Brand ID\t Brand Name");
                            Console.ResetColor();

                            foreach (var brand in context.Brands)
                            {
                                Console.WriteLine($"{brand.BrandId}\t\t   {brand.BrandName}");
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nВыберите действие с таблицей");
                        Console.ResetColor();
                        Console.WriteLine("1. Добавить данные в таблицу");
                        Console.WriteLine("2. Обновить данные в таблице");
                        Console.WriteLine("3. Удалить данные в таблице");

                        //ВЫБОР ДЕЙСТВИЯ С ТАБЛИЦЕЙ
                        int L = Convert.ToInt16(Console.ReadLine());
                        switch (L)
                        {
                            //ДОБАВЛЕНИЕ ДАННЫХ В ТАБЛИЦУ
                            case 1:
                                using (var context = new PostgresContext())
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nДобавление данных...");
                                    Console.ResetColor();

                                    // Получить название бренда от пользователя
                                    Console.WriteLine("Введите название бренда:");
                                    string brandName = Console.ReadLine();

                                    // Создать новый бренд
                                    var newBrand = new Brand { BrandName = brandName };

                                    // Добавить новый бренд в контекст
                                    context.Brands.Add(newBrand);

                                    // Сохранить изменения в базе данных
                                    context.SaveChanges();

                                    Console.WriteLine("Бренд добавлен.");
                                }
                                break;


                                //ОБНОВЛЕНИЕ ДАННЫХ В ТАБЛИЦЕ
                            case 2:
                                using (var context = new PostgresContext()) 
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("\nОбновление данных...");
                                    Console.ResetColor();

                                    // Получить бренд по названию
                                    Console.WriteLine("Введите название бренда для обновления:");
                                    string brandName = Console.ReadLine();
                                    var brand = context.Brands.FirstOrDefault(b => b.BrandName == brandName);

                                    if (brand != null)
                                    {
                                        // Изменить название бренда
                                        Console.WriteLine("Введите новое название бренда:");
                                        string newBrandName = Console.ReadLine();
                                        brand.BrandName = newBrandName;

                                        // Обновить бренд в контексте
                                        context.Brands.Update(brand);

                                        // Сохранить изменения в базе данных
                                        context.SaveChanges();

                                        Console.WriteLine("Бренд обновлен.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Бренд с таким названием не найден.");
                                    }
                                }
                                break;

                            //УДАЛЕНИЕ ДАННЫХ
                            case 3:
                                using (var context = new PostgresContext())
                                {
                                    Console.WriteLine("Введите название бренда для удаления:");
                                    string brandName = Console.ReadLine();
                                    var brand = context.Brands.FirstOrDefault(b => b.BrandName == brandName);

                                    context.Brands.Remove(brand);
                                    context.SaveChanges();
                                }
                                
                             break;

                        }

                    }
                    break;

                case 2:
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        string sql = "SELECT * FROM model_brand";

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Таблица: Модель Бренда\n");
                            Console.ResetColor();
                            //Чтение данных из таблицы
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Model ID\t Brand ID\t Model Name");
                                Console.ResetColor();

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0}\t\t   {1}\t\t {2}", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2));
                                }
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nВыберите действие с таблицей");
                        Console.ResetColor();
                        Console.WriteLine("1. Добавить данные в таблицу");
                        Console.WriteLine("2. Обновить данные в таблице");
                        Console.WriteLine("3. Удалить данные в таблице");

                        int M = Convert.ToInt16(Console.ReadLine());
                        switch (M)
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nДобавление данных...");
                                Console.ResetColor();

                                string insertSQL = "INSERT INTO model_brand (brand_id,model_name) VALUES (@brandID,@ModelName)";
                                using (NpgsqlCommand insert = new NpgsqlCommand(insertSQL, conn))
                                {
                                    int BrandID = -1;
                                    do
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine("Введите название бренда: ");
                                        Console.ResetColor();
                                        string BrandName = Console.ReadLine();

                                        string selectBrandID = "SELECT brand_id FROM brand WHERE brand_name=@BrandName";

                                        using (NpgsqlCommand selectBrand = new NpgsqlCommand(selectBrandID, conn))
                                        {
                                            selectBrand.Parameters.AddWithValue("@BrandName", BrandName);
                                            var result = selectBrand.ExecuteScalar();
                                            if (result != null)
                                            {
                                                BrandID = Convert.ToInt32(result);
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine("\nБренд не найден");
                                                Console.ResetColor();
                                            }
                                        }
                                    } while (BrandID == -1);

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("\nВведите название Модели: ");
                                    Console.ResetColor();
                                    string ModelName = Console.ReadLine();
                                    insert.Parameters.AddWithValue("@brandID", BrandID);
                                    insert.Parameters.AddWithValue("@ModelName", ModelName);
                                    insert.ExecuteNonQuery();
                                }
                            break;


                            //ОБНОВЛЕНИЕ ДАННЫХ
                            case 2:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nОбновление данных...");
                                Console.ResetColor();
                                Console.WriteLine("Введите 1 если хотите поменять id или2 если модель: ");




                                string updateSQL2 = "UPDATE model_brand SET model_name=@NewModelName WHERE  model_name=@ModelName";
                                using (NpgsqlCommand updatee = new NpgsqlCommand(updateSQL2, conn))
                                {

                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Введите название модели для обновления:");
                                    Console.ResetColor();

                                    string ModelName = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Введите новое название модели:");
                                    Console.ResetColor();

                                    string NewModelName = Console.ReadLine();
                                    updatee.Parameters.AddWithValue("@ModelName", ModelName);
                                    updatee.Parameters.AddWithValue("@NewModelName", NewModelName);
                                    updatee.ExecuteNonQuery();

                                }
                            break;

                            //УДАЛЕНИЕ ДАННЫХ
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nУдаление данных...");
                                Console.ResetColor();

                                string deleteSQL = "DELETE from model_brand WHERE model_id=@ModelID";
                                using (NpgsqlCommand delete = new NpgsqlCommand(deleteSQL, conn))
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Введите номер id для удаления:");
                                    Console.ResetColor();
                                    int BrandName = Convert.ToInt32(Console.ReadLine());
                                    delete.Parameters.AddWithValue("@ModelID", BrandName);
                                    delete.ExecuteNonQuery();
                                }
                                break;
                        }

                        break;

                    }
                //СЛОЖНЫЙ ЗАПРОС
                case 3:
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        string sql = $"SELECT brand.brand_name, model_brand.model_name FROM brand INNER JOIN model_brand ON brand.brand_id=model_brand.brand_id;";

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Таблица: Модель Бренда\n");
                            Console.ResetColor();
                            //Чтение данных из таблицы
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("BrandName\t\t   ModelName");
                                Console.ResetColor();

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0}\t\t\t   {1}\t\t ", reader.GetString(0), reader.GetString(1));
                                }
                            }
                        }
                    }

              break;

                case 4:
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        string sql = $"SELECT * FROM brand ORDER BY brand_id DESC";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nBrand ID\t Brand Name");
                                Console.ResetColor();

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0}\t\t   {1}", reader.GetInt32(0), reader.GetString(1));
                                }
                            }
                        }
                    }
               break;
                case 5:
                    using (var conn = new NpgsqlConnection(connString))
                    {
                        conn.Open();
                        string sql = $"SELECT * FROM model_brand ORDER BY model_id DESC";
                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nModel ID\t Brand ID\t Model Name");
                                Console.ResetColor();

                                while (reader.Read())
                                {
                                    Console.WriteLine("{0}\t\t   {1}\t\t   {2} ", reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2));
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}




