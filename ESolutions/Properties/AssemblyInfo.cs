using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Allgemeine Informationen über eine Assembly werden über die folgenden 
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die mit einer Assembly verknüpft sind.
[assembly: AssemblyTitle ("ESolutions")]
[assembly: AssemblyDescription ("Contains basic tools for Rjindael encryption, password hashing and type conversion as well as helpful extensions for basic typs like Images, String and Lists.")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany ("Tobias Mundt - everyday Solutions")]
[assembly: AssemblyProduct ("ESolutions")]
[assembly: AssemblyCopyright ("Copyright © Tobias Mundt - everyday Solutions 2014")]
[assembly: AssemblyTrademark ("")]
[assembly: AssemblyCulture ("en-US")]

// Durch Festlegen von ComVisible auf "false" werden die Typen in dieser Assembly unsichtbar 
// für COM-Komponenten. Wenn Sie auf einen Typ in dieser Assembly von 
// COM zugreifen müssen, legen Sie das ComVisible-Attribut für diesen Typ auf "true" fest.
[assembly: ComVisible (false)]

// Die folgende GUID bestimmt die ID der Typbibliothek, wenn dieses Projekt für COM verfügbar gemacht wird
[assembly: Guid ("e430d9f5-2f04-4e3a-a29f-edab5f2647f9")]

// Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion 
//      Buildnummer
//      Revision
//
// Sie können alle Werte angeben oder die standardmäßigen Revisions- und Buildnummern 
// übernehmen, indem Sie "*" eingeben:
[assembly: AssemblyVersion ("1.8.9.0")]
[assembly: AssemblyFileVersion ("1.8.9.0")]

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ESolutions.Test")]