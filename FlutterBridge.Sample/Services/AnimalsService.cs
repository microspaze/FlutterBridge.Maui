using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Extensions;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Sample.Services
{
    [BridgeService]
    public class AnimalsService
    {
        [BridgeOperation]
        public async Task<AnimalListValue> GetAnimals()
        {
            var animals = new List<Animal>()
            {
                new()
                {
                    Birthday = new DateTime(2000, 1, 1).ToShortDateString(),
                    Name = "Flyppy",
                    Height = 10,
                    Weight = 5,
                    Image = await GetRawBytes("butterfly.jpg"),
                    Type = nameof(Butterfly),
                    Butterfly = new(){
                        AntennaLength = 57,
                    }
                },
                new()
                {
                    Birthday = new DateTime(2007, 11, 7).ToShortDateString(),
                    Name = "Floppy",
                    Height = 250,
                    Weight = 990,
                    Image = await GetRawBytes("elephant.jpg"),
                    Type = nameof(Elephant),
                    Elephant = new(){
                        TrunkLength = 150,
                    }
                },
                new()
                {
                    Birthday = new DateTime(2007, 11, 7).ToShortDateString(),
                    Name = "Gingle",
                    Height = 25,
                    Weight = 35,
                    Image = await GetRawBytes("monkey.jpg"),
                    Type = nameof(Monkey),
                    Monkey = new(){
                        SmartLevel = 57,
                    }
                },
                new()
                {
                    Birthday = new DateTime(2004, 7, 10).ToShortDateString(),
                    Name = "Pappa",
                    Height = 13,
                    Weight = 16,
                    Image = await GetRawBytes("parrot.jpg"),
                    Type = nameof(Parrot),
                    Parrot = new(){
                        BeakLength = 57,
                    }
                },
                new()
                {
                    Birthday = new DateTime(2001, 5, 22).ToShortDateString(),
                    Name = "Rino",
                    Height = 170,
                    Weight = 643,
                    Image = await GetRawBytes("rhino.jpg"),
                    Type = nameof(Rhino),
                    Rhino = new(){
                        HornLength = 57,
                    }
                },
                new()
                {
                    Birthday = new DateTime(2011, 6, 15).ToShortDateString(),
                    Name = "Biss",
                    Height = 180,
                    Weight = 25,
                    Image = await GetRawBytes("snake.jpg"),
                    Type = nameof(Snake),
                    Snake = new(){
                        VenomLevel = 10,
                    }
                },
                new()
                {
                    Birthday = new DateTime(2000, 1, 1).ToShortDateString(),
                    Name = "Lea",
                    Height = 80,
                    Weight = 100,
                    Image = await GetRawBytes("tiger.jpg"),
                    Type = nameof(Tiger),
                    Tiger = new(){
                        ClawLength = 46,
                    }
                },
            };
            var result = new AnimalListValue();
            result.Values.AddRange(animals);
            result.Values.AddRange(animals);
            result.Values.AddRange(animals);
            result.Values.AddRange(animals);
            result.Values.AddRange(animals);

            return result;
        }

        private static async Task<byte[]> GetRawBytes(string fileName)
        {
            byte[]? bytes = null;
            try
            {
                //ISSUE: https://github.com/dotnet/maui/issues/7471
                //If you want to load an image / file using some API -you might be able to use the Stream directly and pass that
                //If you want to just get the file onto disk - you can File.Create to a local/ cache location and then CopyAsync
                //If you want to get the string contents -you can use the StreamReader and ReadToEnd
                //If you really just want raw bytes as you mention - you can copy to a MemoryStream and then either ToArray(make another copy) or just use the inner array buffer with TryGetBuffer and use that(no copy)
                using var stream = await FileSystem.OpenAppPackageFileAsync(fileName);
                using var memoryStream = new MemoryStream();//MUST COPY TO MemoryStream, THEN READ
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return bytes ?? [];
        }
    }
}
