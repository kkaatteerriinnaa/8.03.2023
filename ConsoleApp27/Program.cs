using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27
{
    class Magazine : IDisposable
    {
        FileStream file;
        BinaryWriter writer;
        public string Name { set; get; }
        public string Adress { set; get; }
        public string Type { set; get; }

        public Magazine(string name, string adress, string type)
        {
            this.Name = name;
            this.Adress = adress;
            this.Type = type;
            file = new FileStream(name + ".dat", FileMode.Create, FileAccess.Write);
            file = new FileStream(adress + ".dat", FileMode.Create, FileAccess.Write);
            file = new FileStream(type + ".dat", FileMode.Create, FileAccess.Write);
            writer = new BinaryWriter(file);
            writer.Write("IDisposable");
        }

        public void Dispose()
        {
            Console.WriteLine("Освобождение ресурсов объекта!");
            writer.Close();
            file.Close();
        }
    }
    class Piece
    {
        public string Name { set; get; }
        public int Age { set; get; }
        public string FIO { set; get; }
        public string Genre { set; get; }

        public Piece(string Name, int Age, string fio, string genre)
        {
            this.Name = Name;
            this.Age = Age;
            this.FIO = fio;
            this.Genre = genre;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Количество байт в куче: {0}\nМаксимальное количество поддерживаемых поколений объектов: {1}",
                GC.GetTotalMemory(false), GC.MaxGeneration + 1);

            Piece piece2 = new Piece("Name", 1999,"FIO","Horror");
          
            Console.WriteLine("Поколение объекта user1: " + GC.GetGeneration(piece2));

            for (int i = 0; i < 50000; i++)
            {
                Piece piece = new Piece("Name", 1999, "FIO", "Horror");
            }

            GC.Collect(0, GCCollectionMode.Forced);
            
            GC.WaitForPendingFinalizers();
            Console.WriteLine("\nсборка мусора ...\n");

            Console.WriteLine("Теперь поколение объекта piece: " + GC.GetGeneration(piece2));

            Magazine obj = new Magazine("Name","Adress","Type");
            obj.Dispose();

            Console.Read();
        }
    }
}
