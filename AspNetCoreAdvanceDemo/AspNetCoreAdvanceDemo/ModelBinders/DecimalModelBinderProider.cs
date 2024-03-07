using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreAdvanceDemo.ModelBinders
{
	public class DecimalModelBinderProider : IModelBinderProvider
	{
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			if (context.Metadata.ModelType == typeof(decimal)
				|| context.Metadata.ModelType == typeof(decimal?))
			{
				return new DecimalModelBinder();
			}

			return null;
		}
	}
}
