namespace SimpleMvc.Framework
{
    using WebServer;
    using System;
    using System.Reflection;

    public static class MvcEngine
    {
        public static void Run(WebServer server)
        {
            RegisterAssemblyName();

            try
            {
                server.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //sets the current Assembly, so that we can access the folders in it 
        private static void RegisterAssemblyName()
        {
            MvcContext.Get.AssemblyName = Assembly
                .GetEntryAssembly()
                .GetName()
                .Name;
        }
    }
}