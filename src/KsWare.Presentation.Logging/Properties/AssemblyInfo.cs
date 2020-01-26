using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using static KsWare.Presentation.Logging.AssemblyInfo;


[assembly: Guid("b6413499-65f8-4cc9-8696-aab3f3053947")]


[assembly: XmlnsDefinition(XmlNamespace, RootNamespace)]
[assembly: XmlnsPrefix(XmlNamespace, "ksl")]

[assembly: InternalsVisibleTo("KsWare.Presentation.Logging.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010041A176886DD69E39D1B9A10017A286FBF650E8F0CB84879B097856B3DFDD194CB06561F36780A9AD61BA8A69DEC80B4FAE69D8723BD67ED3052E82A10E221159DF072118B887BE867A299EB12A1256741E0230FDECF6BA9A806034E1C543EABD9E2CC21DCBE9B61463DB6635B0867EDA7E588409A155D97E17162257B61DECB4")]


// namespace must equal to assembly name
// ReSharper disable once CheckNamespace
namespace KsWare.Presentation.Logging
{
	/// <summary>
	/// Provides assembly information
	/// </summary>
	public static class AssemblyInfo
	{

		/// <summary>
		/// Gets this assembly.
		/// </summary>
		/// <value>The assembly.</value>
		public static Assembly Assembly => Assembly.GetExecutingAssembly();

		/// <summary>
		/// The XML namespace for this assembly
		/// </summary>
		public const string XmlNamespace = "http://ksware.de/Presentation/Logging";

		/// <summary>
		/// The root namespace for this assembly
		/// </summary>
		public const string RootNamespace = "KsWare.Presentation.Logging";

	}
}
