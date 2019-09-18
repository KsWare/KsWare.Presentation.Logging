using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("KsWare.Presentation.Logging")]
[assembly: AssemblyDescription("Logging for KsWare Presentation Framwork. ")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("KsWare")]
[assembly: AssemblyProduct("Presentation Framework")]
[assembly: AssemblyCopyright("Copyright © 2019 by KsWare. All rights reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("b6413499-65f8-4cc9-8696-aab3f3053947")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: XmlnsDefinition(KsWare.Presentation.Logging.AssemblyInfo.XmlNamespace, "KsWare.Presentation.Logging")]
[assembly: XmlnsPrefix(KsWare.Presentation.Logging.AssemblyInfo.XmlNamespace, "ksv")]

[assembly: InternalsVisibleTo("KsWare.Presentation.Logging.Tests, PublicKey=002400000480000094000000060200000024000052534131000400000100010041A176886DD69E39D1B9A10017A286FBF650E8F0CB84879B097856B3DFDD194CB06561F36780A9AD61BA8A69DEC80B4FAE69D8723BD67ED3052E82A10E221159DF072118B887BE867A299EB12A1256741E0230FDECF6BA9A806034E1C543EABD9E2CC21DCBE9B61463DB6635B0867EDA7E588409A155D97E17162257B61DECB4")]


// namespace must equal to assembly name
// ReSharper disable once CheckNamespace
namespace KsWare.Presentation.Logging
{
	public static class AssemblyInfo
	{

		public static Assembly Assembly => Assembly.GetExecutingAssembly();

		public const string XmlNamespace = "http://ksware.de/Presentation/ViewFramework";

		public const string RootNamespace = "KsWare.Presentation.Logging";

	}
}