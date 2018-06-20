namespace SimpleMvc.Framework.ViewEngine.Generic
{
    using Contracts.Generic;
    using System;

    public class ActionResult<TModel>: IActionResult<TModel>
    {
        public ActionResult(string viewFullQualifiedName, TModel model)
        {
            this.Action = Activator
                .CreateInstance(
                    Type.GetType(viewFullQualifiedName))
                    as IRenderable<TModel>;

            if(this.Action == null)
            {
                throw new InvalidOperationException("The given view data does not implement IRenderable<TModel>.");
            }
            
            this.Action.Model = model;
        }

        public IRenderable<TModel> Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}