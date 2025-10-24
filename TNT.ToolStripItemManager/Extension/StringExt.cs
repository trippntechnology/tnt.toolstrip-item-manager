using System.Reflection;

namespace TNT.ToolStripItemManager.Extension;

/// <summary>
/// Provides extension methods for string manipulation and resource operations.
/// </summary>
public static class StringExt
{
  /// <summary>
  /// Converts a manifest resource name to an <see cref="Image"/> object.
  /// </summary>
  /// <param name="resource">The name of the manifest resource to load as an image.</param>
  /// <returns>
  /// A new <see cref="Bitmap"/> created from the resource stream, or <c>null</c> if the resource is not found.
  /// </returns>
  /// <remarks>
  /// This method uses the calling assembly to locate and load the embedded resource.
  /// The resource must be a valid image format supported by the <see cref="Bitmap"/> constructor.
  /// </remarks>
  public static Image? ToImage(this string resource)
  {
    try
    {
      var assembly = Assembly.GetCallingAssembly();
      var resourceStream = assembly.GetManifestResourceStream(resource);
      return resourceStream == null ? null : new Bitmap(resourceStream);
    }
    catch
    {
      return null;
    }
  }
}
