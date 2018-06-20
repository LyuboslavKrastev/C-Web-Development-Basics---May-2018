namespace  SimpleMvc.Framework
{
    public class MvcContext
    {
        private static MvcContext Instance;

        private MvcContext(){}

        public static MvcContext Get 
        {
            get
            {
                if (Instance == null)
                {
                     Instance = new MvcContext();
                }     
                 return Instance;          
            }         
        } 

        public string AssemblyName { get; set; }

        public string ControllersFolder { get; set; } = "Controllers";

        public string ControllerSuffix { get; set; } = "Controller";

        public string ViewsFolder { get; set; } = "Views";

        public string ModelsFolder { get; set; } = "Models";
    }
}