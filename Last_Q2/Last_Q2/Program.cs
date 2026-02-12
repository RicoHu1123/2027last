using System;
using MySqlConnector;

namespace Last_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=172.16.2.26;user=Biko;password=114514;database=LastBiko";


            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine("コマンドを選択してください: insert, delete, select");
                string command = Console.ReadLine()?.Trim().ToLower();

                switch (command)
                {
                    case "insert":
                        Console.Write("idを入力してください: ");
                        if (!int.TryParse(Console.ReadLine(), out int insertId))
                        {
                            Console.WriteLine("idは整数で入力してください。");
                            break;

                        }
                        Console.Write("textを入力してください: ");
                        string insertText = Console.ReadLine() ?? "";
                        using (var insertCommand = new MySqlCommand("INSERT INTO LastQ2 (id, string) VALUES (@id, @text)", connection))
                        {
                            insertCommand.Parameters.AddWithValue("@id", insertId);
                            insertCommand.Parameters.AddWithValue("@text", insertText);
                            insertCommand.ExecuteNonQuery();
                            Console.WriteLine("入力完了しました。");
                        }
                        break;

                    case "delete":
                        Console.Write("削除するidを入力してください: ");
                        if (!int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            Console.WriteLine("idは整数で入力してください。");
                            break;
                        }
                        using (var deleteCommand = new MySqlCommand("DELETE FROM LastQ2 WHERE id = @id", connection))
                        {
                            deleteCommand.Parameters.AddWithValue("@id", deleteId);
                            int rows = deleteCommand.ExecuteNonQuery();
                            Console.WriteLine(rows > 0 ? "削除が完了しました。" : "指定したidは存在しません。");
                        }
                        break;

                    case "select":
                        Console.Write("表示するidを入力してください: ");
                        if (!int.TryParse(Console.ReadLine(), out int selectId))
                        {
                            Console.WriteLine("idは整数で入力してください。");
                            break;
                        }
                        using (var selectCommand = new MySqlCommand("SELECT string FROM LastQ2 WHERE id = @id", connection))
                        {
                            selectCommand.Parameters.AddWithValue("@id", selectId);
                            using (var reader = selectCommand.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    Console.WriteLine($"ID: {selectId}, Text: {reader["string"]}");
                                }
                                else
                                {
                                    Console.WriteLine("指定したidは存在しません。");
                                }
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("不正なコマンドです。insert, delete, select から選択してください。");
                        break;
                }
            }

            // プログラム毎回Commandを一つ実行したら終了するようにする
            // プログラム再開したら他のCommand実行してください
        }
    }
}
