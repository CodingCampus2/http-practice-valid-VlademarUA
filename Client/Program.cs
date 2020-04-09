using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("JSON data example {\"dataId\":5,\"weight\":5}");
            string request;
            Client myClient = new Client();

            while (true)
            {
                request = Console.ReadLine();
                string[] argz = request.ToLower().Split(' ');

                string requestType = argz[0];
                switch (requestType)
                {
                    case "get":
                        {
                            bool isSorted = false;
                            string id = "";
                            if (argz.Length >= 3 && argz[1].Contains("id"))
                            {
                                id = argz[2];
                                await myClient.GetById(id);
                            }
                            else if (argz.Length >= 3 && argz[1].Contains("sorted"))
                            {
                                isSorted = argz[2].Contains("true");
                                await myClient.GetAll(isSorted);
                            }
                            else
                            {
                                try
                                {
                                    await myClient.GetAll(isSorted);
                                }
                                catch(Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                            break;
                        }

                    case "post":
                        {
                            SomeData newData = new SomeData();
                            if (argz.Length >= 2)
                            {
                                newData = JsonConvert.DeserializeObject<SomeData>(argz[1]);
                            }
                            try
                            {
                                await myClient.PostData(newData);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        }
                    case "put":
                        {
                            string id = "";
                            if (argz.Length >= 2)
                            {
                                id = argz[1];
                            
                                SomeData updatedData = new SomeData();
                                if (argz.Length >= 3)
                                {
                                    updatedData = JsonConvert.DeserializeObject<SomeData>(argz[2]);
                                }

                                try
                                {
                                    await myClient.PutById(id, updatedData);

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                            break;
                        }
                    case "delete":
                        {
                            string id = "";
                            if (argz.Length >= 2)
                            {
                                id = argz[1];

                                try
                                {
                                    await myClient.DeleteById(id);

                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                            break;
                        }
                    default:
                        Console.WriteLine("Bad request");
                        break;
                }
            }
        }
    }
}
