namespace PaCoSe.Infra.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static void SetProperty(this object obj, string propName, object value)
        {
            obj.GetType().GetProperty(propName).SetValue(obj, value, null);
        }
    }
}
