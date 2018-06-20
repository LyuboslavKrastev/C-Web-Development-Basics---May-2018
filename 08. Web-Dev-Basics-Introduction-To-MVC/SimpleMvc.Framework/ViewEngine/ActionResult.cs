namespace SimpleMvc.Framework.ViewEngine
{
    using Contracts;
    using System;

    public class ActionResult: IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            this.Action = Activator.CreateInstance(
                Type.GetType(viewFullQualifiedName))
                as IRenderable;

            if(this.Action == null)
            {
                throw new InvalidOperationException("The given view data does not implement IRenderable.");
            }
        }

        public IRenderable Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}